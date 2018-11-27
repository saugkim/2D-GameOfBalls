using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    public Transform player;
  
   	// Use this for initialization
	void Start () {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.position.y - 0.1f > transform.position.y)
        {
            GetComponent<MeshRenderer>().material.color = Color.cyan;
            //   gameObject.layer = 9; // active  3D
            GetComponent<BoxCollider2D>().isTrigger = false;

        }          
	}

    
}
