using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Script : MonoBehaviour
{
    public Animation anim;
    public Animation girl_anim;

    public Rigidbody chicken;
    public Rigidbody player;
    public GameObject girl;

    public int num_chickens;

    public Player_Script player_script;
    public Girl_Script girl_script;


    // Start is called before the first frame update
    void Start()
    {
        player_script = player.GetComponent<Player_Script>(); //reference to the script for the player
        girl_script = girl.GetComponent<Girl_Script>(); //reference to the script for the girl

        chicken = GetComponent<Rigidbody>(); //initializes this chicken object
        anim = gameObject.GetComponent<Animation>(); //initializes the animation controller for the chickens
        girl_anim = girl.GetComponent<Animation>(); //initializes the animation controller for the girl

        num_chickens = 15; //scene starts with fifteen chickens

        chicken.useGravity = true;
        chicken.isKinematic = false;
        //chicken.mass = ??

        //randomly chooses Y position
        float rand_y = Random.Range(0.5f, 3.5f);

        //initializes the start position to all zeroes before being replaced
        Vector3 start_position = new Vector3(0.0f, 0.0f, 0.0f);

        //each chicken is assigned a unique starting location based on it's name
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
            start_position = new Vector3(-4.1f, rand_y, -3.0f);
        }
        else if (this.name == "Chicken_9")
        {
            start_position = new Vector3(0.3f, rand_y, -5.0f);
        }
        else if (this.name == "Chicken_10")
        {
            start_position = new Vector3(-2.0f, rand_y, -3.0f);
        }
        else if (this.name == "Chicken_11")
        {
            start_position = new Vector3(2.1f, rand_y, -5.0f);
        }
        else if (this.name == "Chicken_12")
        {
            start_position = new Vector3(3.0f, rand_y, -5.0f);
        }
        else if (this.name == "Chicken_13")
        {
            start_position = new Vector3(1.2f, rand_y, -3.0f);
        }
        else if (this.name == "Chicken_14")
        {
            start_position = new Vector3(-4.8f, rand_y, -3.0f);
        }

        //sets this chicken's position to the chosen start position
        chicken.transform.position = start_position;

        //forces chicken to start on "track" in front of the henhouse
        chicken.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * there are always forces acting on the chickens to subtly move them across the screen, if
         * the chicken's position gets too far to the right, the force moves it to the left, and vice
         * versa; these forces are only enacted after the player has started moving, to ensure that
         * chickens won't climb the ramp or move across the screen until gameplay has begun. this 
         * gives the actors time to explain the scene while also providing a visual of the bouncing
         * chickens.
         */
        if (!player.IsSleeping())
        {
            //chicken is currently on track one
            if (chicken.position.z == -3.0f)
            {
                //chicken is always moving to the right
                chicken.AddForce(0.3f, 0.0f, 0.0f);
            }

            //chicken is currently on track two
            else if (chicken.position.z == -5.0f)
            {
                //if chicken gets too far to the right
                if (chicken.position.x > 5.0f)
                {
                    //chicken is always moving to the left
                    chicken.AddForce(-0.3f, 0.0f, 0.0f);
                }

                //if chicken gets too far to the left
                if (chicken.position.x < -5.5f)
                {
                    //chicken is always moving to the right
                    chicken.AddForce(0.3f, 0.0f, 0.0f);
                }
            }
        }

        /*
         * if two minutes have passed and the chickens are still not in the henhouse, a 
         * fox comes from the left of the screen and scares them back into the henhouse
         * 
         * this action would essesntially stop gameplay and freeze the position of the
         * player as the chickens make their way back into the henhouse
         */
        if (Time.time > 180.0)
        {
            /* TODO:
             * 
             * add code for the fox appearing from the side, disabling user motion, and 
             * forcing the chickens to go into the henhouse
             */ 
        }

        /*
         * chickens move between track one and track two if they are pushed too far to one side, 
         * and only when they are on track one are they aligned properly to move into the henhouse
         * 
         * from the view of the player, the tracks are the same, since their view is "2D", 
         * so it simply appears that the chickens sometimes are pushed right past the ramp
         * and sometimes they walk up it
         * 
         * track one: along the line z = -3.0f
         * track two: along the line z = -5.0f
         */

        //if chicken goes too far on the right of the screen and it's on track two
        if (chicken.position.x > 4.8f && chicken.position.z == -5.0f)
        {
            //jumps to the left onto track one
            anim.Play("JL_T2_to_T1");
        }

        //if chicken goes too far on the left of the screen
        else if(chicken.position.x < -7.5f)
        {
            //random number generator for int numbers 0 (inclusive) to 1 (inclusive) for deciding which track
            int switch_num = Random.Range(0, 1);
            
            //if chicken is currently on track one
            if (chicken.position.z == -3.0f)
            {
                //50% of the time, chicken stays on track one
                if (switch_num == 0)
                {
                    anim.Play("JR_T1_to_T1");
                }
                //50% of the time, chicken switches to track two
                else
                {
                    anim.Play("JR_T1_to_T2");
                }
            }

            //if chicken is currently on track two
            else if (chicken.position.z == -5.0f)
            {
                //50% of the time, chicken switches to track one
                if (switch_num == 0)
                {
                    anim.Play("JR_T2_to_T1");
                }
                //50% of the time, chicken stays on track two
                else
                {
                    anim.Play("JR_T2_to_T2");
                }
            }
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        /*
         * if the ball touches the ramp, it will either enter the henhouse or it will jump off the side, 
         * based on the specified condition
         */ 
        if (other.gameObject.name == "Ramp")
        {
            //random number generator for int numbers 1 (inclusive) to 10 (inclusive) for the jump/enter (10 numbers)
            int rand_enter_num = Random.Range(1, 10);

            //30% of the time, chicken will jump off (if there's more than two chickens left)
            if (rand_enter_num <= 3 && num_chickens > 2)
            {
                anim.Play("Walk_Then_Jump");
            }
            //70% of the time, chicken will enter henhouse
            else
            {
                //plays animation and then destroys the chicken after the animation is done
                anim.Play("Walk_Into_Henhouse");
                Destroy(this.gameObject, 1.7f);
                
                //decrement the number of chickens
                num_chickens--;

                //girl jumps in background unless the animation of her jumping is already currently playing
                girl_anim["Girl_Happy_Jump"].speed = 2.5f; 
                if (!girl_anim.IsPlaying("Girl_Happy_Jump"))
                {
                    girl_anim.Play("Girl_Happy_Jump");
                }
            }
        }
    }
}
