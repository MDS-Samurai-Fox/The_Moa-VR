using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class TorchController : MonoBehaviour {

    public bool isStartingArea = false;

    public List<FireSource> torchList = new List<FireSource> ();

    // Use this for initialization
    void Start () {

        FireSource[] torchArray = GetComponentsInChildren<FireSource> ();

        Debug.Log (torchArray.Length);

        for (int i = 0; i < torchArray.Length; i++) {

            torchList.Add (torchArray[i]);

        }

        if (!isStartingArea) {
			TurnOffLights();
		} else {
			TurnOnLights();
		}

    }

    public void TurnOnLights () {

		foreach (FireSource fs in torchList) {

            fs.StartBurning ();

        }

    }

    public void TurnOffLights () {

		foreach (FireSource fs in torchList) {

            fs.StopBurning ();

        }

    }

}