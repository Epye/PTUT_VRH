using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class ToggleChanged : MonoBehaviour {

    public GameObject contentSkills;
    public GameObject contentLocation;
    public GameObject contentScrollView;
    public GameObject profileCard;

    //string url = "http://192.168.43.113:3000/users/filter?";
    string url = "http://localhost:3000/users/filter?";
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleValueChanged(bool on){
        Toggle[] togglesFilter = contentSkills.GetComponentsInChildren<Toggle>();
        foreach(Toggle toggle in togglesFilter){
            if(toggle.isOn){
                GameObject label = toggle.transform.Find("Label").gameObject;
                var labelText = label.GetComponent<Text>();
                url += "skill=" + labelText.text + "&";
            }
        }
        Toggle[] togglesLocation = contentLocation.GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in togglesLocation)
        {
            if (toggle.isOn)
            {
                GameObject label = toggle.transform.Find("Label").gameObject;
                var labelText = label.GetComponent<Text>();
                url += "location=" + labelText.text + "&";
            }
        }
        StartCoroutine(makeRequest());
    }

    IEnumerator makeRequest(){
        using (WWW www = new WWW(url))
        {
            yield return www;
            url = "http://localhost:3000/users/filter?";
            string text = www.text;
            for (int i = 0; i < contentScrollView.transform.childCount; i++)
            {
                Destroy(contentScrollView.transform.GetChild(i).gameObject);
            }
            profileCard.SetActive(true);
            JObject profiles = JObject.Parse(text);
            foreach (var profil in profiles["profiles"])
            {
                GameObject item = Instantiate(profileCard);
                item.transform.SetParent(contentScrollView.transform);

                item.name = profil["name"].ToString() + " " + profil["surname"].ToString();

                GameObject surname = item.transform.Find("Profile Panel/Name").gameObject;
                var labelTextName = surname.GetComponent<Text>();
                labelTextName.text = profil["name"].ToString() + " " + profil["surname"].ToString();

                GameObject taux = item.transform.Find("Profile Panel/Pourcentage").gameObject;
                var labelTextTaux = taux.GetComponent<Text>();
                labelTextTaux.text = profil["taux"].ToString() + "%";

                GameObject age = item.transform.Find("Profile Panel/Birthdate").gameObject;
                var labelTextAge = age.GetComponent<Text>();
                labelTextAge.text = profil["age"].ToString();

                GameObject skill1 = item.transform.Find("Profile Panel/Skill/Name").gameObject;
                var labelTextSkill1 = skill1.GetComponent<Text>();
                labelTextSkill1.text = profil["skills"][0]["skill"].ToString();

                GameObject skill2 = item.transform.Find("Profile Panel/Skill 2/Name").gameObject;
                var labelTextSkill2 = skill2.GetComponent<Text>();
                labelTextSkill2.text = profil["skills"][1]["skill"].ToString();

                GameObject skill3 = item.transform.Find("Profile Panel/Skill 3/Name").gameObject;
                var labelTextSkill3 = skill3.GetComponent<Text>();
                labelTextSkill3.text = profil["skills"][2]["skill"].ToString();
            }
            profileCard.SetActive(false);
        }
    }
}
