 /// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using System.Collections;
  
[RequireComponent(typeof(Animator))]  

//Name of class must be name of file as well

public class IK_Hands : MonoBehaviour {
	
	protected Animator animator;
	
	private GameObject rightTarget;
	private float startRTime; 
	private int rightHandPos;

	private GameObject leftTarget;
	private float startLTime;
	private int leftHandPos;

	public float speed = 0.1f;
	public bool rightHandActive;
	public bool leftHandActive;
	public Vector3[] rightHandPath;
	public Vector3[] leftHandPath;
	public TrailRenderer[] trails;

	private Vector3 lastRPos;
	private Vector3 lastLPos;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
		
		rightHandPos = 0;
		rightTarget = new GameObject("IK_R_hand");
		rightTarget.transform.SetParent(transform);
		rightTarget.transform.localRotation = Quaternion.identity;
		rightTarget.transform.localPosition = rightHandPath[rightHandPos];
		startRTime = 0f;

		leftHandPos = 0;
		leftTarget = new GameObject("IK_L_hand");
		leftTarget.transform.SetParent(transform);
		leftTarget.transform.localRotation = Quaternion.identity;
		leftTarget.transform.localPosition = leftHandPath[leftHandPos];
		startRTime = 0f;

		//ActivateRightHand();
		//ActivateLeftHand();	
	}
		
    
	void OnAnimatorIK(int layerIndex)
	{
		if (rightHandActive) {
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
			animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
			
			if (rightTarget != null) {
				animator.SetIKPosition(AvatarIKGoal.RightHand, rightTarget.transform.position);
				animator.SetIKRotation(AvatarIKGoal.RightHand, rightTarget.transform.rotation);
			} 
		} else {
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
			animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
		}

		if (leftHandActive) {
			animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
			animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

			if (leftTarget != null) {
				animator.SetIKPosition(AvatarIKGoal.LeftHand, leftTarget.transform.position);
				animator.SetIKRotation(AvatarIKGoal.LeftHand, leftTarget.transform.rotation);
			}
		} else {
			animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
			animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
		}
	}

    private void Update() {
        if (rightHandActive) {
			
				float t = (Time.time - startRTime) * speed;
				rightTarget.transform.localPosition = Vector3.Lerp( lastRPos, rightHandPath[rightHandPos], t);

				if (t > 1.0f) {
					startRTime = Time.time;
					rightHandPos = (rightHandPos + 1) % 4;
					lastRPos = rightTarget.transform.localPosition;
				}	
		}

		if (leftHandActive) {

			float t = (Time.time - startLTime) * speed;
			leftTarget.transform.localPosition = Vector3.Lerp(lastLPos, leftHandPath[leftHandPos], t);

			if (t > 1.0f) {
				startLTime = Time.time;
				leftHandPos = (leftHandPos + 1) % 4;
				lastLPos = leftTarget.transform.localPosition;
			}
		}
	}

	public void ActivateRightHand() {
		rightHandActive = true;
		startRTime = Time.time;
		lastRPos = rightTarget.transform.localPosition;
		trails[1].emitting = true;
	}

	public void ActivateLeftHand() {
		leftHandActive = true;
		startLTime = Time.time;
		lastLPos = leftTarget.transform.localPosition;
		trails[0].emitting = true;
	}

	public void DeactivateRightHand() {
		rightHandActive = false;
		trails[1].emitting = false;
	}

	public void DeactivateLeftHand() {
		leftHandActive = false;
		trails[0].emitting = false;
	}
}
