using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObj : MonoBehaviour
{
    public float grabDistance = 5;
    public Transform parentObj;
    private GameObject heldObj;
    public float moveForce = 250;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ;
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }

        if(heldObj != null)
        {
            MoveObject();
        }
    }


    void DropObject()
    {
        Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
        objRig.useGravity = true;
        objRig.drag = 1;

        objRig.transform.parent = null;
        heldObj = null;
        
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, parentObj.position) > 0.1f)
        {
            Vector3 moveDirection = (parentObj.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickupObject(GameObject pickObject)
    {
        Debug.Log(pickObject.name);
        if(pickObject.GetComponent<Rigidbody>())
        {
            Rigidbody obj = pickObject.GetComponent<Rigidbody>();
            obj.useGravity = false;
            obj.drag = 50;

            obj.transform.parent = parentObj;
            heldObj = pickObject;
        }
    }
}
