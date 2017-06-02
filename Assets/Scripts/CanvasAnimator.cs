using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasAnimator : MonoBehaviour {

	private CanvasGroup transparency;

	private TextMeshProUGUI text;

	// Use this for initialization
	void Start () {

		transparency = GetComponent<CanvasGroup>();

		text = GetComponentInChildren<TextMeshProUGUI>();

		Utils.Instance.FadeOut(transparency);
		
	}
	
}