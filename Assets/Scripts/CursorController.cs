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
    public float forwardZ = 0;

    void Start () {
        transform.DOScale (0, 0);
    }

    public void LookAt (Transform t) {

        transform.DOMoveY (transform.position.y + verticalSpawnOffset, lookAtDuration).SetEase (lookAtEase).From ();

        transform.DOLookAt (t.position, lookAtDuration).SetDelay (delay).SetEase (Ease.OutCubic).OnComplete (Float);

    }

    private void Float () {

        transform.DOLocalMoveZ(1.5f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalMoveX(1, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

    }

    public void Hide() {

        transform.DOScale(0, 1);

    }

}