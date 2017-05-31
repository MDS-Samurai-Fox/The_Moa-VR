using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SamuraiFox.Moa {

    public class BerryFallAnimator : MonoBehaviour {

        private List<Transform> berries = new List<Transform> ();

        void Start () {

            for (int i = 0; i < transform.childCount; i++) {

                berries.Add (transform.GetChild (i));

            }

            transform.DOMoveY (-1, 0);

        }

        public void Animate () {

            transform.DOMoveY (2, 0);

            foreach (Transform t in berries) {

                float randomDelay = Random.Range (0.1f, 0.25f);
                t.DOMoveY (0.1f, 2).SetDelay (randomDelay).SetEase (Ease.OutBounce);

            }

        }

    }

}