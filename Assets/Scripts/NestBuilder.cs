using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace SamuraiFox.Moa
{

    [ExecuteInEditMode]
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class NestBuilder : MonoBehaviour
    {

        [SerializeField]
        private List<Transform> nestPartList = new List<Transform>();

        [SerializeField]
        private List<Transform> nestPartOutlineList = new List<Transform>();

        [SerializeField]
        private List<Vector3> nestPartScaleList = new List<Vector3>();

        private int maxNestChildrenCount = 0;
        private int nestIndex = 0;

        private int prefabNumber = 0;

        // Use this for initialization
        void Start()
        {

            for (int i = 0; i < 3; i++)
            {

                nestPartList.Add(transform.GetChild(i));
                nestPartScaleList.Add(transform.GetChild(i).localScale);

            }

            for (int i = 3; i < 6; i++)
            {

                nestPartOutlineList.Add(transform.GetChild(i));

            }

            maxNestChildrenCount = nestPartList.Count;

            HideNest();

            foreach (Transform t in nestPartOutlineList)
            {
                t.DOScale(Vector3.zero, 0);
            }

            ShowNextPartOutline();

        }

        private void OnHandHoverBegin(Hand hand)
        {

            // Build up one part of the nest
            if (hand.currentAttachedObject.GetComponent<NestPart>())
            {

                BuildNest();

            }

        }

        public void BuildNest()
        {

            if (nestIndex > maxNestChildrenCount - 1)
            {
                Debug.Log("Nest is already built");
                return;
            }

            Debug.Log("Adding " + nestPartList[nestIndex].name + " with a scale of " + nestPartScaleList[nestIndex]);
            nestPartList[nestIndex].DOScale(nestPartScaleList[nestIndex], 1);
            nestIndex++;
            ShowNextPartOutline();

        }

        public void RemoveFromNest()
        {

            if (nestIndex <= 0)
            {
                return;
            }

            nestPartList[nestIndex].DOScale(Vector3.zero, 1);
            nestIndex--;

        }

        public void HideNest()
        {

            foreach (Transform t in nestPartList)
            {
                t.DOScale(Vector3.zero, 0);
            }

        }

        public void ShowNextPartOutline()
        {

            foreach (Transform t in nestPartOutlineList)
            {

                if (nestIndex > maxNestChildrenCount - 1)
                {

                    t.DOScale(Vector3.zero, 0.5f);

                }
                else
                {

                    if (t == nestPartOutlineList[nestIndex])
                    {

                        nestPartOutlineList[nestIndex].DOScale(nestPartScaleList[nestIndex], 0.5f).SetDelay(0.5f);

                    }
                    else
                    {

                        t.DOScale(Vector3.zero, 0.5f);

                    }

                }

            }

        }

    }

}