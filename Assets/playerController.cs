using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    Rigidbody playerRigid;
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal"); // get the left and right movement
        float verticalAxis = Input.GetAxis("Vertical"); // get the left and right movement

        Vector3 movement = new Vector3(-horizontalAxis, 0, 0);
    }
}
