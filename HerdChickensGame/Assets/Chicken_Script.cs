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

        //Randomly decides which track the chicken will start on
        int rand_track = Random.Range(0, 1);
        float track_z;

        if(rand_track == 0) //50% of the time
        {
            //track one is chosen
            track_z = -3.0f;    
        }
        else //50% of the time
        {
            //track two is chosen
            track_z = -5.0f;
        }

        Vector3 start_position = new Vector3(-4.5f, 2.5f, track_z);
        chicken.transform.position = start_position;

        //forces chicken to start on "track" in front of the henhouse
        chicken.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    public void FixedUpdate()
    {
        /*
         * there are always forces acting on the chickens to subtly move them across the screen, if
         * the chicken's position gets too far to the right, the force moves it to the left, and vice
         * versa
         */ 
        if(chicken.position.x > 5.0f)
        {
            chicken.AddForce(-0.3f, 0.0f, 0.0f);
        }

        if (chicken.position.x < -5.5f)
        {
            chicken.AddForce(0.3f, 0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * keep track of the time, if the clock exceeded a certain
         * amount of time (maybe like 2 minutes?), then all of the chickens will
         * enter the henhouse again because an animation of a fox will enter
         * the screen and scare them back in, thus ending the game
         */

        /*
         * chickens randomly move between track one and track two, and only when they
         * are on track one are they aligned properly to move into the henhouse
         * 
         * for the view of the user, the tracks are the same, since their view is "2D", 
         * so it simply appears that the chickens sometimes are pushed right past the ramp
         * and sometimes they walk up it
         * 
         * track one: along the line z = -3
         * track two: along the line z = -5
         */

        //ensures the chicken is in the correct jump zone x range and is currently in a bounce (y is small)
        if (chicken.position.x > -1.5f && chicken.position.x < 1.5f && chicken.position.y < 1.0f) {
            int jump_condition = Random.Range(1, 10);
            float cur_x = chicken.position.x;

            //the chicken is currently on track one
            if(chicken.position.z == -3.0f && jump_condition <= 1) { //jumps to track two 10% of the time
                //jump to track two
                anim.Play("Jump_To_Track_Two"); //animation should end in the air somewhat to allow for bounce
                Vector3 new_position = new Vector3(cur_x, 2.5f, -5.0f);
                chicken.transform.position = new_position;
            }
            //the chicken is currently on track two
            else if(chicken.position.z == -5.0f && jump_condition <= 1) { //jumps to track one 10% of the time
                //jump to track one
                anim.Play("Jump_To_Track_One"); //animation should end in the air somewhat to allow for bounce
                Vector3 new_position = new Vector3(cur_x, 2.5f, -3.0f);
                chicken.transform.position = new_position;
            }
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        /*
         * if the user pushes the chicken too far to the right, it jumps high over
         * the user to the left
         */
        if(chicken.position.x > 5.0f && collision.gameObject.name == "Player")
        {
            //anim.Play("Jump_To_The_Left");
        }

        /*
         * if the user pushes the chicken too far to the left, it jumps high over
         * the user to the right
         */
        else if(chicken.position.x < -5.5f && collision.gameObject.name == "Player")
        {
            //anim.Play("Jump_To_The_Right");
        }
    }

    /*
     * need to alter this method so that it's not that it gets triggered by the 
     * ramp but that it gets triggered by being on the correct "track" because of the
     * 2D nature of the movement of the player
     */
    public void OnTriggerEnter(Collider other)
    {
        //random number generator for int numbers 1 (inclusive) to 10 (inclusive) for the jump/enter (10 numbers)
        int rand_enter_num = Random.Range(1, 10);

        //random number generator for float numbers for speed of walk (0.0 to 1.0f ??)
        //float rand_speed = Random.Range(0.0f, 2.0f);

        if (other.gameObject.name == "Ramp")
        {
            if(rand_enter_num <= 3) { //30% of the time
                //chicken will jump off
                //anim["Jump_Test"].speed = 0.5f;
                anim.Play("Walk_Then_Jump");
                //go back onto track two

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
