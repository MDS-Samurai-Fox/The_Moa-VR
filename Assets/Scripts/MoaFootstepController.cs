using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SphereCollider), (typeof (Rigidbody)))]
public class MoaFootstepController : MonoBehaviour {

    private bool canPlayFootstep = true;

    private bool initialFootstep = true;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {

        if (initialFootstep) {
            initialFootstep = false;
            return;
        }

        if (canPlayFootstep) {
            canPlayFootstep = false;
            AudioManager.Instance.Play("Footstep");
        }

    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other) {

		canPlayFootstep = true;

    }

}