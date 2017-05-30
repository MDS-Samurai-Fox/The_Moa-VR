//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Demonstrates how to create a simple interactable object
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class TreeShaker : MonoBehaviour
	{
		public GameObject berryPrefab;

		private TextMesh textMesh;
		private Vector3 oldPosition;
		private Quaternion oldRotation;

		private float attachTime;

		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & ( ~Hand.AttachmentFlags.SnapOnAttach ) & ( ~Hand.AttachmentFlags.DetachOthers );

		//-------------------------------------------------
		void Awake()
		{
			
		}


		//-------------------------------------------------
		// Called when a Hand starts hovering over this object
		//-------------------------------------------------
		private void OnHandHoverBegin( Hand hand )
		{
			ControllerButtonHints.ShowTextHint( hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger, "Pick berries");
		}


		//-------------------------------------------------
		// Called when a Hand stops hovering over this object
		//-------------------------------------------------
		private void OnHandHoverEnd( Hand hand )
		{
			ControllerButtonHints.HideTextHint( hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
		}


		//-------------------------------------------------
		// Called every Update() while a Hand is hovering over this object
		//-------------------------------------------------
		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				if ( hand.currentAttachedObject != gameObject )
				{
					GameObject berry = Instantiate (berryPrefab, hand.gameObject.transform.position, Quaternion.identity);
//					// Save our position/rotation so that we can restore it when we detach
//					oldPosition = transform.position;
//					oldRotation = transform.rotation;
//
					// Call this to continue receiving HandHoverUpdate messages,
					// and prevent the hand from hovering over anything else
					hand.HoverLock( GetComponent<Interactable>() );

					// Attach this object to the hand
					hand.AttachObject( berry, attachmentFlags );
				}
//				else
//				{
//					// Detach this object from the hand
//					hand.DetachObject( gameObject );
//
//					// Call this to undo HoverLock
//					hand.HoverUnlock( GetComponent<Interactable>() );
//
//					// Restore position/rotation
//					transform.position = oldPosition;
//					transform.rotation = oldRotation;
//				}
			}
		}


		//-------------------------------------------------
		// Called when this GameObject becomes attached to the hand
		//-------------------------------------------------
		private void OnAttachedToHand( Hand hand )
		{
//			attachTime = Time.time;
		}


		//-------------------------------------------------
		// Called when this GameObject is detached from the hand
		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
		}


		//-------------------------------------------------
		// Called every Update() while this GameObject is attached to the hand
		//-------------------------------------------------
		private void HandAttachedUpdate( Hand hand )
		{
		}


		//-------------------------------------------------
		// Called when this attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
		}


		//-------------------------------------------------
		// Called when another attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
		}
	}
}
