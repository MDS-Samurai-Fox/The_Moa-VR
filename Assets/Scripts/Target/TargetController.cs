using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TargetController : MonoBehaviour {

    public void TakeDamage() {
        
        transform.parent.gameObject.SendMessageUpwards("ApplyDamage", SendMessageOptions.DontRequireReceiver);

    }

}