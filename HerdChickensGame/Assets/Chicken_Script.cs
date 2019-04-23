﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Script : MonoBehaviour
{
    private Animation anim;
    private Rigidbody chicken;
    //used later to restrict track jumps
    public int track_two_jump;
    public float start_time;
    public float total_time;

    // Start is called before the first frame update
    void Start()
    { 
        chicken = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animation>();
        track_two_jump = 0;

        chicken.useGravity = true;
        chicken.isKinematic = false;

        //start_time = Time.time;

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

        Vector3 start_position = new Vector3(-4.5f, 2.5f, -5.0f); //FIXME: made it always start track 2
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

        //track one, so it's always moving towards the ramp
        if (chicken.position.z == -3.0f)
        {
            chicken.AddForce(0.3f, 0.0f, 0.0f);
        }

        //track two
        else if (chicken.position.z == -5.0f) {
            if (chicken.position.x > 5.0f)
            {
                chicken.AddForce(-0.3f, 0.0f, 0.0f);
            }

            if (chicken.position.x < -5.5f)
            {
                chicken.AddForce(0.3f, 0.0f, 0.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Until the player is moved by the kinect, the a set animation plays on the chickens to make
         * them seem startled and jumping around, but they will never enter the henhouse until
         * the player starts moving around
         * 
         * On that trigger, set the start time to the time, so the elapsed time is time - start time, 
         * if that exceeds 3 minutes play the fox animation
         */ 
        /*
         * if two minutes have passed and the chickens are still not in the henhouse, a 
         * fox comes from the left of the screen and scares them back into the henhouse
         * 
         * this action would essesntially stop gameplay and freeze the position of the
         * player as the chickens make their way back into the henhouse
         */
        if(Time.time > 180.0) //times out in two minutes
        {

        }

        //if chicken goes too far on the right of the screen and it's on track two
        if (chicken.position.x > 4.8f && chicken.position.z == -5.0f)
        {
            anim.Play("JL_T2_to_T1");
        }
        //if chicken goes too far on the left of the screen
        else if(chicken.position.x < -7.5f)
        {
            int switch_num = Random.Range(0, 2);
            
            //chicken currently on track one
            if (chicken.position.z == -3.0f)
            {
                if (switch_num < 2) //66% of the time
                {
                    //stay on track one
                    anim.Play("JR_T1_to_T1");
                }
                else //33% of the time
                {
                    //switch to track two
                    anim.Play("JR_T1_to_T2");
                }
            }

            //chicken currently on track two
            else if (chicken.position.z == -5.0f)
            {
                if (switch_num < 2) //66% of the time
                {
                    //switch to track one
                    anim.Play("JR_T2_to_T1");
                }
                else //33% of the time
                {
                    //stay on track two
                    anim.Play("JR_T2_to_T2");
                }
            }
        }

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

        /*
        //ensures the chicken is in the correct "jump zone" x range and is currently at the bottom of a bounce (y is small)
        if (chicken.position.x > -1.0f && chicken.position.x < 1.0f && chicken.position.y < 1.0f) {
            int jump_condition = Random.Range(1, 10); //random number used to determine whether a chicken jumps or not
            float cur_x = chicken.position.x; //the chickens current x position doesn't change

            //the chicken is currently on track one
            Debug.Log(track_two_jump);
            if(chicken.position.z == -3.0f && track_two_jump < 1 && jump_condition <= 1) { //jumps to track two 10% of the time
                //jump to track two
                anim.Play("Jump_To_Track_Two"); //animation should end in the air somewhat to allow for bounce
                Vector3 new_position = new Vector3(cur_x, 2.5f, -5.0f);
                chicken.transform.position = new_position;
                track_two_jump++; 
            }
            //the chicken is currently on track two
            else if(chicken.position.z == -5.0f && jump_condition <= 7) { //jumps to track one 70% of the time
                //jump to track one
                anim.Play("Jump_To_Track_One"); //animation should end in the air somewhat to allow for bounce
                Vector3 new_position = new Vector3(cur_x, 2.5f, -3.0f);
                chicken.transform.position = new_position;
            }
        }
        */
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collides");
        /*
         * if the user pushes the chicken too far to the right, it jumps high over
         * the user to the left
         */
         /*
        if(chicken.position.x > 5.0f  && collision.gameObject.name == "Player")
        {
            anim.Play("Jump_To_The_Left");
        }

        /*
         * if the user pushes the chicken too far to the left, it jumps high over
         * the user to the right
         
        else if(chicken.position.x < -5.5f && collision.gameObject.name == "Player")
        {
            anim.Play("Jump_To_The_Right");
        }
        */
       
        
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
                Debug.Log("destroyed!");
                Destroy(this.gameObject, 1.7f); //have to alter destroy time based on rand_speed
                
                //play an animation of the girl jumping up and down once
                //to signal she's happy
            }
        }
    }
}
