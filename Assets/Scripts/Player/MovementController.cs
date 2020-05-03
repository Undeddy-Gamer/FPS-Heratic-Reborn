using UnityEngine;



[RequireComponent(typeof(Rigidbody))]

public class MovementController : MonoBehaviour
{

    [Header("Speed Variables")]
    public float moveSpeed;
    public float walkSpeed, runSpeed, crouchSpeed, jumpStrength;

    [Header("References")]
    public Animator anim; // player model animator

    // the gravity 'force' to apply to y axis when the player is in the air (eg after jumping)
    private float gravity = 20;

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
        //Move();
        MoveV2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
    /// Apply movement to the player character
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


            if (!PlayerHandler.isDead)  // If play is not dead allow directional movement
            {
                //set speed and animation
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
                moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);

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
                if (Input.GetButtonDown("Jump") && IsGrounded())
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
            Debug.Log("UNITY ERROR\n" + e);
            // Handle error
        }
    }

}




