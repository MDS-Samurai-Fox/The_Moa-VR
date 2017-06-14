using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Utils : MonoBehaviour {

    public static Utils Instance { get; private set; }

    private float fadeDuration = 1;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake () {

        if (Instance != null && Instance != this) {
            Destroy (gameObject);
        }

        Instance = this;

        DontDestroyOnLoad (gameObject);

    }

    public void FadeIn (CanvasGroup cg) {

        cg.DOFade (1, fadeDuration);

    }

    public void FadeOut (CanvasGroup cg) {

        cg.DOFade (0, fadeDuration);

    }

    public void FadeTransformIn (Transform t) {

        t.DOScale (1, fadeDuration);

    }

	public void FadeEggTransformIn (Transform t) {

		t.DOScale (2, fadeDuration);

	}

    public void FadeTransformOut (Transform t) {

        t.DOScale (0, fadeDuration);

    }

    public void SetFadeDuration (float fadeDuration) {

        this.fadeDuration = fadeDuration;

    }

}