using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float distanceX = 10;
    public float distanceY = 0;
    public float distanceZ;
    public float speed = 2;

    // Update is called once per frame

    void moveObject()
    {
        gameObject.transform.Translate(new Vector3(distanceX, distanceZ, distanceY).normalized * speed * Time.deltaTime);
        Debug.Log(gameObject.transform.position);
    }

    void Update()
    {
        //Reversing of Vector is yet not implemented, complication with if statement possible as its basically always true
        if (distanceX != 0 || distanceY != 0 || distanceZ != 0)
        {
            moveObject();
        }


    }
}
