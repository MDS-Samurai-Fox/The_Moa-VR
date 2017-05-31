using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace SamuraiFox.Moa {

    //-------------------------------------------------------------------------
    [RequireComponent (typeof (Interactable))]
    public class BerryInteractable : MonoBehaviour {

        //-------------------------------------------------
        // Called when this GameObject becomes attached to the hand
        //-------------------------------------------------
        private void OnAttachedToHand (Hand hand) {

            GameObject cbh = hand.GetComponentInChildren<ControllerButtonHints> ().gameObject;

            if (cbh) {

                cbh.SetActive (false);
                cbh.SetActive (true);

            }

        }

        //-------------------------------------------------
        // Called when this GameObject is detached from the hand
        //-------------------------------------------------
        private void OnDetachedFromHand (Hand hand) {
            Debug.Log ("OnDetachedFromHand " + name);
            ControllerButtonHints.HideTextHint (hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        }

        //-------------------------------------------------
        // Called every Update() while this GameObject is attached to the hand
        //-------------------------------------------------
        private void HandAttachedUpdate (Hand hand) {
            ControllerButtonHints.ShowTextHint (hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad, "Deliver to next area", true);
        }

        //-------------------------------------------------
        // Called when this attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusAcquired (Hand hand) {
            Debug.Log ("OnHandFocusAcquired");
        }

        //-------------------------------------------------
        // Called when another attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusLost (Hand hand) {
            Debug.Log ("OnHandFocusLost");
        }

    }
}