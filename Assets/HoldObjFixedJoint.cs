using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObjFixedJoint : MonoBehaviour
{
    [SerializeField] GameObject heldObj;
    private GameObject cam;
    public float grabDistance = 5;
    private GameObject objRB;
    RaycastHit hit;

    private void Start()
    {
        //heldObj = GameObject.FindGameObjectWithTag("objHolder");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        //heldObj.transform.position = cam.transform.position;// + new Vector3(0,-1,1);
        if (Input.GetMouseButtonDown(0))
            if (objRB == null)
            {
                Debug.Log("Initial held obj value 1: " + heldObj);
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                Debug.Log("Initial held obj value 2: " + heldObj);
                DropObject();
                Debug.Log("Initial held obj value after Drop: " + heldObj);
                Debug.Log("Initial objRB value after Drop: " + objRB);
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
        Destroy(objRB.GetComponent<FixedJoint>());
        Destroy(heldObj.GetComponent<FixedJoint>());
        Destroy(heldObj.GetComponent<Rigidbody>());
        heldObj.transform.DetachChildren();
        objRB.GetComponent<Rigidbody>().isKinematic = false;
        cam.transform.parent = heldObj.transform;
        objRB = null;
        heldObj = null;
    }

    /* Yeet Function if time to implement:
       public float yeetForce;
    
       if(Input.GetKeyDown(KeyCode.G) && heldObj != null)
        {
            Debug.Log("Yeet");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 direction = player.transform.forward;
            Rigidbody objYeet = heldObj.GetComponent<Rigidbody>();
            objYeet.AddForce(direction  * yeetForce);
            DropObject();
            Debug.Log(direction);
        }
    */
}
