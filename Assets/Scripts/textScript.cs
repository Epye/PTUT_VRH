using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textScript : MonoBehaviour
{
    private float x;
    private float y;
    private Vector3 rotateValue;
    GameObject obj;

    // Update is called once per frame
    void Update()
    {  
        if(Input.GetAxis("Swipe")<0){
            rotateValue = new Vector3(5, 5, 5);
            obj.transform.Rotate(rotateValue);
        }
    }
}
