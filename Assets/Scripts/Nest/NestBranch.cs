using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class NestBranch : MonoBehaviour {

    //-------------------------------------------------
    // Called when a Hand starts hovering over this object
    //-------------------------------------------------
    private void OnHandHoverBegin (Hand hand) {

        GameObject hand1 = GameObject.Find ("BlankController_Hand1");
        GameObject hand2 = GameObject.Find ("BlankController_Hand2");
        bool noObjectsInHand = hand.currentAttachedObject == hand1 || hand.currentAttachedObject == hand2;

        if (noObjectsInHand) {
            ControllerButtonHints.ShowTextHint (hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger, "Pick me up", true);
        }

    }

    //-------------------------------------------------
    // Called when a Hand stops hovering over this object
    //-------------------------------------------------
    private void OnHandHoverEnd (Hand hand) {
        ControllerButtonHints.HideTextHint (hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
    }

    private void OnAttachedToHand (Hand hand) {

        //    GameObject cbh = hand.GetComponentInChildren<ControllerButtonHints> ().gameObject;

        // if (cbh) {

        //     cbh.SetActive (false);
        //     cbh.SetActive (true);

        // }
        Debug.Log ("Attached to hand");

    }

    // private void HandHoverUpdate (Hand hand) {

    //     if (hand.GetStandardInteractionButtonDown () || ((hand.controller != null) && hand.controller.GetPressDown (Valve.VR.EVRButtonId.k_EButton_Grip))) {

    //         if (hand.currentAttachedObject != gameObject) {

    //             ControllerButtonHints.HideTextHint (hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);

    //             hand.AttachObject (this.gameObject, attachmentFlags);

    //         }

    //     }

    // }

}