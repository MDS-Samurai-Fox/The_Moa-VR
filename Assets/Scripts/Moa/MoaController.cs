using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SamuraiFox.Moa {
    public class MoaController : MonoBehaviour {

        public enum State {
            EatingBerries,
            Wandering
        }

        public State state = State.EatingBerries;

        [Header("Berries")]
        public Transform foodArea;

        private Animator anim = null;
        private Vector3 newPosition = Vector3.zero;
        private float timer = 0;

        private bool canPlayScreech = true;

        // Use this for initialization
        void Start() {

            anim = GetComponent<Animator>();
            state = State.EatingBerries;

            transform.LookAt(foodArea);
            
        }

        public void EatBerries() {

            StartCoroutine(BerryEatingAnimation());

        }

        private IEnumerator BerryEatingAnimation() {

            yield return new WaitForSeconds(2);

            anim.SetBool("isWalking", true);

            Vector3 directionToFoodArea = transform.position - foodArea.position;
            // float distanceToFoodArea = Vector3.Distance(transform.position, foodArea.position);

            transform.DOMove(foodArea.position - (transform.forward * 2f), 10).SetEase(Ease.Linear);

            yield return new WaitForSeconds(9);

            Debug.Log("Standing");

            anim.SetBool("isWalking", false);

            anim.SetBool("isStanding", true);

            yield return new WaitForSeconds(2.75f);

            AudioManager.Instance.Play("Screech");

            yield return new WaitForSeconds(3.5f);

            Debug.Log("Eating");

            anim.SetBool("isStanding", false);
            anim.SetBool("isEating", true);

            yield return new WaitForSeconds(1.5f);

            AudioManager.Instance.Play("Eating");

            yield return new WaitForSeconds(2);

            Debug.Log("Moa has finished eating!");
            
            anim.SetBool("isWalking", true);
            
            yield return new WaitForSeconds(1);
            transform.DORotate(Vector3.zero, 3);
            yield return new WaitForSeconds(3);
            anim.SetBool("isWalking", false);
            // transform.DORotate(Vector3.forward * 90)
            // anim.SetBool("isEating", false);
            // state = State.Wandering;

        }

        // Update is called once per frame
        void Update() {

            if (state == State.EatingBerries) {
                return;
            }

            timer += Time.deltaTime;

            if (timer > 7.0f) {

                Debug.Log("Timer > 7");

                float random = Random.value;

                // Moa starts to walk
                if (random > 0.6f) {

                    anim.SetBool("isWalking", true);
                    anim.SetBool("isStanding", false);
                    canPlayScreech = true;

                }
                // Moa stands up
                else if (random > 0.3f) {

                    if (canPlayScreech) {
                        StartCoroutine(PlayScreech(2.65f));
                        canPlayScreech = false;
                    }
                    anim.SetBool("isStanding", true);
                    anim.SetBool("isWalking", false);

                }
                // Idle
                else {

                    anim.SetBool("isWalking", false);
                    anim.SetBool("isStanding", false);
                    canPlayScreech = true;

                }

                timer = 0.0f;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {

                WalkCircle();

            }

        }

        private void WalkCircle() {
            //	print ("walk circle");
            transform.RotateAround(Vector3.zero, Vector3.up, 37.5f * Time.deltaTime / 3);
        }

        private IEnumerator PlayScreech(float delay) {

            yield return new WaitForSeconds(delay);
            Debug.Log("Playing screech");
            AudioManager.Instance.Play("Screech");

        }

    }

}