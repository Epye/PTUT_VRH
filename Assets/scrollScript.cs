using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollScript : MonoBehaviour
{
    public RectTransform content;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Touch.One))
        {
            Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            content.anchoredPosition = new Vector2(content.anchoredPosition.x + primaryTouchpad.x * 50, content.anchoredPosition.y);
        }
    }
}
