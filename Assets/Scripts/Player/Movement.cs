using UnityEngine;

namespace FPS.Player
{ 
    
    [RequireComponent(typeof(CharacterController))]
    
    public class Movement : MonoBehaviour
    {

        [Header("Speed Variables")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;

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
            Move();
        }


        private void Move()
        {

            if (!PlayerHandler.isDead)  // If play is not dead allow directional movement
            { 
                if(charC.isGrounded)
                {
                    //set speed

                    if (Input.GetButton("Sprint"))
                    {
                        moveSpeed = runSpeed;
                    }
                    else if (Input.GetButton("Crouch"))
                    {
                        moveSpeed = crouchSpeed;
                    }
                    else
                    {
                        moveSpeed = walkSpeed;
                    }
                
                    //calculate moement direction based off inputs
                    moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);

                    if(Input.GetButton("Jump"))
                    {
                        moveDir.y = jumpSpeed;
                    }
                }
            }

            if(PlayerHandler.isDead)
            {
                moveDir = Vector3.zero;
            }
            //Regardless if we are grounded
            //apply gravity
            moveDir.y -= _gravity * Time.deltaTime;
            //apply movement
            charC.Move(moveDir * Time.deltaTime);
        }


    }
}
