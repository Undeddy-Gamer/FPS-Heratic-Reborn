using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Player
{ 
    
    [RequireComponent(typeof(CharacterController))]
    
    public class Movement : MonoBehaviour
    {

        [Header("Speed Variables")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;

        [Header("References")]
        public Animator anim; // player model animator

        // the gravity 'force' to apply to y axis when the player is in the air (eg after jumping)
        private float gravity = 20;
        
        // vector 3 move direction
        private Vector3 moveDir;
        
        //Ref to character controller
        private CharacterController charC;

        private void Start()
        {
            charC = GetComponent<CharacterController>();
        }


        private void Update()
        {           
            Move();
        }


        
        private void Move()
        {

            //if (!PlayerHandler.isDead)  // If play is not dead allow directional movement
            //{ 
                if(charC.isGrounded)
                {

                    //set speed and animation


                    if (Input.GetButton("Sprint") && (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f))
                    {
                        moveSpeed = runSpeed;
                        anim.SetBool("Run", true);
                    }
                    else if (Input.GetButton("Crouch"))
                    {
                        moveSpeed = crouchSpeed;
                        anim.SetBool("Crouch", true);
                    }
                    else if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f)
                    {
                        moveSpeed = walkSpeed;
                        anim.SetBool("Walk", true);
                    }
                    else if (Input.GetAxis("Horizontal") < 0f || Input.GetAxis("Vertical") < 0f)
                    {
                        moveSpeed = walkSpeed;
                        anim.SetBool("WalkBack", true);
                    }
                    else
                    {
                        anim.SetBool("Walk", false);
                        anim.SetBool("Run", false);
                        anim.SetBool("Crouch", false);
                    }
                
                    //calculate moement direction based off inputs
                    moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);

                    if(Input.GetButton("Jump"))
                    {
                        moveDir.y = jumpSpeed;
                        anim.SetTrigger("Jump");
                    }
                }
            //}

            //if(PlayerHandler.isDead)
            //{
            //    moveDir = Vector3.zero;
            //}


            //Regardless if we are grounded
            //apply gravity
            moveDir.y -= gravity * Time.deltaTime;
            //apply movement
            charC.Move(moveDir * Time.deltaTime);
        }


    }
}
