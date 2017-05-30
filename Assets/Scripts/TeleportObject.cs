using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportObject : MonoBehaviour {

    public GameObject objectToTeleport;

    [Header ("Initial Animation Stage")]
	public float initialScaleMultiplier = 1;
	public float verticalMoveOffset = 2;
    public float initialDelay = 0;
    public Ease InitialEaseType;
    public float initialDuration = 1;

	[Header("Final Animation Stage")]
	public float finalDelay = 0;
    public Ease finalEaseType;
    public float finalDuration = 1;
    public bool shouldAttachRigidbody;

    void Start () {

        if (!objectToTeleport) {
			Debug.Log("Object to teleport not set. Destroying " + name);
            Destroy(this.gameObject);
        }

		if (initialScaleMultiplier <= 0) {
			initialScaleMultiplier = 1;
		}

    }

    public void Teleport () {

        StartCoroutine (Animate ());

        if (shouldAttachRigidbody) {
            StartCoroutine (AttachRigidBody ());
        }

    }

    private IEnumerator Animate () {

        objectToTeleport.GetComponent<ItemPackageSpawner>().canTakeBackItem = false;

        Transform t = objectToTeleport.transform;

        Vector3 originalScale = objectToTeleport.transform.localScale;

        t.SetParent (this.transform);
		
		yield return new WaitForSeconds (initialDelay);

        t.DOScale (originalScale * initialScaleMultiplier, initialDuration).SetEase (InitialEaseType);

        t.DOMoveY (t.position.y + verticalMoveOffset, initialDuration).SetEase (InitialEaseType);

        yield return new WaitForSeconds (finalDelay);

		t.DOScale (originalScale, finalDuration).SetEase (finalEaseType);

        t.DOLocalMove (Vector3.zero, finalDuration).SetEase (finalEaseType);

        // Apply the changes when the animation finishes
        yield return new WaitForSeconds(finalDuration);

        objectToTeleport.GetComponent<ItemPackageSpawner>().canTakeBackItem = true;

    }

    private IEnumerator AttachRigidBody () {

        yield return new WaitForSeconds (initialDuration);
        objectToTeleport.gameObject.AddComponent<Rigidbody> ();

    }

}