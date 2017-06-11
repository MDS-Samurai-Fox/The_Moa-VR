using DG.Tweening;
using UnityEngine;
using Valve.VR.InteractionSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SamuraiFox.Moa {

    [RequireComponent(typeof (ArcheryTarget))]
    public class TargetController : MonoBehaviour {

        public enum MovementType {
            MoveHorizontally,
            MoveVertically,
            Rotate
        }

        public MovementType movementType;

        private Vector3 currentPosition = Vector3.zero;
        
        private float angle;

        void Start() {

            currentPosition = transform.localPosition;

            switch (movementType) {

                case MovementType.Rotate:
                    {
                        currentPosition = transform.position;
                        currentPosition.y += 0.5f;
                    }
                    break;
                case MovementType.MoveHorizontally:
                    {
                        transform.DOLocalMoveX(currentPosition.x + 1, 2).SetLoops(-1, LoopType.Yoyo);
                    }
                    break;
                case MovementType.MoveVertically:
                    {
                        transform.DOLocalMoveY(currentPosition.y + 1, 2).SetLoops(-1, LoopType.Yoyo);
                    }
                    break;

            }

            // Debug.Log(name + " - " + currentPosition);

        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update() {

            if (movementType == MovementType.Rotate) {

                // transform.RotateAround(currentPosition, Vector3.right, 50 * Time.deltaTime);
                angle += 2 * Time.deltaTime;

                var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * 0.5f;
                transform.position = currentPosition + (Vector3) offset;

            }

        }

        public void TakeDamage() {

            gameObject.SendMessageUpwards("ApplyDamage", SendMessageOptions.DontRequireReceiver);

        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof (TargetController))]
    public class TargetControllerEditor : Editor {

        public override void OnInspectorGUI() {

            TargetController t = (TargetController) target;

            if (DrawDefaultInspector()) { }

            if (GUILayout.Button("Take damage")) {

                t.TakeDamage();

            }

        }

    }

#endif
}