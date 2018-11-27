using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    public float movingSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * movingSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            movingSpeed *= -1;
        }
    }


    public class AnotherMethod : MonoBehaviour
    {
        private Vector3 pos1 = new Vector3(-4, 0, 0);
        private Vector3 pos2 = new Vector3(4, 0, 0);
        public float speed = 1.0f;

        void Update()
        {
            transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
        }
    }




}