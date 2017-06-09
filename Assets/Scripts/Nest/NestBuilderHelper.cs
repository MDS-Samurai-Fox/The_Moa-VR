using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace SamuraiFox.Moa {

    [CustomEditor (typeof (NestBuilder))]
    public class NestBuilderHelper : Editor {

		public override void OnInspectorGUI() {

			NestBuilder nest = (NestBuilder) target;

			// Called every time the inspector changes a value
			if (DrawDefaultInspector()) {

				

			}

			if (GUILayout.Button("Build Nest")) {

				nest.BuildNest();

			}

			if (GUILayout.Button("Hide Nest")) {

				nest.HideNest();

			}

		}
        
    }

}
#endif