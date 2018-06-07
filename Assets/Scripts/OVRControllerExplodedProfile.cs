using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRControllerExplodedProfile : MonoBehaviour {
    public GameObject contacts;
    public GameObject ProfileExploded;
    public GameObject camera;
    private Vector3 origin;

    // Use this for initialization
    void Start () {
        origin = camera.gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {    
        OVRInput.Controller activeController = OVRInput.GetActiveController();
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            contacts.SetActive(false);
            ProfileExploded.SetActive(true);
        }
        if (OVRInput.Get(OVRInput.Button.One))
        {
            contacts.SetActive(true);
            ProfileExploded.SetActive(false);
        }
        if (OVRInput.Get(OVRInput.Touch.One))
        {
            Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            this.transform.RotateAround(camera.transform.position, camera.transform.up, primaryTouchpad.x * 5);
            //posOnCircle(primaryTouchpad, contacts);

            //content.anchoredPosition = new Vector2(content.anchoredPosition.x + primaryTouchpad.x * 50, content.anchoredPosition.y);
        }
    }

    Vector3 posOnCircle(Vector2 primaryTouchpad, GameObject gameObject)
    {
        float radius = gameObject.transform.position.x;

        //   Debug.Log(a);
        float ang = 1;
        Vector3 pos;
        pos.x = origin.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = origin.y;
        pos.z = origin.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
    
        return pos;
    }
}
