﻿using UnityEngine;



[RequireComponent(typeof(Rigidbody))]

public class MovementController : MonoBehaviour
{

    [Header("Speed Variables")]
    public float moveSpeed = 5;
    public float walkSpeed = 5, runSpeed = 8, crouchSpeed = 3, jumpStrength = 200;

    [Header("References")]
    public Animator anim; // player model animator

    

    // Vector 3 move direction
    public Vector3 moveDir;

    // Ref to character controller
    //private CharacterController charC;

    // player rigid body
    Rigidbody playerRigid;
    // player collider
    CapsuleCollider playerCollider;
    // ground check layer mask
    [SerializeField]
    LayerMask groundLayerMask;
    // extra height check
    public float extraHeightCheck = 0.1f;

    // weapon animator
    public Animator weaponAnimator;


    private void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
    }


    private void FixedUpdate()
    {
      
        MoveV2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetButtonDown("Jump"));
    }

    private bool IsGrounded()
    {

        //CHANGE TO Capsule/RAYCAST CHECK        

        //Bounds colliderBounds = new Bounds(playerCollider.bounds.center, new Vector3(playerCollider.bounds.size.x, playerCollider.bounds.size.y + extraHeightCheck, playerCollider.bounds.size.z));
        //Debug.Log(colliderBounds.size);
        //Collider[] check = Physics.OverlapCapsule(playerCollider.bounds.center, colliderBounds.size, playerCollider.radius, groundLayerMask);

        //foreach (Collider col in check)
        //{
        //    Debug.Log(col.name);
        //}

        //if (check.Length > 0)            
        //    return true;        
        //else
        //    return false;

        return true;
    }



    /// <summary>
    /// Apply movement and speed to the player character dependent on movement type modifier (applied by input crouch, jump, run, default is walk)
    /// </summary>
    /// <param name="horizontalAxis">Amount to move forward</param>
    /// <param name="verticalAxis">Amount to move left/right (positive to move right, negative to move left)</param>
    /// <remarks>
    ///     <para>Jump is also applied within the function</para>
    /// </remarks>
    /// <example>
    /// <code>
    ///     MoveV2(1,-1);
    /// </code>
    /// </example>
    /// <exception cref="System.OverflowException">Throws an overflow error</exception>    

    public void MoveV2(float xAxis, float zAxis, bool jump)
    {
       
        try
        {
            float forward = zAxis;
            float sideways = xAxis;

            if (forward > 1 || forward < -1)
                throw new System.OverflowException("Forward axis is beoynd its bounds (-1,1)");
            if (sideways > 1 || sideways < -1)
                throw new System.OverflowException("Sidways axis is beoynd its bounds (-1,1)");


            if (!PlayerHandler.isDead)  // If play is not dead allow directional movement
            {
                //set speed and animation dependent on movement type
                if (Input.GetButton("Sprint") && (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f))
                {
                    moveSpeed = runSpeed;
                }                
                else if (Input.GetButton("Crouch"))
                {
                    moveSpeed = crouchSpeed;
                }
                else if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f)
                {
                    moveSpeed = walkSpeed;
                }
                else if (Input.GetAxis("Horizontal") < 0f || Input.GetAxis("Vertical") < 0f)
                {
                    moveSpeed = walkSpeed;
                }
                else
                {
                    moveSpeed = 0;
                    anim.SetBool("Crouch", false);
                }

                //calculate moement direction based off inputs
                moveDir = transform.TransformDirection(new Vector3(xAxis, 0, zAxis) * moveSpeed);

                //apply movement
                playerRigid.MovePosition(transform.position + (moveDir * Time.fixedDeltaTime));

                // if not sprinting set the forward velocity in the animator to half to indicate walking not running
                var run = Input.GetButton("Sprint");
                if (!run)
                {
                    forward = Mathf.Clamp(forward, -0.5f, 0.5f);
                }
                //set the movement animation
                anim.SetFloat("VelocityY", forward);
                anim.SetFloat("VelocityX", sideways);


                // apply jump
                if (jump && IsGrounded())
                {
                    playerRigid.AddForce(new Vector3(0, jumpStrength, 0));
                    anim.SetTrigger("Jump");
                }
            }
            else // player is dead
            {
                moveDir = Vector3.zero;
            }
        }
        catch (UnityException e)
        {
            // Handle error
            Debug.Log("UNITY ERROR\n" + e);            
        }
    }

}




