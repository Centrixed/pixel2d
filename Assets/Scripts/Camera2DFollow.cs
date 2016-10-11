// This script has the camera follow the player around the map along only the x-axis
// the script is also designed to restrict the camera to a set Y-Axis position, as well as stop at the edge of each level.

using System;
using UnityEngine;

public class Camera2DFollow :MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Vector2 previousCamPos;       //Used for locking the camera
    public bool cameraCollide = false;

    // Use this for initialization
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        // Checks whether the camera's x-movement needs to be locked
        lockCamera();

        if(target && cameraCollide == false){
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            destination.y = 0.65f;      //Locks camera's y-axis movement
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }

    //Checks if the camera is colliding with the borders, and locks it's position accordingly
    void lockCamera()
    {
        // If camera is colliding with border, freeze it at the previous position before it was 
        if(cameraCollide == true)
        {
            // If camera hit the right border
            if(gameObject.transform.position.x - previousCamPos.x > 0){
                Debug.Log("Hit right wall");
                
            }
            // If camera hit the left border
            else if(gameObject.transform.position.x - previousCamPos.x < 0) {
                Debug.Log("Hit left wall");

            }
        }
        // Otherwise, save the current position for the next frame
        else if(cameraCollide == false)
        {
            previousCamPos = gameObject.transform.position;
        }
    }

    // When the camera collider triggers against another collider
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "CamBorder")
        {
            cameraCollide = true;
            Debug.Log("Hit the Border");
        }
    }

    // When the camera is done colliding with the current border, set the bool to unfreeze it.
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "CamBorder" && cameraCollide)
        {
            cameraCollide = false;
            Debug.Log("Away from Border");
        }
    }
}

