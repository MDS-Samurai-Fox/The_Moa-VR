using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportPointUnlocker : MonoBehaviour {

	public TeleportPoint teleportPoint;

	public void Unlock() {
		teleportPoint.locked = false;
		teleportPoint.markerActive = true;
	}

	public void Lock() {
		teleportPoint.locked = true;
		teleportPoint.markerActive = false;
	}

}