using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SamuraiFox.Moa {
    public class EagleController : MonoBehaviour {

        public enum State {
            Arriving,
            Circling,
            Attacking,
            Dead
        }

        public State state = State.Arriving;

        [Header("Transforms")]
        public GameObject eagle;
        public Transform startingPoint;
        public Transform arrivingPointBeforeAttack;
        // public Transform startingPoint;

        [Header("Arriving to attack point")]
        public float arrivalDuration = 5;

        [Header("Variables")]
        public float flyingSpeed = 50;
        public float health = 1000;

        public UnityEvent OnEagleAttack;
        public UnityEvent OnEagleDeath;

        public GameObject player;

        // Attacking
        private Vector3 beginAttackPosition = Vector3.zero;
        private float timer = 0f;
        private bool HasArrived = false;

        private Animator animator;

        // Use this for initialization
        void Start() {

            eagle.transform.position = startingPoint.position;
            eagle.transform.localScale = Vector3.zero;
            
        }

        void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Fly")) || (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly0")))
            {
                if (HasArrived)
                {
                    state = State.Circling;
                    Debug.Log("Set state to circling");
                }          
            }
            else if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) || (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack0")))
            {
                if (HasArrived)
                {
                    state = State.Attacking;
                    Debug.Log("Set state to attacking");
                }
            }

            if (state == State.Arriving)
            {

                eagle.transform.LookAt(arrivingPointBeforeAttack);
                return;

            }
            else if (state == State.Circling)
            {

                if (health <= 0)
                {

                    OnEagleDeath.Invoke();
                    state = State.Dead;

                }

                // Rotate the transform around the middle
                eagle.transform.RotateAround(Vector3.zero, Vector3.up, flyingSpeed * Time.deltaTime * 0.33f);

                timer += Time.deltaTime;

                if (timer > 12)
                {

                    float chance = Random.Range(0.0f, 1.0f);

                    if (chance > 0.5f)
                    {

                        Debug.Log("Eagle now moving up and back to original Y-position");
                        // Move up and down
                        eagle.transform.DOMoveY(beginAttackPosition.y + 2, 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

                    }
                    else
                    {

                        Debug.Log("Eagle now moving down and back to original Y-position");
                        // Move down and up
                        eagle.transform.DOMoveY(beginAttackPosition.y - 3, 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

                    }

                    // Reset the timer
                    timer = 0.0f;

                }

            }

            else if (state == State.Attacking)
            {
                if (health <= 0)
                {
                    state = State.Dead;
                }

                Debug.Log("Move towards player - state is attack");
                Vector3 vectorToPlayer = (player.transform.position + new Vector3(0, 5, 0)) - transform.position;
                float distanceToPlayer = vectorToPlayer.magnitude;
                Vector3 normalised = Vector3.Normalize(vectorToPlayer);
                Vector3 attackPosition = normalised * (distanceToPlayer - 5.0f);
                eagle.transform.LookAt(player.transform.position);

                eagle.transform.DOMove(attackPosition, 3.0f);
            }
        }

        public void SpawnEagle() {

            animator.SetTrigger("EagleSpawnTrigger");
            Debug.Log ("Before arrival helper");
            StartCoroutine(ArrivalHelper());
			Debug.Log ("Spawn eagle");

        }

        private IEnumerator ArrivalHelper() {

            eagle.transform.DOScale(Vector3.one * 2, 3);
            AudioManager.Instance.Play("Screech Eagle");

            yield return new WaitForSeconds(3);

            eagle.transform.DOMove(arrivingPointBeforeAttack.position, arrivalDuration).SetEase(Ease.Linear).OnComplete(Circle);
            eagle.transform.DOLookAt(arrivingPointBeforeAttack.position, 0.5f).SetEase(Ease.Linear);

            //if (transform.position == arrivingPointBeforeAttack.position)
            //{
            //    HasArrived = true;
            //}

        }

        //public void Attack() {

        //    Debug.Log("Eagle is now attacking");

        //    state = State.Attacking;

        //    animator.SetTrigger("Attack");

        //    // Change between (0, 0, 0) and (180, 0, 0) depending on the model's orientation
        //    // eagle.transform.eulerAngles = new Vector3(0, 180, 0);

        //    beginAttackPosition = eagle.transform.position;

        //    // eagle.transform.DOMoveY(beginAttackPosition.y + 2, 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        //}

        public void Circle()
        {
            OnEagleAttack.Invoke();

            HasArrived = true;

            Debug.Log("Eagle is now circling");

            state = State.Circling;

            animator.SetBool("HasArrivedBool", true);

            // Change between (0, 0, 0) and (180, 0, 0) depending on the model's orientation
            // eagle.transform.eulerAngles = new Vector3(0, 180, 0);

            beginAttackPosition = eagle.transform.position;

            // eagle.transform.DOMoveY(beginAttackPosition.y + 2, 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        void OnCollisionEnter(Collision other) {

        }

        public void TakeDamage() {

        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof (EagleController))]
    public class EagleControllerEditor : Editor {

        public override void OnInspectorGUI() {

            EagleController eagle = (EagleController) target;

            if (DrawDefaultInspector()) { }

            if (GUILayout.Button("Spawn Eagle")) {

                eagle.SpawnEagle();

            }

        }

    }
#endif

}