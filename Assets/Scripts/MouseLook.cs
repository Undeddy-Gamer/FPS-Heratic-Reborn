using UnityEngine;


public class MouseLook : MonoBehaviour
{
    // List of 'types' in this case mouse axis direction X/Y
    public enum RotationalAxis
    {
        MouseX,
        MouseY
    }


    // Header for the component menu in unity
    [Header("Rotation Variables")]
    public RotationalAxis axis = RotationalAxis.MouseX; //The axis of player view using the enum created above

    [Range(0, 120)] // set a max/min range for next variable which also creates a slider 
    public float sensitivity = 40; //Sensitivity of the player view
    public float minY = -60, maxY = 60;  // Max lookup/down 
    private float _rotY; // Rotaion of the Y axis (left right)


    private void Start()
    {
        
        //If the object is a rigid body (the player)
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true; // freeze the rigid body rotation so it doesn't muk up our mouse look code
        }
        Cursor.lockState = CursorLockMode.Locked; // lock the mouse cursor
        Cursor.visible = false; // hide the mouse cursor


        if (GetComponent<Camera>()) // if the object is a camera
        {
            axis = RotationalAxis.MouseY; // look up and down
        }


    }

    private void Update()
    {        
        RotateAxis();        
    }

    private void RotateAxis()
    {
        if (!GameManager.Instance.inGameMenuOpen)
        { 
            if (!PlayerHandler.isDead)  //If player is not dead allow 
            {
                // If axis movement is X (the player left and rght)
                if (axis == RotationalAxis.MouseX)
                {
                    //Change rotation of X Axis
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity* Time.deltaTime, 0);
                }
                else // otherwise axis movement is Y (up/down look)
                {
                    _rotY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
                    _rotY = Mathf.Clamp(_rotY, minY, maxY);
                    transform.localEulerAngles = new Vector3(_rotY, 0, 0);
                }
            }
        }
    }
}


