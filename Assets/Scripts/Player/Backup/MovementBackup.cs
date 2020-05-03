using UnityEngine;

namespace FPS.Player
{ 
    
    [RequireComponent(typeof(CharacterController))]
    
    public class MovementBackup : MonoBehaviour
    {

        [Header("Speed Variables")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;

        [Header("References")]
        public Animator animator; // player model animator

        // the gravity 'force' to apply to y axis when the player is in the air (eg after jumping)
        private float gravity = 20;
        
        // vector 3 move direction
        /// <value>Move Direction (combination of forward, sideways and fall/jump direction)</value>
        private Vector3 moveDir;
        
        //Ref to character controller
        private CharacterController charC;

        

        private void Start()
        {
            charC = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }


        private void Update()
        {           
            //Move();

            if (animator == null)
            {
                return;
            }

            Move();
        }

        /// <summary>
        /// Apply movement to the character
        /// </summary>
        /// <param name="horizontalAxis">Amount to move forward</param>
        /// <param name="verticalAxis">Amount to move left/right (positive to move right, negative to move left)</param>
        /// <remarks>
        ///     <para>Jump is also applied within the function</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// FPS.Player.Move(1,-1);
        /// </code>
        /// </example>
        /// <exception cref="System.OverflowException">Throws an overflow error</exception>
        /// See <see cref="Move()"/> for same function which gets axis within function"
        void MoveV2()
        {
            
            var forward = Input.GetAxis("Horizontal");
            var sideways = Input.GetAxis("Vertical");

            var run = Input.GetButton("Sprint");

            if (!run)
            {
                forward = Mathf.Clamp(forward, -0.5f, -0.5f);
            }

                //set the movement animation
                animator.SetFloat("VelocityX", forward);
                animator.SetFloat("VelocityY", sideways);


                //Set the actual movement speed of the player
                if(Input.GetButton("Sprint") && (forward > 0f))
                {
                    moveSpeed = runSpeed;
                    
                }
                else if (Input.GetButton("Crouch"))
                {
                    moveSpeed = crouchSpeed;                   
                }
                else if (forward != 0f || sideways != 0f)
                {
                    moveSpeed = walkSpeed;                    
                }

                if (Input.GetButtonDown("Jump"))
                {
                    moveDir.y = jumpSpeed;
                    animator.SetTrigger("Jump");
                }

                //calculate moement direction based off inputs
                moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);

            

            moveDir.y -= gravity * Time.deltaTime;
            //apply movement
            charC.Move(moveDir * Time.deltaTime);

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
                        animator.SetBool("Run", true);
                    }
                    else if (Input.GetButton("Crouch"))
                    {
                        moveSpeed = crouchSpeed;
                    animator.SetBool("Crouch", true);
                    }
                    else if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f)
                    {
                        moveSpeed = walkSpeed;
                    animator.SetBool("Walk", true);
                    }
                    else if (Input.GetAxis("Horizontal") < 0f || Input.GetAxis("Vertical") < 0f)
                    {
                        moveSpeed = walkSpeed;
                    animator.SetBool("WalkBack", true);
                    }
                    else
                    {
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
                    animator.SetBool("Crouch", false);
                    }

                if (Input.GetButtonDown("Jump"))
                {
                    moveDir.y = jumpSpeed;
                    animator.SetTrigger("Jump");
                }

                //calculate moement direction based off inputs
                moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);

                    
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
