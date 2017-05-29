using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInputTest : MonoBehaviour {

	// A reference to the object being tracked
	private SteamVR_TrackedObject trackedObj;

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

	void Update () {

		// Get the position of the finger when it’s on the touchpad and write it to the Console.
		if (Controller.GetAxis() != Vector2.zero)
		{
			Debug.Log(gameObject.name + Controller.GetAxis());
		}

		// 2
		if (Controller.GetHairTriggerDown())
		{
			Debug.Log(gameObject.name + " Trigger Press");
		}

		// 3
		if (Controller.GetHairTriggerUp())
		{
			Debug.Log(gameObject.name + " Trigger Release");
		}

		// 4
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			Debug.Log(gameObject.name + " Grip Press");
		}

		// 5
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			Debug.Log(gameObject.name + " Grip Release");
		}
		
	}

}