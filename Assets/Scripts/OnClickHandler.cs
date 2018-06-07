using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OnClickHandler : MonoBehaviour, IPointerDownHandler{
    public static string clicked;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.clickCount == 2){
            clicked = this.name;
            SceneManager.LoadScene("Scene_Profile", LoadSceneMode.Single);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
