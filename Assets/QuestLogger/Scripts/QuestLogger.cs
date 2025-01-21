using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestLogger : MonoBehaviour
{

    public Camera headCam;
    public Transform left, right;

    public bool logFingers;

    private OVRSkeleton leftSkeleton, rightSkeleton;
    private OVRHand leftHand, rightHand;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;
    private bool isIndexFingerPinching;

    //private LineRenderer line;
    private Transform p0;
    private Transform p1;
    private Transform p2;

    private Transform handIndexTipTransform;

    public Logger logger;
    private Text debugText;
    
    public OVRManager ovrManager;

   // public bool logFACS = false;
   // public OVRFaceExpressions face;
   // float[] facs;
    string[] feNames;

    public OVRInput.Button startButton;

    private Dictionary<OVRBone, string> leftBones = new Dictionary<OVRBone, string>();
    private Dictionary<OVRBone, string> rightBones = new Dictionary<OVRBone, string>();

    private const string dps = "F4";



    void Awake()
    {
        print("awake called");
        ovrManager = GameObject.FindObjectOfType<OVRManager>();
        //face = GameObject.FindObjectOfType<OVRFaceExpressions>();
        left = GameObject.Find("LeftHandAnchor").transform;
        right = GameObject.Find("RightHandAnchor").transform;
        headCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

    }

    public void CallWhenAddedToScenceInEditor()
    {
        print("CallWhenAddedToScenceInEditor called");
        ovrManager = GameObject.FindObjectOfType<OVRManager>();
        //face = GameObject.FindObjectOfType<OVRFaceExpressions>();
        left = GameObject.Find("LeftHandAnchor").transform;
        right = GameObject.Find("RightHandAnchor").transform;
        headCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

    }


    // Start is called before the first frame update
    void Start()
    {
       

        leftHand = left.GetComponentInChildren<OVRHand>(true);      // include inactive
        rightHand = right.GetComponentInChildren<OVRHand>(true);    // include inactive

        
        logFingers =    (   logFingers &&
                            (leftHand != null) && 
                            (rightHand != null)
                        );

        // GetActiveController returns "None". Hands not reocognized at startup?
        print("logfingers: " + OVRInput.GetActiveController().ToString() + "  " + (leftHand == null ? "NULLHAND": "LEFTHANDOK") );

        if (logFingers) 
        {
            leftHand.gameObject.SetActive(true);
            rightHand.gameObject.SetActive(true);
            leftSkeleton = left.GetComponentInChildren<OVRSkeleton>(); 
            rightSkeleton = right.GetComponentInChildren<OVRSkeleton>(); 
        }

        //logFACS = logFACS && face != null && face.ValidExpressions;

        //if (logFACS)
        //    facs = new float[63];

        ovrManager.isInsightPassthroughEnabled = false;

        Invoke("StartLogging", 5f); // temporary 
    }

    // Update is called once per frame
    void Update()
    {
        

        if (logger.logging)
        {
            
            logger.UpdateEntry("HMD", headCam.transform.rotation.ToString("F4") + " " + headCam.transform.eulerAngles.ToString(dps) + " " + headCam.transform.position.ToString(dps));

            if (logFingers) {

                var fingers = Enum.GetValues(typeof(OVRHand.HandFinger));

                if (leftHand.IsTracked)
                {
                    float d = headCam.transform.InverseTransformPoint(leftHand.transform.position).magnitude;
                    logger.UpdateEntry("LHandConf", leftHand.HandConfidence.ToString());
                    logger.UpdateEntry("LeftHand",
                        //d.ToString(dps) + " " +
                        headCam.transform.InverseTransformPoint(leftHand.transform.position).ToString(dps) + " " +
                        leftHand.transform.rotation.ToString(dps) + " " + 
                        leftHand.transform.eulerAngles.ToString(dps) + " " +  
                        leftHand.transform.position.ToString(dps)
                    );

                

                        foreach (OVRHand.HandFinger finger in fingers)
                        {
                            if (finger == OVRHand.HandFinger.Max)
                                continue;
                            string fingerName = finger.ToString().Replace("HandFinger.", "");
                            //Debug.Log(finger);
                            
                            logger.UpdateEntry("L" + fingerName + "Pinch", leftHand.GetFingerPinchStrength(finger).ToString(dps));
                        }
                    

                    foreach (KeyValuePair<OVRBone, string> labeledBone in leftBones)
                    {
                        OVRBone bone = labeledBone.Key;
                        logger.UpdateEntry(labeledBone.Value, bone.Transform.localRotation.ToString(dps) + " " + bone.Transform.localEulerAngles.ToString(dps) + " " + bone.Transform.position.ToString(dps));
                    }

                    logger.UpdateEntry("LHandScale", leftHand.HandScale.ToString(dps));


                } else 
                {
                    logger.UpdateEntry("LHandConf","NOT_TRACKED"); 
                }
                if (rightHand.IsTracked)
                {
                    logger.UpdateEntry("RHandConf",rightHand.HandConfidence.ToString());
                    float d = headCam.transform.InverseTransformPoint(leftHand.transform.position).magnitude;
                    logger.UpdateEntry("RightHand",
                        //d.ToString(dps) + " " +
                        headCam.transform.InverseTransformPoint(rightHand.transform.position).ToString(dps) + " " +
                        rightHand.transform.rotation.ToString(dps) + " " + 
                        rightHand.transform.eulerAngles.ToString(dps) + " " +  
                        rightHand.transform.position.ToString(dps)
                    );

                   
                        foreach (OVRHand.HandFinger finger in fingers)
                        {
                            if (finger == OVRHand.HandFinger.Max)
                                continue;
                            string fingerName = finger.ToString().Replace("HandFinger.", "");
                            //print(finger);
                            logger.UpdateEntry("R" + fingerName + "Pinch", rightHand.GetFingerPinchStrength(finger).ToString(dps));
                        }

                    foreach (KeyValuePair<OVRBone, string> labeledBone in rightBones)
                    {
                        OVRBone bone = labeledBone.Key;
                        logger.UpdateEntry(labeledBone.Value, bone.Transform.localRotation.ToString(dps) + " " + bone.Transform.localEulerAngles.ToString(dps) + " " + bone.Transform.position.ToString(dps));
                    }


                    logger.UpdateEntry("RHandScale", rightHand.HandScale.ToString(dps));

                } else {

                    logger.UpdateEntry("RHandConf","NOT_TRACKED"); 
                }
            } else {
                logger.UpdateEntry("LeftHand", left.transform.rotation.ToString(dps) + " " +  left.transform.eulerAngles.ToString(dps) + " " + left.transform.position.ToString(dps));
                logger.UpdateEntry("RightHand", right.transform.rotation.ToString(dps) + " " +  right.transform.eulerAngles.ToString(dps) + " " +  right.transform.position.ToString(dps));
            }
        /*
            if (logFACS && face.ValidExpressions ) {

                face.CopyTo(facs,0);

                for (int i = 0; i < facs.Length; i++){
                    logger.UpdateEntry(feNames[i], facs[i].ToString("F4"));
                }
                  
            
            } */
            
            
        } else if (

            (!logFingers && OVRInput.GetDown(startButton, OVRInput.Controller.Active)) ||
            (logFingers && rightHand.GetFingerIsPinching(OVRHand.HandFinger.Middle))

        ) {

            StartLogging();

        }


        
    }

    private void StartLogging(){


        logger.AddEntry("HMD");
        logger.AddEntry("LeftHand");
        logger.AddEntry("RightHand");

        if (logFingers)
        {

            logger.AddEntry("LHandConf");
            logger.AddEntry("RHandConf");


            var fingers = Enum.GetValues(typeof(OVRHand.HandFinger));

            foreach (OVRHand.HandFinger finger in fingers)
            {
                if (finger == OVRHand.HandFinger.Max)
                    continue;
                string fingerName = finger.ToString().Replace("HandFinger.", "");
                logger.AddEntry("L" + fingerName + "Pinch");
                logger.AddEntry("R" + fingerName + "Pinch");
            }


            foreach (OVRBone bone in leftSkeleton.Bones)
            {
                string boneLabel = "L" + OVRSkeleton.BoneLabelFromBoneId(leftSkeleton.GetSkeletonType(), bone.Id);
                if (boneLabel != null && !boneLabel.Contains("Unknown"))
                {
                    logger.AddEntry(boneLabel);
                    leftBones.Add(bone, boneLabel);
                    print(boneLabel);
                }


            }

            foreach (OVRBone bone in rightSkeleton.Bones)
            {

                string boneLabel = "R" + OVRSkeleton.BoneLabelFromBoneId(rightSkeleton.GetSkeletonType(), bone.Id);
                if (boneLabel != null && !boneLabel.Contains("Unknown"))
                {
                    logger.AddEntry(boneLabel);
                    rightBones.Add(bone, boneLabel);
                    print(boneLabel);

                }

            }

            logger.AddEntry("LHandScale");
            logger.AddEntry("RHandScale");
        }

            /*      if (logFACS){

                      feNames = System.Enum.GetNames (typeof(OVRFaceExpressions.FaceExpression));
                      foreach (string feName in feNames){
                          logger.AddEntry(feName);
                      }

                  } 
            */


            logger.StartLogging("TestLog");
            // Invoke("BlankIt", 2.0f);            // temporary off
            //logFingers = false; // for testing


        }

    private void BlankIt()
    {
        leftHand.GetComponent<OVRMeshRenderer>().enabled = false;
        rightHand.GetComponent<OVRMeshRenderer>().enabled = false;
        ovrManager.isInsightPassthroughEnabled = true;
        headCam.clearFlags = CameraClearFlags.SolidColor;
        headCam.backgroundColor = Color.clear;
    }

    void MaskHands()
    {
        GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
        OVRPassthroughLayer layer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
        layer.AddSurfaceGeometry(leftHand.gameObject, true);
        layer.AddSurfaceGeometry(rightHand.gameObject, true);
        // Disable the mesh renderer to avoid rendering the surface within Unity
        MeshRenderer mr = leftHand.GetComponent<MeshRenderer>();
        if (mr)
        {
            mr.enabled = false;
        }
        mr = rightHand.GetComponent<MeshRenderer>();
        if (mr)
        {
            mr.enabled = false;
        }
    }

}
