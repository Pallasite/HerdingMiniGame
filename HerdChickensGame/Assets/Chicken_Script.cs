﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Script : MonoBehaviour
{
    private Animation anim;
    private Rigidbody chicken;

    // Start is called before the first frame update
    void Start()
    { 
        chicken = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animation>();

        chicken.useGravity = true;
        chicken.isKinematic = false;
        
        //maybe randomize this a little bit later?
        //Vector3 start_position = new Vector3(7.5f, 0.25f, -7.5f);
        //this.gameObject.transform.position = start_position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Ramp")
        {
            //anim.Play("Walk_Up_Ramp");
            Debug.Log("climbing");
            anim.Play("Walk_Up_Ramp");

            Destroy(this.gameObject, 0.75f);
            Debug.Log("this collider: " + other.gameObject.name);
        }
    }
}