using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class EagleContainerController : MonoBehaviour {


    public UnityEvent OnArrowCollision;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("On collider enter for eagle");
     
        if (col.gameObject.tag == "projectile")
        {
            OnArrowCollision.Invoke();
            Debug.Log("Projectile tag found, lower health");
            Destroy(col.gameObject);
        }

    }
}
