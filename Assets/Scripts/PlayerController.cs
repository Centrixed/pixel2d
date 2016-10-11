/*
 * PlayerController.cs is used to take player input and control the movement of the character player
 */ 

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeedX, moveSpeedY;
    private Rigidbody2D playerRigid;

	// Use this for initialization
	void Start () {
        playerRigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        checkMoveInput();
        checkFlip();
	}

    // Checks for user input on each key and sets new velocity
    void checkMoveInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector2 newPos = new Vector2(xInput * moveSpeedX, yInput * moveSpeedY);
        playerRigid.position += newPos * Time.deltaTime;
    }
    
    void checkFlip()
    {
        if(Input.GetAxis("Horizontal") == 0)
        {
            //Don't do anything :)
        }
        else if(Input.GetAxis("Horizontal") > 0f && playerRigid.velocity.x > 0.25f)    //If moving right & at a certain speed, flip right
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(Input.GetAxis("Horizontal") < 0f & playerRigid.velocity.x < -0.25f)    //If moving left & at a certain speed, flip left
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
