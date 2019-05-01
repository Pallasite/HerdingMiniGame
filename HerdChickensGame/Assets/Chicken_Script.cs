using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chicken_Script : MonoBehaviour
{
    public Animation anim;
    public Animator animator;
    public Animation girl_anim;

    public Rigidbody chicken;
    public Rigidbody player;
    public GameObject girl;
    public Collider chicken_collider;

    public float max_velocity;
    public bool has_collided_with_player;
    public int num_switches;
    public bool has_tried_to_enter;
    public bool has_been_dizzy;

    public Player_Script player_script;
    public Girl_Script girl_script;
    public Game_Runner game_runner_script;
    
    // Start is called before the first frame update
    void Start()
    {
        player_script = player.GetComponent<Player_Script>(); //reference to the script for the player
        girl_script = girl.GetComponent<Girl_Script>(); //reference to the script for the girl


        chicken = GetComponent<Rigidbody>(); //initializes this chicken object
        anim = gameObject.GetComponent<Animation>(); //initializes the animation controller for the chickens
        girl_anim = girl.GetComponent<Animation>(); //initializes the animation controller for the girl
        game_runner_script = FindObjectOfType<Game_Runner>();
        animator = gameObject.GetComponent<Animator>();
        chicken_collider = chicken.GetComponent<Collider>();

        chicken.useGravity = true;
        chicken.isKinematic = false;

        max_velocity = 10.0f;
        has_collided_with_player = false;
        has_tried_to_enter = false;
        num_switches = 0;
        has_been_dizzy = false;

        //chicken.mass = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (chicken.velocity.magnitude > max_velocity)
        {
            chicken.velocity = max_velocity * chicken.velocity.normalized;
        }

        if (chicken.position.x > 2.0 && chicken.position.z == -3.0f)
        {
            ResetAnimations();
            animator.SetBool("faceright_bool", true);
        }

        else
        {
            if(animator.GetBool("faceright_bool") == true)
            {
                ResetAnimations();
                animator.SetBool("faceforward_bool", true);
                animator.SetBool("flyagain_bool", true);
            }
        }

        /*
         * there are always forces acting on the chickens to subtly move them across the screen, if
         * the chicken's position gets too far to the right, the force moves it to the left, and vice
         * versa; these forces are only enacted after the player has started moving, to ensure that
         * chickens won't climb the ramp or move across the screen until gameplay has begun. this 
         * gives the actors time to explain the scene while also providing a visual of the bouncing
         * chickens.
         */

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

        if (chicken.position.x > 6.5f || chicken.position.x < -7.5f)
        {

            //if chicken goes too far on the right of the screen and it's on track two
            if (chicken.position.x > 6.5f && chicken.position.z == -5.0f)
            {
                chicken.AddForce(-300.0f, 500.0f, 0.0f);
            }

            //if chicken goes too far on the left of the screen
            else if (chicken.position.x < -7.5f)
            {
                //if chicken is currently on track one
                if (chicken.position.z == -3.0f)
                {
                    chicken.AddForce(100.0f, 300.0f, 0.0f);
                }

                //if chicken is currently on track two
                else if (chicken.position.z == -5.0f)
                {
                    chicken.AddForce(100.0f, 300.0f, 0.0f);
                }
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            has_collided_with_player = true;
            int rand = RandomNum();

            if (!animator.GetBool("faceright_bool") && !animator.GetBool("faceforward_bool"))
            {
                ResetAnimations();
                if (rand <= 5)
                {
                    animator.SetBool("shout_bool", true);
                }
                else
                {
                    animator.SetBool("panic_bool", true);
                }
            }

            chicken.AddForce(2500.0f, 1000.0f, 0.0f);
        }

        if (collision.gameObject.name == "Grass")
        {
            int rand = RandomNum();

            ResetAnimations();
            if (rand <= 5)
            {
                animator.SetBool("pokpok_bool", true);
            }
            else
            {
                animator.SetBool("cheer_bool", true);
            }

            //makes chicken bounce in the air
            if (rand <= 5)
            {
                chicken.AddForce(500.0f, 1000.0f, 0.0f);
            }
            else
            {
                chicken.AddForce(-500.0f, 1000.0f, 0.0f);
            }
       
            //random number generator for int numbers 0 (inclusive) to 99 (inclusive) for deciding which track
            int switch_num = Random.Range(0, 99);

            //variables for the current position
            float cur_x = chicken.position.x;
            float cur_y = chicken.position.y;
            float cur_z = chicken.position.z;

            //go to track one
            if (cur_z == -5.0f && switch_num <= 50 && chicken.position.x <= 3.0f) //3.0 is where the ramp starts
            {
                chicken.transform.position = new Vector3(cur_x, cur_y, -3.0f);
                num_switches++;
            }

            //go to track two
            else if (game_runner_script.Get_Num_Chickens() >= 5 && cur_z == -3.0f && num_switches <= 15)
            {
                chicken.transform.position = new Vector3(cur_x, cur_y, -5.0f);
                num_switches++;
            }
        }

        /*
         * if the ball touches the ramp, it will either enter the henhouse or it will jump off the side, 
         * based on the specified condition
         */
        if (collision.gameObject.name == "Ramp")
        {
            //random number generator for int numbers 1 (inclusive) to 10 (inclusive) for the jump/enter (10 numbers)
            int rand_enter_num = Random.Range(1, 10);

            //60% of the time, chicken will jump towards the henhouse
            if (has_tried_to_enter || rand_enter_num <= 6)
            {
                has_tried_to_enter = true;
                //SetEnterHenhouseRotation();
                chicken.AddForce(1800.0f, 750.0f, 0.0f);
            }

            //40% of the time, chicken will jump away from the henhouse
            else
            {
                chicken.AddForce(-800.0f, 200.0f, 0.0f);
            }
        }

        if (collision.gameObject.name == "Henhouse" && OnTrackOne())
        {
            if(has_collided_with_player) {
                Destroy(this.gameObject);
                game_runner_script.Decrement_Num_Chickens();
            }

            //girl jumps in background unless the animation of her jumping is already currently playing
            girl_anim["Girl_Happy_Jump"].speed = 2.5f;
            if (!girl_anim.IsPlaying("Girl_Happy_Jump"))
            {
                girl_anim.Play("Girl_Happy_Jump");
            }
        }
    }
    public void SetEnterHenhouseRotation()
    {
        float rotation_num = 0.0f;
        float cur_x = chicken.position.x;
        float cur_z = chicken.position.z;

        if(chicken.rotation.y == 0.0f)
        {
            rotation_num = 90.0f;
        } 
        else if(chicken.rotation.y == 90.0f)
        {
            rotation_num = 0.0f;
        } 
        else if(chicken.rotation.y == 180.0f)
        {
            rotation_num = -90.0f;
        } 
        else if(chicken.rotation.y == 270.0f)
        {
            rotation_num = -180.0f;
        }

        chicken.transform.Rotate(cur_x, rotation_num, cur_z);
        return;
    }

    public int RandomNum() //1 through 10
    {
        int rand = Random.Range(1, 10);
        return rand;
    }

    public void ResetAnimations()
    {
        animator.SetBool("shout_bool", false);
        animator.SetBool("panic_bool", false);
        animator.SetBool("cheer_bool", false);
        animator.SetBool("pokpok_bool", false);
        animator.SetBool("gethit_bool", false);
        animator.SetBool("dizzy_bool", false);
        animator.SetBool("faceright_bool", false);
        animator.SetBool("faceforward_bool", false);
        animator.SetBool("flyagain_bool", false);

        return;
    }

    public bool OnTrackOne()
    {
        if (chicken.position.z == -3.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool OnTrackTwo()
    {
        if (chicken.position.z == -5.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
