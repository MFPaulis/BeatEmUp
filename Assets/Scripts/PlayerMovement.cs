using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float movementSpeed;
    public float forceConst = 2.0f;
    public bool isGrounded;
    public Vector3 jump;

    private bool canJump;
    private float dirX, dirZ;

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void FixedUpdate() 
    {
        rb.velocity = new Vector3(dirX, rb.velocity.y, dirZ);
        //if(canJump){
            //canJump = false;
            //rb.AddForce(0, forceConst, 0, ForceMode.Impulse);
        //}
    }

    void OnCollisionStay()
    {
    	canJump = true;
    }

    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * movementSpeed;
        dirZ = Input.GetAxis("Vertical") * movementSpeed;

        if(Input.GetKeyUp(KeyCode.Space) && canJump){
            rb.AddForce(jump * forceConst, ForceMode.Impulse);
            canJump = false;
        }
    }
}

