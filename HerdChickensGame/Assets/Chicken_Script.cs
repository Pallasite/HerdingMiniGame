using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Script : MonoBehaviour
{
    public Animation anim;
    public Rigidbody chicken;
    //used later to restrict track jumps
    public int track_two_jump;
    public float start_time;
    public float total_time;
    public float bounce_time;

    public Rigidbody player;
    public Rigidbody girl;
    public Player_Script player_script;


    // Start is called before the first frame update
    void Start()
    {
        player_script = player.GetComponent<Player_Script>(); //reference to the script for the player
        chicken = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animation>();
        track_two_jump = 0;

        chicken.useGravity = true;
        chicken.isKinematic = false;
        //chicken.mass = 5.0f;

        //Randomly chooses Y position
        float rand_y = Random.Range(0.5f, 3.5f);

        //Initializes the start position to all zeroes before being replaced
        Vector3 start_position = new Vector3(0.0f, 0.0f, 0.0f);

        if (this.name == "Chicken_0")
        {
            start_position = new Vector3(-4.15f, rand_y, -5.0f);
        }
        else if (this.name == "Chicken_1")
        {
            start_position = new Vector3(-3.29f, rand_y, -3.0f);
        }
        else if (this.name == "Chicken_2")
        {
            start_position = new Vector3(-1.8f, rand_y, -5.0f);
        }
        else if (this.name == "Chicken_3")
        {
            start_position = new Vector3(0.0f, rand_y, -3.0f);
        }
        else if (this.name == "Chicken_4")
        {
            start_position = new Vector3(1.3f, rand_y, -5.0f);
        }
        else if (this.name == "Chicken_5")
        {
            start_position = new Vector3(-2.8f, rand_y, -5.0f);
        }
        else if (this.name == "Chicken_6")
        {
            start_position = new Vector3(-0.9f, rand_y, -5.0f);
        }
        else if (this.name == "Chicken_7")
        {
            start_position = new Vector3(-1.2f, rand_y, -3.0f);
        }
        else if (this.name == "Chicken_8")
        {
            start_position = new Vector3(-3.9f, rand_y, -3.0f);
        }
        else if (this.name == "Chicken_9")
        {
            start_position = new Vector3(0.3f, rand_y, -5.0f);
        }

        //sets this chicken's position to the chosen start position
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

        //only have these forces acting if the player is in motion
        if (!player.IsSleeping()) {
            //track one, so it's always moving toward the ramp
            if (chicken.position.z == -3.0f)
            {
                chicken.AddForce(0.4f, 0.0f, 0.0f);
            }

            //track two
            else if (chicken.position.z == -5.0f) {
                if (chicken.position.x > 5.0f)
                {
                    chicken.AddForce(-0.4f, 0.0f, 0.0f);
                }

                if (chicken.position.x < -5.5f)
                {
                    chicken.AddForce(0.4f, 0.0f, 0.0f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * if two minutes have passed and the chickens are still not in the henhouse, a 
         * fox comes from the left of the screen and scares them back into the henhouse
         * 
         * this action would essesntially stop gameplay and freeze the position of the
         * player as the chickens make their way back into the henhouse
         */
        if (Time.time > 180.0) //times out in two minutes
        {

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
    }
    
    public void OnTriggerEnter(Collider other)
    {
        //random number generator for int numbers 1 (inclusive) to 10 (inclusive) for the jump/enter (10 numbers)
        int rand_enter_num = Random.Range(1, 10);

        if (other.gameObject.name == "Ramp")
        {
            if(rand_enter_num <= 3) { //30% of the time
                //chicken will jump off
                anim.Play("Walk_Then_Jump");

            }
            else { //70% of the time
                //chicken will enter henhouse
                anim.Play("Walk_Into_Henhouse");
                Debug.Log("destroyed!");
                Destroy(this.gameObject, 1.7f); //have to alter destroy time based on rand_speed
                //play animation of girl jumping
            }
        }
    }
}
