using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace SamuraiFox.Moa {

    [CustomEditor(typeof (MoaController))]
    public class MoaEditor : Editor {

        public override void OnInspectorGUI() {

            MoaController colorMan = (MoaController) target;

            // this if statement is true whenever the inspector changes its values
            if (DrawDefaultInspector()) { }

            if (GUILayout.Button("Eat berries")) {

                colorMan.EatBerries();

            }

        }

    }

}
#endif