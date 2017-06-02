using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CursorController : MonoBehaviour {

    public Ease lookAtEase = Ease.OutBack;
    public float verticalSpawnOffset = -1;
    public float lookAtDuration = 1;
    public float delay = 1;

	// Float
	private Vector3 floatPosition = Vector3.zero;
	private Vector3 floatForward = Vector3.zero;
    private bool canFloat = false;

    void Start () {
        transform.DOScale (0, 0);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update () {

        if (canFloat) {

            transform.position = floatPosition + floatForward * Mathf.PingPong (Time.time, 1);

        }

    }

    public void LookAt (Transform t) {

        transform.DOMoveY (transform.position.y + verticalSpawnOffset, lookAtDuration).SetEase (lookAtEase).From ();

        transform.DOLookAt (t.position, lookAtDuration).SetDelay (delay).SetEase (Ease.OutCubic).OnComplete (Float);

    }

    private void Float () {

		floatForward = transform.forward;
		floatPosition = transform.position;
		canFloat = true;

    }

}