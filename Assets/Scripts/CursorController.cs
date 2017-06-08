using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CursorController : MonoBehaviour {

    public Ease lookAtEase = Ease.OutBack;
    public float verticalSpawnOffset = -1;
    public float lookAtDuration = 1;
    public float delay = 1;

    void Start () {
        transform.DOScale (0, 0);
    }

    public void LookAt (Transform t) {

        transform.DOMoveY (transform.position.y + verticalSpawnOffset, lookAtDuration).SetEase (lookAtEase).From ();
        transform.DOLookAt (t.position, lookAtDuration).SetDelay (delay).SetEase (Ease.OutCubic).OnComplete (Float);

    }

    private void Float () {

        transform.DOMoveY(transform.position.y + 0.25f, 0.5f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);

    }

    public void Hide() {

        transform.DOScale(0, 0.25f);

    }

}