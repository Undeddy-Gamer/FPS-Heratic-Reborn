using UnityEngine;

namespace FPS.Player
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
        
        // Vector 3 move direction
        public Vector3 moveDir;
        
        // Ref to character controller
        private CharacterController charC;

        // weapon animator
        public Animator weaponAnimator;

        public float horiz;
        public float vert;

        private void Start()
        {
            charC = GetComponent<CharacterController>();
        }


        private void Update()
        {
            //Move();            
            MoveV2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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

        public void MoveV2(float horizontalAxis, float verticalAxis)
        {

            try
            {
                float forward = verticalAxis;
                float sideways = horizontalAxis;

                if (forward > 1 || forward < -1)
                    throw new System.OverflowException("Forward axis is beoynd its bounds (-1,1)");
                if (sideways > 1 || sideways < -1)
                    throw new System.OverflowException("Sidways axis is beoynd its bounds (-1,1)");

                if (charC == null)
                {
                    charC = GetComponent<CharacterController>();
                }

                //if (!PlayerHandler.isDead)  // If play is not dead allow directional movement
                //{ 
                if (charC.isGrounded)
                {

                    //set speed and animation


                    if (Input.GetButton("Sprint") && (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f))
                    {
                        moveSpeed = runSpeed;
                        //anim.SetBool("Run", true);
                    }
                    else if (Input.GetButton("Crouch"))
                    {
                        moveSpeed = crouchSpeed;
                        //anim.SetBool("Crouch", true);
                    }
                    else if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f)
                    {
                        moveSpeed = walkSpeed;
                        //anim.SetBool("Walk", true);
                    }
                    else if (Input.GetAxis("Horizontal") < 0f || Input.GetAxis("Vertical") < 0f)
                    {
                        moveSpeed = walkSpeed;
                        //anim.SetBool("WalkBack", true);
                    }
                    else
                    {
                        //anim.SetBool("Walk", false);
                        // anim.SetBool("Run", false);
                        moveSpeed = 0;
                        anim.SetBool("Crouch", false);
                    }

                    //calculate moement direction based off inputs
                    moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);

                    if (Input.GetButtonDown("Jump"))
                    {
                        moveDir.y = jumpSpeed;
                        anim.SetTrigger("Jump");
                    }
                }


                //if(PlayerHandler.isDead)
                //{
                //    moveDir = Vector3.zero;
                //}
                var run = Input.GetButton("Sprint");

                if (!run)
                {
                    forward = Mathf.Clamp(forward, -0.5f, 0.5f);
                }


                //set the movement animation
                anim.SetFloat("VelocityY", forward);
                anim.SetFloat("VelocityX", sideways);

                //Regardless if we are grounded
                //apply gravity
                moveDir.y -= gravity * Time.deltaTime;
                //apply movement
                charC.Move(moveDir * Time.deltaTime);
            }
            catch(UnityException e)
            {
                Debug.Log("UNITY ERROR\n" + e);
                // Handle error

            }
        }

    }




}
