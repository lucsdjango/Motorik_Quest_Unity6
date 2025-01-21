using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class LisaControl : MonoBehaviour {

    private Transform viewer;
    //public GameObject particle;

    protected Animator animator;
    //protected NavMeshAgent agent;
    //protected Locomotion locomotion;
    //protected Object particleClone;

    private float targetTurn = 90f;
	
   	private Quaternion targetRotation; //target world rotation


    void Start()
	{
		animator = GetComponent<Animator>();
        viewer = Camera.main.transform;

        
    }

    public void TurnFront() {
        Vector3 targetDir = viewer.position - transform.position;
        targetDir.y = 0f;
        targetTurn = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);

        if (Mathf.Abs(targetTurn) > 2f) {
            animator.SetTrigger("Turn");
            animator.SetFloat("TurnAngle", targetTurn);
            targetRotation = transform.rotation * Quaternion.AngleAxis(targetTurn, Vector3.up); // Compute target world rotation
        }
    }
    void Update()
	{
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {

           if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("in Idle");

            if (Input.GetKeyDown(KeyCode.PageDown)) {
                targetTurn = 90f;
                if (Input.GetKey(KeyCode.LeftShift))
                    targetTurn *= 2f;

                animator.SetTrigger("Turn");
                animator.SetFloat("TurnAngle", targetTurn);
                targetRotation = transform.rotation * Quaternion.AngleAxis(targetTurn, Vector3.up); // Compute target world rotation
               
            }

            if (Input.GetKeyDown(KeyCode.PageUp)) {
                targetTurn = -90f;
                if (Input.GetKey(KeyCode.LeftShift))
                    targetTurn *= 2f;

                animator.SetTrigger("Turn");
                animator.SetFloat("TurnAngle", targetTurn);
                targetRotation = transform.rotation * Quaternion.AngleAxis(targetTurn, Vector3.up); // Compute target world rotation
               
            }

            if (Input.GetKeyDown(KeyCode.Home)) {
                TurnFront();
            }


        }
		else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Turn") && !animator.IsInTransition(0))
		{
			// calls MatchTarget when in Turn state, subsequent calls are ignored until targetTime (0.93f) is reached .
			animator.MatchTarget(Vector3.one, targetRotation, AvatarTarget.Root, new MatchTargetWeightMask(Vector3.zero, 1), animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 0.93f);			
		}

    }

    public void GoIdle() {
        animator.SetTrigger("Go idle");
    }
    public void LeftHandUp() {
        animator.SetTrigger("Left hand up");
    }
    public void RightHandUp() {
        animator.SetTrigger("Right hand up");
    }
    public void RightHandScrew() {
        animator.SetTrigger("Right hand screw");
    }
    public void LeftHandScrew() {
        animator.SetTrigger("Left hand screw");
    }
    public void ForwardReach() {
        animator.SetTrigger("Forward reach");
    }
    public void OneLegStandingRight() {
        animator.SetTrigger("Stand one leg right");
    }
    public void OneLegStandingLeft() {
        animator.SetTrigger("Stand one leg left");
    }
    public void OneLegJumpingRight() {
        animator.SetTrigger("Jump one leg right");
    }
    public void OneLegJumpingLeft() {
        animator.SetTrigger("Jump one leg left");
    }

    public void WalkHeels() {
        animator.SetTrigger("Walk heels");
    }

    public void WalkDistal() {
        animator.SetTrigger("Walk distal");
    }

    public void Tapping() {
        animator.SetTrigger("Tapping");
    }
}
