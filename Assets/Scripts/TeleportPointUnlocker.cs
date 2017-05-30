using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportPointUnlocker : MonoBehaviour {

	public TeleportPoint teleportPoint;

	public void Unlock() {
		teleportPoint.locked = false;
	}

	public void Lock() {
		teleportPoint.locked = true;
	}

}