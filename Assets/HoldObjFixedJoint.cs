using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObjFixedJoint : MonoBehaviour
{
    //heldObj is the GameObject floating in front of the camera and which connects with the held object
    [SerializeField] GameObject heldObj;

    //grabDistance, as it's name says, defines the distance to which it is possible to pick up objects
    public float grabDistance = 5;

    //objRB is the GameObject which handles the Rigidbody, the object that will be picked up
    GameObject objRB;

    //objChecker is a GameObject which chekcs if an object should be picked up or not
    GameObject objChecker;

    //hit is the POV Ray which points at the object
    RaycastHit hit;

    //force to yeet with
    public float yeetForce;
    public float upYeetForce;
    bool yeetable;
    public Transform cam;

    private void Start()
    {
        //Altlast heldObj = GameObject.FindGameObjectWithTag("objHolder");
        //cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        //heldObj.transform.position = cam.transform.position;// + new Vector3(0,-1,1);
        if (Input.GetMouseButtonDown(0))
        {
            if (objRB == null)
            {
                if (objRB == null && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance))
                {
                    objChecker = hit.transform.gameObject;
                    if (objChecker.CompareTag("pickable"))
                    {
                        yeetable = true;
                        Debug.Log(yeetable);
                        PickupObject(objChecker);
                    }
                    else
                    {
                        Debug.Log("Fuck off das kannst du nicht aufheben");
                    }
                }
            }
            else
            {
                DropObject();
            }
        }
        if (Input.GetKeyDown(KeyCode.G) && yeetable == true)
        {
            yeet();
        }
    }

    void PickupObject(GameObject pickObject)
    {
        objRB = pickObject;
        heldObj = GameObject.FindGameObjectWithTag("objHolder");
        heldObj.AddComponent<FixedJoint>();
        heldObj.gameObject.GetComponent<FixedJoint>().connectedBody = objRB.GetComponent<Rigidbody>();
        objRB.GetComponent<Rigidbody>().isKinematic = true;
        objRB.transform.parent = heldObj.transform;
    }

    void DropObject()
    {
        //Remove FixedJoints and Rigidbody of holder to reset, also reset kinematic of objRB
        Destroy(objRB.GetComponent<FixedJoint>());
        Destroy(heldObj.GetComponent<FixedJoint>());
        Destroy(heldObj.GetComponent<Rigidbody>());
        heldObj.transform.DetachChildren();
        objRB.GetComponent<Rigidbody>().isKinematic = false;
        // not necessary anymore cam.transform.parent = heldObj.transform;
        objRB = null;
        heldObj = null;
    }

    void yeet()
    {
        Vector3 direction = cam.transform.forward * yeetForce + transform.up * upYeetForce;
        Rigidbody objYeet = objRB.GetComponent<Rigidbody>();
        DropObject();
        objYeet.AddForce(direction, ForceMode.Impulse);
        yeetable = false;
    }
}