using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbitCam : MonoBehaviour {

	public bool isPerspective = true;
	public Transform target;
    public float zoomFactor = 0.9f;
    public float orbitFactor = 3f;
    public float moveFactor = 0.04f;
    //public int qualityLevel = 5;
    public float touchZoom = 0.002f;
    public float touchOrbit = 0.2f;
    public float touchMove = 0.003f;

    private Camera cam;
	private Vector3 distance;
    private Vector3 targetStartPos;
    private float targetStartDistance;
    private Quaternion startRotation;

    void Start () {
        //QualitySettings.SetQualityLevel(qualityLevel, false);

        if (!target)
            Debug.Log("Target is null");
        targetStartPos = target.position;
        targetStartDistance = (transform.position - target.position).magnitude;
        startRotation = transform.rotation;
        cam = GetComponent<Camera>();

		distance =  new Vector3(0f, 0f, targetStartDistance);
	}

   
    void Update() {
        
        if (Input.anyKey)
            Cursor.visible = false;
        else
            Cursor.visible = true;


        if (Input.GetKeyDown("r"))
            Reset();

        if (Input.GetKey("escape"))
            Application.Quit();

        distance.z = distance.z * (1f - Input.GetAxis("Mouse ScrollWheel") * zoomFactor); // mouse zoom

        var y_orig = target.position.y;
       
        transform.Rotate(0f, Input.GetAxis("Horizontal")/4f, 0f, Space.World); //keyboard turn
        //target.Translate(0f, Input.GetAxis("Vertical"), 0f, transform); //keyboard move
        //target.position = new Vector3(target.position.x, y_orig, target.position.z); // keep target in original y-position

        // RIGHT mouse button: Rotate
        if (Input.GetMouseButton(1) /*&& Input.GetKey(KeyCode.LeftAlt) */) { 
            transform.Rotate(0f, Input.GetAxis("Mouse X")*orbitFactor, 0f, Space.World);
            transform.Rotate( -Input.GetAxis("Mouse Y")*orbitFactor, 0f, 0f, Space.Self);
        }
        /*
        // right mouse button: Move target
        if (Input.GetMouseButton(1)){
            var y = target.position.y;
            target.Translate(-Input.GetAxis("Mouse X") * moveFactor * distance.z, 0f, 0f, transform);
            target.Translate(0f, -Input.GetAxis("Mouse Y") * moveFactor * distance.z, 0f, transform);
            target.position = new Vector3(target.position.x, y, target.position.z); // keep target in original y-position
        }
        */
        /* Touch functionality--------------------------------------------------------------------------------------------     
        if (Input.touchCount >= 3) // Move view with 3 fingers
        {
             
            if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                var y = target.position.y;
                target.Translate(-Input.touches[0].deltaPosition.x * touchMove * distance.z , 0f, 0f, transform);
                target.Translate(0f, -Input.touches[0].deltaPosition.y * touchMove * distance.z, 0f, transform);
                target.position = new Vector3(target.position.x, y, target.position.z); // keep target in original y-position
            }
        }
       
        if (Input.touchCount == 2) // Zoom view with 2 fingers (pinch)
        { 
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = (currentMagnitude - prevMagnitude)* touchZoom;

            distance.z = distance.z * (1f - difference); 
        }

        
        if (Input.touchCount == 1) // Rotate view with 1 finger (Swipe)
        {           
            if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                transform.Rotate(0f, Input.touches[0].deltaPosition.x * touchOrbit, 0f, Space.World);
                transform.Rotate(-Input.touches[0].deltaPosition.y * touchOrbit, 0f, 0f, Space.Self);
                
            }
        }
        */
        //(End) Touch functionality --------------------------------------------------------------------------

        transform.position = target.position - transform.rotation * distance;

    }

    public void Reset() {
        
        transform.rotation = startRotation;
        target.position = targetStartPos;
        distance = new Vector3(0f, 0f, targetStartDistance);
        transform.position = target.position - transform.rotation * distance;
    }

}
