/*
 *      ParallaxController.cs manages the different scrolling backgrounds in the "distance"
 */ 

using UnityEngine;
using System.Collections;

public class ParallaxController : MonoBehaviour {

    public Transform[] backgrounds;         // Array of background/foreground that are being parallaxed
    private float[] paraScales;             // The proportion of the Camera's movement to move the backgrounds by
    public float smoothing;                 // How smooth to make the parallax look. ** MUST SET VALUE > 0f **

    private Transform cam;                  // Reference to the main camera's transform
    private Vector3 previousCamPos;         // Stores the position of the camera in the previous frame (Allows for the calculation of parallaxing)

    // Called before Start(), but after all Game Objects have been initialized. Great for setting references and shit.
    void Awake(){
        // Sets up camera reference to main camera
        cam = Camera.main.transform; 
    }


	// Use this for initialization
	void Start () {
        // The previous frame had the current frame's camera position
        previousCamPos = cam.position;

        // Assigning corresponding parallax scales
        paraScales = new float[backgrounds.Length];
        for(int i = 0; i < backgrounds.Length; ++i) {
            paraScales[i] = backgrounds[i].position.z * -1;
        }

	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < backgrounds.Length; ++i){
            float parallax;

            // The parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            parallax = (previousCamPos.x - cam.position.x) * paraScales[i];

            // Set a target x position which is the current position plus the parallax
            float bgTargetPosX = backgrounds[i].position.x + parallax;

            // Create a target Vector3 position which is the background's current position with it's target x position
            Vector3 bgTargetVector = new Vector3(bgTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Smoothly transitions between original position and target position using smoothing 
            // (Time.deltaTime allows the smoothing to be independent of frame rates and instead decided by time in seconds) 
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bgTargetVector, smoothing * Time.deltaTime);
        }
        // Set the previousCamPos to the camera's position at the end of the update loop (frame)
        previousCamPos = cam.position;
    }
}
