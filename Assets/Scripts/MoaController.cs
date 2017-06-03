using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoaController : MonoBehaviour {

	private Animator anim;
	private Vector3 newPosition;
	private float timer;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();		
	}

	void Awake()
	{
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		

		if (timer > 7.0f)
		{
			float random = Random.value;

		//	print(random);

			if(random > 0.6f)
			{
				anim.SetBool("isWalking", true);
				anim.SetBool("isStanding", false);
			}
				
			else if (random > 0.3f)
			{
				anim.SetBool("isStanding", true);
				anim.SetBool("isWalking", false);
			}	
			else
			{
				anim.SetBool("isWalking", false);
				anim.SetBool("isStanding", false);
			}

			timer = 0.0f;		
		}


		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
		{
			WalkCircle ();	
		}
			
	}

	private void WalkCircle()
	{
	//	print ("walk circle");
		transform.RotateAround(Vector3.zero, Vector3.up, 40 * Time.deltaTime / 3);	
	}
}
