using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]

    public class NestBuilder : MonoBehaviour
    {

        public GameObject[] nests;
        //public List<GameObject> nestObjects;

        private int prefabNumber = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void BuildNest()
        {
            if (prefabNumber >= nests.Length)
                return;

            if (prefabNumber == 0)
            {
                GameObject moa = Instantiate(nests[prefabNumber]);
            }
            else
            {
                Destroy(nests[prefabNumber - 1]);
                GameObject moa = Instantiate(nests[prefabNumber]);
            }

            if (prefabNumber < nests.Length)
            {
                prefabNumber++;
            }

        }

    }

}
