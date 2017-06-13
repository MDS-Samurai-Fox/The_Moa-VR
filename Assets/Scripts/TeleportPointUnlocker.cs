using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportPointUnlocker : MonoBehaviour {

    public TeleportPoint teleportPoint;

    private bool canBeLocked = true;

    void Start () {

        teleportPoint = GetComponent<TeleportPoint> ();

        if (teleportPoint == null) {
            Destroy (this.gameObject);
        }

    }

    public void Unlock () {

        teleportPoint.locked = false;
        teleportPoint.markerActive = true;

    }

    public void Lock () {

        if (canBeLocked) {

            teleportPoint.locked = true;
            teleportPoint.markerActive = false;

            canBeLocked = false;

        }

    }

}