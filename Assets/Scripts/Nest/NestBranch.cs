using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace SamuraiFox.Moa {

    public class NestBranch : MonoBehaviour {

        private bool canPlayHitSound = true;

        private bool firstCollision = true;

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

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        void OnCollisionEnter(Collision other) {

            if (firstCollision) {
                firstCollision = false;
                return;
            }

            Debug.Log("Collision enter with " + other.collider.name);

            if (canPlayHitSound) {
                canPlayHitSound = false;
                AudioManager.Instance.Play("Stick");
            }

            TargetController target = other.collider.gameObject.GetComponentInParent<TargetController>();

            // Hit a minigame target
            if (target != null) {

                target.TakeDamage();

            }

        }

        /// <summary>
        /// OnCollisionExit is called when this collider/rigidbody has
        /// stopped touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        void OnCollisionExit(Collision other) {

            Debug.Log("Collision Exit");
            canPlayHitSound = true;

        }

    }

}