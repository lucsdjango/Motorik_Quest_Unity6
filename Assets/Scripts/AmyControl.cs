using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class AmyControl : MonoBehaviour {

    public Transform viewer;
    public GameObject particle;

    protected Animator animator;
    protected NavMeshAgent agent;
    protected Locomotion locomotion;
    protected Object particleClone;

    public float targetTurn = 90f;
	
   	private Quaternion targetRotation; //target world rotation


    void Start()
	{
		animator = GetComponent<Animator>();
        if (!viewer)
            viewer = Camera.main.transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        
        locomotion = new Locomotion(animator);

        particleClone = null;
    }

    protected void SetDestination() {
        // Construct a ray from the current mouse coordinates
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit)) {
            if (particleClone != null) {
                GameObject.Destroy(particleClone);
                particleClone = null;
            }

            // Create a particle if hit
            Quaternion q = new Quaternion();
            q.SetLookRotation(hit.normal, Vector3.forward);
            particleClone = Instantiate(particle, hit.point, q);

            agent.destination = hit.point;
        }
    }

    protected void AgentLocomotion() {
        if (AgentDone()) {
            locomotion.Do(0, 0);
            if (particleClone != null) {
                GameObject.Destroy(particleClone);
                particleClone = null;
                TurnFront();
            }
        } else {
            float speed = agent.desiredVelocity.magnitude;

            Vector3 velocity = Quaternion.Inverse(transform.rotation) * agent.desiredVelocity;

            float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;

            locomotion.Do(speed, angle);
        }
    }

    void OnAnimatorMove() {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
        transform.rotation = animator.rootRotation;
    }

    protected bool AgentDone() {
        return !agent.pathPending && AgentStopping();
    }

    protected bool AgentStopping() {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    private void TurnFront() {
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
        if (Input.GetKeyDown(KeyCode.C)) {
            animator.SetTrigger("Clap");
        }
        
        if (Input.GetKeyDown(KeyCode.G)) {
            animator.SetTrigger("Greet");
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            animator.SetTrigger("Dance");
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            animator.SetTrigger("Talk");
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            animator.SetTrigger("Waving");
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            animator.SetTrigger("Left hand up");
        }

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


        if (Input.GetButtonDown("Fire1"))
            SetDestination();

        AgentLocomotion();
    }
}
