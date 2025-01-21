 /// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using System.Collections;
  
[RequireComponent(typeof(Animator))]  

//Name of class must be name of file as well

public class IK_LookAt : MonoBehaviour {
	
	protected Animator animator;
	
	public Transform lookAtObj = null;
	public float overallWeight = 1.0f;
	public float bodyWeight = 0.3f;
	public float headWeight = 0.6f;
	public float eyesWeight = 1.0f;
	public float clampWeight = 0.5f;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();

	}
		
    
	void OnAnimatorIK(int layerIndex)
	{
		if (animator) {

			animator.SetLookAtWeight(overallWeight, bodyWeight, headWeight, eyesWeight, clampWeight);

			if (lookAtObj != null) {
				animator.SetLookAtPosition(lookAtObj.position);
			} else {
				animator.SetLookAtWeight(0.0f);
			}
		}
	}  
}
