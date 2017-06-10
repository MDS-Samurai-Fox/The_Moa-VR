using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace SamuraiFox.Moa {
    public class EagleController : MonoBehaviour {

        public enum State {
            Arriving,
            Attacking,
            Dead
        }

        public State state = State.Arriving;

        [Header("Transforms")]
        public GameObject eagle;
        public Transform startingPoint;
        public Transform arrivingPointBeforeAttack;
        // public Transform startingPoint;

        [Header("Arriving to attack point")]
        public float arrivalDuration = 5;

        [Header("Variables")]
        public float flyingSpeed = 50;
        public float health = 1000;

        public UnityEvent OnEagleAttack;
        public UnityEvent OnEagleDeath;

        // Attacking
        private Vector3 beginAttackPosition = Vector3.zero;
        private float timer = 0f;

        // Use this for initialization
        void Start() {

            eagle.transform.position = startingPoint.position;
            eagle.transform.localScale = Vector3.zero;

        }

        // Update is called once per frame
        void Update() {

            if (state == State.Arriving) {
                eagle.transform.LookAt(arrivingPointBeforeAttack);
                return;
            } else if (state == State.Attacking) {

                if (health <= 0) {

                    OnEagleDeath.Invoke();
                    state = State.Dead;

                }

                // Rotate the transform around the middle
                eagle.transform.RotateAround(Vector3.zero, Vector3.up, flyingSpeed * Time.deltaTime * 0.33f);

                timer += Time.deltaTime;

                if (timer > 12) {

                    float chance = Random.Range(0.0f, 1.0f);

                    if (chance > 0.5f) {

                        Debug.Log("Eagle now moving up and back to original Y-position");
                        // Move up and down
                        eagle.transform.DOMoveY(beginAttackPosition.y + 2, 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

                    } else {

                        Debug.Log("Eagle now moving down and back to original Y-position");
                        // Move down and up
                        eagle.transform.DOMoveY(beginAttackPosition.y - 3, 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

                    }

                    // Reset the timer
                    timer = 0.0f;

                }

            }

        }

        public void SpawnEagle() {

            StartCoroutine(ArrivalHelper());

        }

        private IEnumerator ArrivalHelper() {

            eagle.transform.DOScale(Vector3.one, 3);

            yield return new WaitForSeconds(3);

            AudioManager.Instance.Play("Screech eagle");

            eagle.transform.DOMove(arrivingPointBeforeAttack.position, arrivalDuration).SetEase(Ease.Linear).OnComplete(BeginAttack);
            eagle.transform.DOLookAt(arrivingPointBeforeAttack.position, 0.5f).SetEase(Ease.Linear);

        }

        public void BeginAttack() {

            Debug.Log("Eagle is now attacking");

            OnEagleAttack.Invoke();

            state = State.Attacking;

            // Change between (0, 0, 0) and (180, 0, 0) depending on the model's orientation
            // eagle.transform.eulerAngles = new Vector3(0, 180, 0);

            beginAttackPosition = eagle.transform.position;

            // eagle.transform.DOMoveY(beginAttackPosition.y + 2, 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        void OnCollisionEnter(Collision other) {

        }

        public void TakeDamage() {

        }

    }

}