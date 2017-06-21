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
            BetweenAttacks,
            Waiting,
            Chasing,
            Dead
        }

        public State state = State.Arriving;

        public GameObject RunningMoa;

        [Header("Transforms")]
        public GameObject eagle;
        public Transform startingPoint;
        public Transform arrivingPointBeforeAttack;
        // public Transform startingPoint;

        [Header("Arriving to attack point")]
        public float arrivalDuration = 5;

        [Header("Variables")]
        public float flyingSpeed = 50;
        public float chasingSpeed = 40;
        public float health = 1000;

        public UnityEvent OnEagleAttack;
        public UnityEvent OnEagleDeath;

        public GameObject player;

        // Attacking
        private Vector3 beginAttackPosition = Vector3.zero;
        private float timer = 0f;
        private bool HasArrived = false;
        private bool HasAttacked = false;
        private bool LookingAtTarget = false;
        private bool AttackPositionSet = false;
		private bool DeathInvokeCalled = false;
        private Vector3 target;
        private Vector3 attackPosition;

        private Animator animator;

        // Use this for initialization
        void Start() {

            eagle.transform.position = startingPoint.position;
            eagle.transform.localScale = Vector3.zero;

            if (state == State.Waiting)
            {
                SpawnEagle();
                animator.Play("Chasing");
            }
        }

        void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (state == State.Waiting)
            {          
                return;
            }

            if (state == State.Chasing)
            {
                  eagle.transform.DOMove(new Vector3(22.87f, -1.69f, -40.31f), chasingSpeed).SetEase(Ease.Linear).OnComplete(ResetChasing);
             //   eagle.transform.DOMove(RunningMoa.transform.position, 2).SetEase(Ease.Linear);//.OnComplete(ResetChasing);
                return;
            }

            if (health <= 0)
			{
				state = State.Dead;
			}
				
			else if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Fly")) || (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly0")))
            {
                if ((HasArrived) && (!HasAttacked))
                {
                    state = State.Circling;
                 //   Debug.Log("Set state to circling");
                }   
                else if ((HasArrived) && (HasAttacked))
                {
                    state = State.BetweenAttacks;
                   // Debug.Log("Set state to between attacks");
                    AttackPositionSet = false;
                }       
            }
            else if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) || (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack0")))
            {
                if (HasArrived)
                {
                    state = State.Attacking;
                //    Debug.Log("Set state to attacking");
                    LookingAtTarget = false;
                }
            }



            if (state == State.Arriving)
            {

                eagle.transform.LookAt(arrivingPointBeforeAttack);
                return;

            }
            else if (state == State.Circling)
            {

           //     if (health <= 0)
           //     {

            //        OnEagleDeath.Invoke();
            //        state = State.Dead;

            //    }

                // Rotate the transform around the middle
                eagle.transform.RotateAround(Vector3.zero, Vector3.up, flyingSpeed * Time.deltaTime * 0.33f);

                timer += Time.deltaTime;

                if (timer > 12)
                {

                    float chance = Random.Range(0.0f, 1.0f);

                    if (chance > 0.5f)
                    {

                     //   Debug.Log("Eagle now moving up and back to original Y-position");
                        // Move up and down
                        eagle.transform.DOMoveY(beginAttackPosition.y + 2, 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

                    }
                    else
                    {

                    //    Debug.Log("Eagle now moving down and back to original Y-position");
                        // Move down and up
                        eagle.transform.DOMoveY(beginAttackPosition.y - 3, 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

                    }

                    // Reset the timer
                    timer = 0.0f;

                }

            }

            else if (state == State.Attacking)
            {
            //    if (health <= 0)
            //    {
            //        state = State.Dead;
            //    }

                //Debug.Log("Move towards player - state is attack");

                if (!AttackPositionSet)
                {
                    Vector3 vectorToPlayer = (player.transform.position) - eagle.transform.position;
                    float distanceToPlayer = vectorToPlayer.magnitude;
                    Vector3 normalised = Vector3.Normalize(vectorToPlayer);

                    if (distanceToPlayer < 15.0f)
                        attackPosition = normalised * (distanceToPlayer * -0.35f);
                    else
                        attackPosition = normalised * (distanceToPlayer * -0.1f);

                    //attackPosition = player.transform.position + (player.transform.forward) * 1.2f; // + new Vector3(0, 1.5f, 0);
                    attackPosition = player.transform.position + new Vector3(0, 1, 0); // + new Vector3(0, 1.5f, 0);


                    eagle.transform.LookAt(player.transform.position);
                    AttackPositionSet = true;

                    Debug.Log("Attack Position Set");
                    print("Eagle position: " + eagle.transform.position);
                    print("Player position " + player.transform.position);
                    print("vectorToPlayer: " + vectorToPlayer);
                    print("distanceToPlayer: " + distanceToPlayer);
                    print("attackPosition " + attackPosition);
                }

                eagle.transform.DOMove(attackPosition, 12.0f);

                //Vector3 vectorToPlayer = (player.transform.position) - eagle.position;
                //float distanceToPlayer = vectorToPlayer.magnitude;
                //print("distanceToPlayer: " + distanceToPlayer);

                //if (distanceToPlayer > 5.0f)
                //{
                //    eagle.transform.DOMove(player.transform.position, 12.0f);
                //    print("In if statement");
                //}
      
                HasAttacked = true;
            }

            else if (state == State.BetweenAttacks)
            {
             //   if (health <= 0)
            //    {
            //        state = State.Dead;
            //    }

                if (!LookingAtTarget)
                {
                    float random1 = Random.Range(20.0f, 30.0f);
                    float random2 = Random.Range(20.0f, 30.0f);

                    if (Random.Range(0.0f, 1.0f) < 0.5f)
                    {
                        random1 *= -1;
                    }

                    if (Random.Range(0.0f, 1.0f) < 0.5f)
                    {
                        random2 *= -1;
                    }

                 //   print("Random 1: " + random1 + "  Random 2: " + random2);

                    target = new Vector3(random1, Random.Range(20.0f, 30.0f), random2);

                    HasAttacked = true;
               
                    //var newRot = Quaternion.LookRotation(-target);
                    //transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 3.0f);

                    eagle.transform.LookAt(target);
                    LookingAtTarget = true;
                }

                eagle.transform.DOMove(target, 30.0f);
           //     eagle.transform.RotateAround(target, Vector3.up, Time.deltaTime * 20.0f);
            }

            else if (state == State.Dead)
            {
				if (!DeathInvokeCalled) 
				{
					OnEagleDeath.Invoke();
					DeathInvokeCalled = true;
				}

                animator.Play("Dead");

				eagle.transform.DOMove(new Vector3(eagle.transform.position.x, 0.0f, eagle.transform.position.z), (eagle.transform.position.y / 8.0f)).SetEase(Ease.Linear);
				eagle.transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, -180.0f), (eagle.transform.position.y / 8.0f)).SetEase(Ease.Linear);

                //DORotate(Vector3 to, float duration, RotateMode mode)
            }
        }

        public void SpawnEagle() {

            animator.SetTrigger("EagleSpawnTrigger");
       //     Debug.Log ("Before arrival helper");
            StartCoroutine(ArrivalHelper());
		//	Debug.Log ("Spawn eagle");

        }

        //void OnCollisionEnter(Collider col)
        //{
        //    Debug.Log("On collider enter for eagle");

        //    if (col.tag == "projectile")
        //    {
        //        TakeDamage();
        //        Debug.Log("Projectile tag found, lower health");
        //    }

        //}


        public void StartChasing()
        {
            state = State.Chasing;
        }

        public void ResetChasing()
        {
            state = State.Waiting;
            transform.position = new Vector3(-63.09f, 0.74f, 7.73f);
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

           // Debug.Log("Eagle is now circling");

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


        public void TakeDamage() {
            //Debug.Log("Take damage function");
            health--;
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