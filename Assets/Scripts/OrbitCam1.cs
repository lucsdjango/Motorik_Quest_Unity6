using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCam1 : MonoBehaviour {

	public bool isPerspective = true;
	public Transform target;
	public GameObject ball;

	private Camera cam;
	private Vector3 distance;
 

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		distance =  new Vector3(0f, 0f, (transform.position - target.position).magnitude);

	}
	
	// Update is called once per frame
	void Update () {
       
        if (Input.GetKeyDown ("b")) {
			var clone = Instantiate(ball, transform.position, transform.rotation);
			clone.GetComponent<Rigidbody>().linearVelocity = transform.TransformDirection(Vector3.forward * 10f);
		}
		
		if (Input.GetKeyDown ("p"))
			if (isPerspective) {
				isPerspective = false;
				cam.orthographic = true;
			} else {
				isPerspective = true;
				cam.orthographic = false;
			}

       

		if (isPerspective)		
		    distance.z -= Input.GetAxis("Mouse ScrollWheel")*5f; 
        else
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 5f;

        transform.Rotate( 0f, -Input.GetAxis("Horizontal"), 0f, Space.World );	
		transform.Rotate( Input.GetAxis("Vertical"), 0f, 0f,  Space.Self );	
		transform.position = target.position - transform.rotation * distance;
	}
		
}
