using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour {
    public GameObject obj;
	// Use this for initialization
	void Start () {
      //  OVRTouchpad.Create();
      //  OVRTouchpad.TouchHandler += HandleTouchHandler;
    }

    void Update()
    {

        if (Input.GetButton("Tap"))
        {
            // Do something if tap starts
        }

        if (Input.GetButtonUp("Tap"))
        {
            obj.transform.Rotate(new Vector3(1, 2, 5));
        }
        /*
        if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Right)
        {
            text2.text = "Right!";

        }
        if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Left)
        {
            text2.text = "Left!";

        }*/
    }

}
