using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoaController : MonoBehaviour {

    private Animator anim;
    private Vector3 newPosition;
    private float timer;

    private bool canPlayScreech = true;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
    }

    void Awake() {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update() {

        timer += Time.deltaTime;

        if (timer > 7.0f) {

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