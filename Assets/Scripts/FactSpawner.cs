using DG.Tweening;
using UnityEngine;

public class FactSpawner : MonoBehaviour {

    public bool shouldSpawn = true;

    public Transform cameraTransform;

    private GameObject currentFact;
    private Transform currentFactTransform;

    public void SpawnFact (GameObject g) {

        if (!shouldSpawn) {
            return;
        }

        currentFact = g;
        currentFactTransform = g.transform;
        currentFactTransform.SetParent (cameraTransform);

        currentFactTransform.eulerAngles = Vector3.zero;
        // currentFactTransform.DORotate(Vector3.zero, 0);
        currentFactTransform.DOLocalMove (new Vector3 (0, -1.5f, 1), 0);

        Utils.Instance.FadeTransformIn (currentFactTransform);

    }

    public void DeleteFact () {

        Debug.Log ("Deleting fact " + currentFactTransform.name + " - Parent: " + currentFactTransform.parent);

        Destroy (currentFactTransform.gameObject);
        Destroy (currentFact);

    }

}