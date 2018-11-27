using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {


    public float damage;
    public float rotationSpeed;

    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    //   transform.Rotate(transform.rotation.x, transform.rotation.y, rotationSpeed * Time.deltaTime );
       // transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); my code
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));  

        // homework -tehty
        //try to create obstacles that constantly rotate at some speed
        //    create obstacle that rotate different speed
        //    create a variable rotateSpeed
        //    use transform.Rotate, Time.deltaTime
    }

    public float ReturnDamageValue()
    {
        return damage;
    }


}
// homework -tehty
//// create emplty gameobject and put 3 obstacle as child to the gameobject 
//take 3 child obstacles to 
//    put s script to empty gameobject that rotates so all 3 obstacles rotate together too




    // homework3

   //  Create at least 3 different levels for this game. Different difficulties
   // Put to the top of the level and object. when player hits the object
   // Debug.Log("Level passed") 
