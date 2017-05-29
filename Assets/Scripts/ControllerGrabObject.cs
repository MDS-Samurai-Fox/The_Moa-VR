using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

	// A reference to the object being tracked
	private SteamVR_TrackedObject trackedObj;

	// Stores the GameObject that the trigger is currently colliding with
	private GameObject collidingObject;

	// Reference to the object being grabbed
	private GameObject objectInHand;

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

	/// <summary>
	/// Sets the colliding object.
	/// </summary>
	/// <param name="other"> The collider </param>
	private void SetCollidingObject(Collider other) {

		// Return if holding something else or object doesn't have a RigidBody
		if (collidingObject || !other.GetComponent<Rigidbody> ()) {
			return;
		}

		collidingObject = other.gameObject;

	}

	public void OnTriggerEnter(Collider other) {

		SetCollidingObject (other);

	}

	public void OnTriggerStay(Collider other) {

		SetCollidingObject (other);

	}

	public void OnTriggerExit(Collider other) {

		// Don't delete if the object doesn't exist
		if (!collidingObject) {
			return;
		}

		collidingObject = null;

	}

	/// <summary>
	/// Grabs the object once a colliding object exists
	/// </summary>
	private void GrabObject() {

		objectInHand = collidingObject;

		// Set the colliding object to be null
		collidingObject = null;

		// Make a new fixed joint and connect it to the object in hand
		FixedJoint joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody> ();

	}

	/// <summary>
	/// Add a fixed joint to the controller so it doesn't break up easily
	/// </summary>
	/// <returns>The fixed joint.</returns>
	private FixedJoint AddFixedJoint() {

		FixedJoint fx = gameObject.AddComponent<FixedJoint> ();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;

	}

	/// <summary>
	/// Releases the object.
	/// </summary>
	private void ReleaseObject() {

		// Make sure the object has a fixed joint
		if (GetComponent<FixedJoint> ()) {

			// Get rid of the connected body
			GetComponent<FixedJoint> ().connectedBody = null;
			Destroy (GetComponent<FixedJoint> ());

			objectInHand.GetComponent<Rigidbody> ().velocity = Controller.velocity;
			objectInHand.GetComponent<Rigidbody> ().angularVelocity = Controller.angularVelocity;

		}

		// Reset the object in hand
		objectInHand = null;

	}

	void Update() {

		// Check for the hair trigger
		if (Controller.GetHairTriggerDown ()) {

			// grab the colliding object if it exists
			if (collidingObject) {

				GrabObject ();

			}

		}

		if (Controller.GetHairTriggerUp ()) {

			if (objectInHand) {

				ReleaseObject ();

			}

		}

	}

}