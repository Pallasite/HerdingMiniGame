using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Script : MonoBehaviour
{
    private Animator anim;
    private Rigidbody chicken;

    // Start is called before the first frame update
    void Start()
    {
        //maybe randomize this a little bit later?
        Vector3 start_position = new Vector3(7.5f, 0.25f, -7.5f);
        this.gameObject.transform.position = start_position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "Ramp")
        {
            this.anim.enabled = true;
            Destroy(this.gameObject);
        }
    }
    */

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ramp" || collision.gameObject.name == "Henhouse")
        {
            //anim.Play("Climb_Up_Ramp");
            Destroy(this.gameObject);
        }
    }
    */
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ramp")
        {
           anim.Play("Climb_Up_Ramp");
           Destroy(this.gameObject);
        }
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Ramp")
        {
            anim.Play("Climb_Up_Ramp");

            //if () {
            //    Destroy(this.gameObject);
            //}
        }
    }
    */
}
