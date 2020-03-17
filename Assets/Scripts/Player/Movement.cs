using UnityEngine;
using UnityEngine.InputSystem;

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

        private float _gravity = 20;
        //Struct (mutiple type array)

        private Vector3 moveDir;
        //Ref
        private CharacterController charC;

        private void Start()
        {
            charC = GetComponent<CharacterController>();
        }


        private void Update()
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);

            Move();
        }


        public void Fire(InputAction.CallbackContext context)
        {
            Debug.Log("Pew Pew");
        }


        private void Move()
        {

            //if (!PlayerHandler.isDead)  // If play is not dead allow directional movement
            //{ 
                if(charC.isGrounded)
                {
                    //set speed
                    

                    if (Input.GetButton("Sprint"))
                    {
                        moveSpeed = runSpeed;
                        anim.SetBool("Run", true);
                    }
                    else if (Input.GetButton("Crouch"))
                    {
                        moveSpeed = crouchSpeed;
                    }
                    else if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f)
                    {
                        moveSpeed = walkSpeed;
                        anim.SetBool("Walk", true);
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
            moveDir.y -= _gravity * Time.deltaTime;
            //apply movement
            charC.Move(moveDir * Time.deltaTime);
        }


    }
}
