using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class NestBranch : MonoBehaviour {

    private bool canPlayHitSound = true;

    //-------------------------------------------------
    // Called when a Hand starts hovering over this object
    //-------------------------------------------------
    private void OnHandHoverBegin(Hand hand) {

        GameObject hand1 = GameObject.Find("BlankController_Hand1");
        GameObject hand2 = GameObject.Find("BlankController_Hand2");
        bool noObjectsInHand = hand.currentAttachedObject == hand1 || hand.currentAttachedObject == hand2;

        if (noObjectsInHand) {
            ControllerButtonHints.ShowTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger, "Pick me up", true);
        }

    }

    //-------------------------------------------------
    // Called when a Hand stops hovering over this object
    //-------------------------------------------------
    private void OnHandHoverEnd(Hand hand) {
        ControllerButtonHints.HideTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
    }

    //-------------------------------------------------
    // Called when this GameObject is detached from the hand
    //-------------------------------------------------
    private void OnDetachedFromHand(Hand hand) {
        Debug.Log("OnDetachedFromHand " + name);
        ControllerButtonHints.HideTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
    }

    //-------------------------------------------------
    // Called every Update() while this GameObject is attached to the hand
    //-------------------------------------------------
    private void HandAttachedUpdate(Hand hand) {
        ControllerButtonHints.ShowTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger, "Throw inside the glowing nest", true);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other) {

        if (canPlayHitSound) {
            canPlayHitSound = false;
            AudioManager.Instance.Play("Stick hit");
        }

    }

}