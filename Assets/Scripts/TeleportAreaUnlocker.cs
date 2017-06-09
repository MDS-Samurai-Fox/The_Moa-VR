using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportAreaUnlocker : MonoBehaviour {

    public TeleportArea teleportArea;

    void Start () {

        teleportArea = GetComponent<TeleportArea> ();

        if (teleportArea == null) {
            Destroy (this.gameObject);
        }

    }

    public void Unlock () {
        teleportArea.locked = false;
    }

    public void Lock () {
        teleportArea.locked = true;
    }

}