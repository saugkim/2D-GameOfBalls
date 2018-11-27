using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public float force;
    public Rigidbody2D playerRB;

    public float minForce;
    public float maxForce;

    public bool launched;
    public bool goingDown;
    public float highPoint;

    public float health;
    public float damage;

    public Image forceMeter;

    public SceneControl sceneControl;

    public GameObject obstacleParticle;
    //public ParticleSystem effectParticle;
    GameObject keyPair;

  //  private Vector3 startPoint;

    public GUIStyle myGUIStyle;

    public SpriteRenderer myDoor;
    public Sprite closedDoor; //Drag your first sprite here in inspector.
    public Sprite openDoor; //Drag your second sprite here in inspector.

    // Use this for initialization
    void Start ()
    {
        myDoor.GetComponent<SpriteRenderer>().sprite = closedDoor;

        myGUIStyle.fontSize = 16;
        myGUIStyle.normal.textColor = Color.white;
	}
	
	// Update is called once per frame
	void Update ()
    {
        LaunchPlayer();
        GetHighestPoint();
        ShowForce();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void ShowForce()
    {
        forceMeter.fillAmount = force / maxForce;
    }

    private void GetHighestPoint()
    {
        if (playerRB.velocity.y < 0 && goingDown == false)
        {
            // We are going down
            GetComponent<MeshRenderer>().material.color = Color.blue;
            highPoint = gameObject.transform.position.y;
            goingDown = true;
        }
    }

    private void LaunchPlayer()
    {
        if (Input.GetMouseButton(0) && launched == false)
        {
            force += 5 * Time.deltaTime;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        if (Input.GetMouseButtonUp(0) && launched == false)
        {
            launched = true;
            GetComponent<MeshRenderer>().material.color = Color.yellow;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Vector3 direction = (mousePos - transform.position).normalized;

            if (direction.y < 0)
            {
                direction *= -1;
            }

            force = Mathf.Clamp(force, minForce, maxForce);

            Launch(force, direction);
            force = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHitBar(collision);
        PlayerHitBase(collision);
        PlayerHitObstacle(collision);
        PlayerHitEnemy(collision);
        PlayerHitTheKey(collision);
        PlayerHitCoin(collision);
        PlayerHitTarget(collision);
    }

    private void PlayerHitTarget(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            int i = sceneControl.currentLevel;
            int lastLevel = sceneControl.finalLevel;

            if (i < lastLevel)
            {
                Debug.Log("Level " + i + " passed!!");
                sceneControl.ChangeScene(i + 1);
            }
            else
            {
                Debug.Log("Congratulation, All levels are completed, there is no upper level!");
            }
        }
    }

    private void PlayerHitCoin(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            int coinValue = int.Parse(collision.gameObject.name);

            health += coinValue;
            Debug.Log("I got additional health of " + coinValue);

         // Destroy(collision.gameObject); --> removed completely when run the game,,,, unless restart game
         // collision.gameObject.SetActive(false);  --> you cannot find Inactive object with Find function!!!
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void PlayerHitTheKey(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Debug.Log("I got the " + collision.gameObject.name + " key, let's jump!");

            string keyPairName = collision.gameObject.name;
            GameObject[] keyLocks = GameObject.FindGameObjectsWithTag("Lock");

            foreach (GameObject keyLock in keyLocks)
            {
                if (keyLock.name == keyPairName)
                {
                    keyPair = keyLock;
                }
            }

            transform.position = keyPair.transform.position;
         // Destroy(collision.gameObject);
         // collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            goingDown = false;
            launched = false;

        }
    }

    private void PlayerHitEnemy(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = myDoor.transform.position;
            myDoor.GetComponent<SpriteRenderer>().sprite = openDoor;

            ResetBars();

            goingDown = false;
            launched = false;
        }
    }

    private void PlayerHitObstacle(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(collision.gameObject.GetComponent<Obstacle>().damage);
            launched = false;

            InstantiateParticle();
        }
    }

    private void PlayerHitBase(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base") && goingDown == true)
        {
            goingDown = false;
            launched = false;
        }
    }

    private void PlayerHitBar(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bar") && goingDown == true)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            damage = Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y) * (highPoint - transform.position.y));

            TakeDamage(damage);
            goingDown = false;
            launched = false;

            // Kotitehtävä (saa yrittää)
            // Korjaa, että damage ei voi olla NaN. Estä, että pelaaja ei voi ladata 
            // forcea ilmassa ja siten hypätä. => launched true
        }
    }

    private void InstantiateParticle()
    {
      // GameObject effect = 
          Instantiate(obstacleParticle, transform.position, Quaternion.identity);
    
    //    effectParticle.transform.position = this.transform.position;
    //    effectParticle.Play();
    }

    void Launch(float forceAmount, Vector3 dir)
    {
        playerRB.AddForce(dir * forceAmount, ForceMode2D.Impulse);
        goingDown = false;
    }

    void TakeDamage(float dmg)
    {
        health -= dmg; 
        if(health <0)
        {
            health = 0;
            Die();
            Debug.Log("You failed, start again");
        }
    }

    private void Die()
    {
        transform.position = new Vector3(0, 1.5f, 0);
        health = 100;
        force = 0;
        goingDown = false;
        launched = false;

        ResetBars();
        ResetKeys();
        ResetCoins();
    }

    private static void ResetCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
 //         coin.SetActive(true);
            coin.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private static void ResetKeys()
    {
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        foreach (GameObject key in keys)
        {
 //         key.SetActive(true); ==> YOU Cannot Find the Inactive Object with Find
            key.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private static void ResetBars()
    {
        GameObject[] bars = GameObject.FindGameObjectsWithTag("Bar");
        foreach (GameObject bar in bars)
        {
            bar.GetComponent<BoxCollider2D>().isTrigger = true;
            bar.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    //IEnumerator ResetDoor()
    //{
    //    myDoor.GetComponent<SpriteRenderer>().sprite = openDoor;
    //    yield return new WaitForSeconds(seconds: 2);
    //    myDoor.GetComponent<SpriteRenderer>().sprite = closedDoor;
    //}

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Force: " + force, myGUIStyle);
        GUI.Label(new Rect(10, 30, 100, 20), "Launched: " + launched, myGUIStyle);
        GUI.Label(new Rect(10, 50, 100, 20), "Going Down: " + goingDown, myGUIStyle);
        GUI.Label(new Rect(10, 70, 100, 20), "High Point: " + highPoint, myGUIStyle);
        GUI.Label(new Rect(10, 90, 100, 20), "Damage: " + damage, myGUIStyle);
        GUI.Label(new Rect(10, 110, 100, 20), "Health: " + health, myGUIStyle);
    }

    // texxture.com
}
