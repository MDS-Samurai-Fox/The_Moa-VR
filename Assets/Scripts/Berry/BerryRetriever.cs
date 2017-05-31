using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

namespace SamuraiFox.Moa {

    [RequireComponent (typeof (Interactable))]
    public class BerryRetriever : MonoBehaviour {

		public GameObject berryPrefab;
        public Transform berryOutline;

        private Vector3 originalScale = Vector3.one;

        public UnityEvent OnBerryRetrieval;

        void Start () {

            originalScale = berryOutline.localScale;
            
			HideOutline();

        }

        public void ShowOutline () {

            berryOutline.localScale = originalScale;

        }

		public void HideOutline() {

			berryOutline.localScale = Vector3.zero;

		}

        //-------------------------------------------------
        private void OnHandHoverBegin (Hand hand) {

            // If the retrieving object is the berry
            if (hand.currentAttachedObject.GetComponent<BerryInteractable> ()) {

                TakeBackItem (hand);

            }

        }

        private void TakeBackItem (Hand hand) {

			GameObject berry = Instantiate(berryPrefab, berryOutline.position, Quaternion.identity);

			HideOutline();

            for (int i = 0; i < hand.AttachedObjects.Count; i++) {

                GameObject detachedItem = hand.AttachedObjects[i].attachedObject;

                hand.DetachObject (detachedItem);

            }

			hand.DetachObject(hand.currentAttachedObject);

			ControllerButtonHints.HideTextHint (hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

            OnBerryRetrieval.Invoke ();

        }

    }

}