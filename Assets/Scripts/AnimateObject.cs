using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour {

    private Animator animator;

    void Awake () {

		animator = GetComponent<Animator>();

    }

	public void StopAnimation() {

		animator.StopPlayback();

	}

	public void PlayAnimationBool(string boolToCheck) {

		animator.SetBool(boolToCheck, true);
		StartCoroutine(DisableAnimationBool(boolToCheck));

	}
	
	private IEnumerator DisableAnimationBool(string boolToCheck) {

		yield return new WaitForSeconds(0.1f);
		animator.SetBool(boolToCheck, false);

	}
    
}