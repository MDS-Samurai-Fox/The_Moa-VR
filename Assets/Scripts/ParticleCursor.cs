using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParticleCursor : MonoBehaviour {

	public Transform target;
	public float duration;
	public Ease ease;

	public GameObject particleSystem;

	public void MoveToTarget() {

		transform.DOMove(target.position, duration).SetEase(ease);

	}
	
}