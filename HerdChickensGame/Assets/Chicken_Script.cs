using System.Collections;
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

        /*
         * begins the action of the chickens bouncing around everywhere
         */

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * keep track of the location of the chicken, ensure it never
         * goes out of bounds, if it gets too far play an animation where it
         * jumps closer to the center
         * 
         * 
         * keep track of the time, if the clock exceeded a certain
         * amount of time (maybe like 2 minutes?), then all of the chickens will
         * enter the henhouse again because an animation of a fox will enter
         * the screen and scare them back in, thus ending the game
         */
    }

    /*
     * need to alter this method so that it's not that it gets triggered by the 
     * ramp but that it gets triggered by being on the correct "track" because of the
     * 2D nature of the movement of the player
     */
    private void OnTriggerEnter(Collider other)
    {
        //random number generator for int numbers 1 (inclusive) to 10 (inclusive) for the jump/enter (10 numbers)
        int rand_enter_num = Random.Range(1, 10);

        //random number generator for float numbers for speed of walk (0.0 to 1.0f ??)
        //float rand_speed = Random.Range(0.0f, 2.0f);

        if (other.gameObject.name == "Ramp")
        {
            if(rand_enter_num < 3) { //30% of the time FIXME:
                //chicken will jump off
                //anim["Jump_Test"].speed = 0.5f;
                anim.Play("Walk_Then_Jump");
            } else { //70% of the time
                //chicken will enter henhouse
                //anim["Walk_Up_Ramp].speed = rand_speed;
                anim.Play("Walk_Into_Henhouse");
                Destroy(this.gameObject, 1.7f); //have to alter destroy time based on rand_speed
                
                //play an animation of the girl jumping up and down once
                //to signal she's happy
            }
        }
    }
}
