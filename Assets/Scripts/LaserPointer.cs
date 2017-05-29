using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class LaserPointer : MonoBehaviour {

	// A reference to the object being tracked
	private SteamVR_TrackedObject trackedObj;

	public GameObject laserPrefab;

	private GameObject laser;

	private Transform laserTransform;

	private Vector3 hitPoint;

	// ------- Teleport Reticle

	public Transform cameraRigTransform; 

	public GameObject teleportReticlePrefab;

	// Reference to the reticle
	private GameObject reticle;

	// Stores a reference to the teleport reticle transform for ease of use
	private Transform teleportReticleTransform; 

	// Stores a reference to the player’s head (the camera).
	public Transform headTransform; 

	// Is the reticle offset from the floor, so there’s no “Z-fighting” with other surfaces.
	public Vector3 teleportReticleOffset; 

	// Is a layer mask to filter the areas on which teleports are allowed.
	public LayerMask teleportMask; 

	// Is set to true when a valid teleport location is found.
	private bool shouldTeleport;

	// The image to fade when the user teleports
	public GameObject canvasFadeImage;

	// The duration of the fade
	private float canvasFadeDuration = 0.3f;

	// ---------------------------------------------------------------------------------------------------------

	// Device property to provide easy access to the controller
	private SteamVR_Controller.Device Controller {
		get {
			return SteamVR_Controller.Input ((int)trackedObj.index);
		}
	}

	void Awake() {

		trackedObj = GetComponent < SteamVR_TrackedObject> ();

	}

	void Start() {

		// Instantiate the laser
		laser = Instantiate (laserPrefab);
		laserTransform = laser.transform;

		// Instantiate the reticle
		reticle = Instantiate(teleportReticlePrefab);
		teleportReticleTransform = reticle.transform;

	}
	
	// Update is called once per frame
	void Update () {

		// Touchpad is held down
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {

			// Create a raycast hit
			RaycastHit hit;

			// 
			if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask)) {
				
				hitPoint = hit.point;
				ShowLaser(hit);

				// Create the teleport reticle
				reticle.SetActive (true);
				teleportReticleTransform.position = hitPoint + teleportReticleOffset;
				shouldTeleport = true;

			}

		}
		// Deactivate the laser if the user isn't touching the gamepad (And teleport the player to the desired position
		else {
			
			laser.SetActive(false);
			reticle.SetActive (false);


		}

		if (Controller.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) {

			StartCoroutine(Teleport ());

		}
		
	}

	/// <summary>
	/// Draw the laser from the controller to the object it collides with
	/// </summary>
	/// <param name="hit">Hit.</param>
	private void ShowLaser(RaycastHit hit) {

		laser.SetActive (true);

		laserTransform.position = Vector3.Lerp (trackedObj.transform.position, hitPoint, 0.5f);
		laserTransform.LookAt (hitPoint);
		laserTransform.localScale = new Vector3 (laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);

	}

	private IEnumerator Teleport() {

		// Disable the teleport reticle
		reticle.SetActive (false);

		// Calculate the diference between the positions of the camera rig's center and the player's head
		Vector3 difference = cameraRigTransform.position - headTransform.position;

		// Reset the y-position for the above difference to 0, because the calculation doesn't consider the vertical position of the player's head
		difference.y = 0;

		// Make the screen black
		FadeTo (1);
		yield return new WaitForSeconds (canvasFadeDuration);

		// Move the camera rig to the position of the hit point and add the calculated difference
		// Without the difference, the player would teleport to an incorrect location
		cameraRigTransform.position = hitPoint + difference;

		// Make the screen appear again
		yield return new WaitForSeconds (canvasFadeDuration);
		FadeTo (0);

		shouldTeleport = false;

	}

	private void FadeTo(float fadevalue) {

		canvasFadeImage.GetComponent<Image> ().DOFade (fadevalue, canvasFadeDuration);

	}

}