//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Demonstrates how to create a simple interactable object
//
//=============================================================================
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

namespace SamuraiFox.Moa {
    //-------------------------------------------------------------------------
    [RequireComponent(typeof (Interactable))]
    public class BerryTreeController : MonoBehaviour {

        private Animator animator;

        public GameObject berryPrefab;

        [EnumFlags]
        public Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags;

        public UnityEvent OnBerryPickUpEvent;

        private Vector3 oldPosition;
        private Quaternion oldRotation;

        private float attachTime;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start() {

            animator = GetComponentInChildren<Animator>();

        }

        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand) {

            GameObject hand1 = GameObject.Find("BlankController_Hand1");
            GameObject hand2 = GameObject.Find("BlankController_Hand2");
            bool noObjectsInHand = hand.currentAttachedObject == hand1 || hand.currentAttachedObject == hand2;

            if (noObjectsInHand) {
                ControllerButtonHints.ShowTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger, "Press me", true);
            }

        }

        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand) {
            ControllerButtonHints.HideTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        }

        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        private void HandHoverUpdate(Hand hand) {

            if (hand.GetStandardInteractionButtonDown() || ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))) {

                if (hand.currentAttachedObject == null) {
                    
                    animator.SetBool("isShaking", true);
                    // animator.SetBool("isShaking", false);

                    OnBerryPickUpEvent.Invoke();

                    GameObject berry = Instantiate(berryPrefab, hand.gameObject.transform.position, Quaternion.identity);

                    hand.AttachObject(berry, attachmentFlags);

                    return;

                } else {
                    
                    animator.SetBool("isShaking", true);
                    // animator.SetBool("isShaking", false);

                    if (hand.currentAttachedObject.GetComponent<BerryInteractable>() == null) {

                        Debug.Log("Instantiating berry");

                        ControllerButtonHints.HideTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);

                        OnBerryPickUpEvent.Invoke();

                        GameObject berry = Instantiate(berryPrefab, hand.gameObject.transform.position, Quaternion.identity);

                        // Call this to continue receiving HandHoverUpdate messages,
                        // and prevent the hand from hovering over anything else
                        // hand.HoverLock( berry.GetComponent<Interactable>() );

                        // Attach this object to the hand
                        hand.AttachObject(berry, attachmentFlags);

                    } else {

                        Debug.Log("Already picking up berry");

                    }

                }

            }
        }
    }
}