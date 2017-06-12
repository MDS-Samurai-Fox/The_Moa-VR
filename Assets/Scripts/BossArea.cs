using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossArea : MonoBehaviour {

    // Use this for initialization
    void Start () {

        // gameObject.SetActive (false);
        // transform.DOScale (Vector3.zero, 0);

    }

    public void Show () {

        transform.DOScale (Vector3.one, 1).OnComplete(MakeActive);

    }

    public void Hide () {

        transform.DOScale (Vector3.zero, 1);

    }

    private void MakeActive () {

        gameObject.SetActive (true);

    }

}