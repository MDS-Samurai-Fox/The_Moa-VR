using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace SamuraiFox.Moa {

    [CustomEditor(typeof (TargetController))]
    public class TargetControllerEditor : Editor {

        public override void OnInspectorGUI() {
			
			TargetController t = (TargetController) target;
			
			if (DrawDefaultInspector()) {}
			
			
						
		}

    }

}
#endif