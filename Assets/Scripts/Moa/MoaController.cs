using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SamuraiFox.Moa {
    public class MoaController : MonoBehaviour {

        public enum State {
            EatingBerries,
            Walking,
            Shouting,
            Running,
            Waiting,
            Idle
        }

        public State state = State.EatingBerries;

        [Header("Eating")]
        public Transform foodArea;
        public UnityEvent OnFinishEating;

        [Header("Wandering")]
        public float movementSpeed = 35.0f;

        public float runningSpeed = 10.0f;

        public Vector3 centrePoint = Vector3.zero;

        private Animator animator = null;
        private float timer = 0;

        // Use this for initialization
        void Start() {

            animator = GetComponent<Animator>();
            //state = State.EatingBerries;

            if (state != State.Waiting)
            {
                transform.LookAt(foodArea);
            }
        }

        public void EatBerries() {

            //Debug.Log("Begin eating berries");
            StartCoroutine(BerryEatingAnimation());

        }

        /// <summary>
        /// Makes the moa walk towards the food area, screech and then eat
        /// </summary>
        private IEnumerator BerryEatingAnimation() {

            yield return new WaitForSeconds(1);

            animator.SetBool("isWalking", true);

            transform.DOMove(foodArea.position - (transform.forward * 2f), 10).SetEase(Ease.Linear);

            yield return new WaitForSeconds(10);

            //Debug.Log("Standing");

            animator.SetBool("isWalking", false);

            animator.SetBool("isStanding", true);

            yield return new WaitForSeconds(2.5f);

            AudioManager.Instance.Play("Screech Moa");

            yield return new WaitForSeconds(3.5f);

            //Debug.Log("Eating");

            animator.SetBool("isStanding", false);
            animator.SetBool("isEating", true);

            yield return new WaitForSeconds(1.5f);

            AudioManager.Instance.Play("Eating");

            yield return new WaitForSeconds(2);

            //Debug.Log("Moa has finished eating!");

            animator.SetBool("isEating", false);

            yield return new WaitForSeconds(1);  

            animator.SetBool("isWalking", true);

         //   yield return new WaitForSeconds(1);

            OnFinishEating.Invoke();

       //     yield return new WaitForSeconds(9);

        //    transform.DORotate(new Vector3(0, 210, 0), 3);  
        //    transform.DORotate(transform.forward, 5);

            //     yield return new WaitForSeconds(2);

            //     animator.SetBool("isWalking", false);

            //     yield return new WaitForSeconds(3);

            state = State.Walking;

            animator.SetBool("isWalking", true);

            yield return new WaitForSeconds(1);  

            transform.DORotate(new Vector3(0, 210, 0), 3);

        }

        // Update is called once per frame
        void Update() {

            if (state == State.Running)
            {
                animator.Play("Run");
                transform.DOMove(new Vector3(6.634f, 0.097f, -5.94f), runningSpeed).SetEase(Ease.Linear).OnComplete(ResetRunning);
            }

            if (state == State.EatingBerries) {
                return;
            }

            if (state == State.Waiting)
            {
                return;
            }

            timer += Time.deltaTime;

            if (timer > 8.0f) {

                //Debug.Log("Previous state: " + state.ToString());

                float chance = Random.Range(0.0f, 1.0f);

                // Determine the new state
                switch (state) {

                    case State.Walking:
                        {
                            if (chance > 0.6f && chance <= 1.0f) {
                                state = State.Shouting;
                            } else if (chance > 0.4f && chance <= 0.6f) {
                                state = State.Idle;
                            } else {
                                // Keep same state
                            }
                        }
                        break;
                    case State.Idle:
                        {
                            if (chance > 0.6f && chance <= 1.0f) {
                                state = State.Shouting;
                            } else if (chance > 0.2f && chance <= 0.6f) {
                                state = State.Walking;
                            } else {
                                // Keep same state
                            }
                        }
                        break;
                    case State.Shouting:
                        {
                            if (chance >= 0.5f) {
                                state = State.Walking;
                            } else {
                                state = State.Idle;
                            }
                        }
                        break;

                }

                //Debug.Log("New state: " + state.ToString());

                // Change the moa's state accordingly
                switch (state) {

                    case State.Walking:
                        {
                            animator.SetBool("isWalking", true);
                            animator.SetBool("isStanding", false);
                        }
                        break;
                    case State.Idle:
                        {
                            animator.SetBool("isWalking", false);
                            animator.SetBool("isStanding", false);
                        }
                        break;
                    case State.Shouting:
                        {
                            animator.SetBool("isWalking", false);
                            animator.SetBool("isStanding", true);

                            StartCoroutine(PlayScreech(2.65f));
                        }
                        break;

                }

                timer = 0.0f;

            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {

                WalkCircle();

            }

        }

        private void WalkCircle() {
            //transform.RotateAround(Vector3.zero, Vector3.up, movementSpeed * Time.deltaTime * 0.33f);
            transform.RotateAround(centrePoint, Vector3.up, movementSpeed * Time.deltaTime * 0.33f);
        }

        private IEnumerator PlayScreech(float delay) {

            yield return new WaitForSeconds(delay);
            AudioManager.Instance.Play("Screech");

        }

        public void StartRunning()
        {
            state = State.Running;
        }

        public void ResetRunning()
        {
            state = State.Waiting;
            transform.position = new Vector3(-27.302f, -0.139f, 26.121f);
        }

    }

#if UNITY_EDITOR

    [CustomEditor(typeof (MoaController))]
    public class MoaEditor : Editor {

        public override void OnInspectorGUI() {

            MoaController colorMan = (MoaController) target;

            // this if statement is true whenever the inspector changes its values
            if (DrawDefaultInspector()) { }

            if (GUILayout.Button("Eat berries")) {

                colorMan.EatBerries();

            }

        }

    }

#endif
}