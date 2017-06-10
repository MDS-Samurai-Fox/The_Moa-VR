using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace SamuraiFox.Moa {

    [CustomEditor(typeof (EagleController))]
    public class EagleControllerEditor : Editor {

        public override void OnInspectorGUI() {
			
			EagleController eagle = (EagleController) target;
			
			if (DrawDefaultInspector()) {}
            
            if (GUILayout.Button("Spawn Eagle")) {
                
                eagle.SpawnEagle();
                
            }
						
		}

    }

}
#endif