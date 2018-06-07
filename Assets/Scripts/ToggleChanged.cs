using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ToggleChanged : MonoBehaviour {

    public GameObject contentSkills;
    public GameObject contentLocation;
    public GameObject contentScrollView;
    public GameObject profileCard;
    public TextMesh textMesh;
    int tmp = 0;
    bool isEmpty;

    string url = "http://192.168.43.113:3000/users/filter?";
    //string url = "http://localhost:3000/users/filter?";
	// Use this for initialization
	void Start () {
        isEmpty = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleValueChanged(bool on){
        isEmpty = true;
        Toggle[] togglesFilter = contentSkills.GetComponentsInChildren<Toggle>();
        foreach(Toggle toggle in togglesFilter){
            if(toggle.isOn){
                isEmpty = false;
                GameObject label = toggle.transform.Find("Label").gameObject;
                var labelText = label.GetComponent<Text>();
                if (!url.Contains(labelText.text))
                {
                    url += "skill=" + labelText.text + "&";
                }
            }
        }
        Toggle[] togglesLocation = contentLocation.GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in togglesLocation)
        {
            if (toggle.isOn)
            {
                isEmpty = false;
                GameObject label = toggle.transform.Find("Label").gameObject;
                var labelText = label.GetComponent<Text>();
                if (!url.Contains(labelText.text))
                {
                    url += "location=" + labelText.text + "&";
                }
            }
        }
        if (isEmpty)
        {
            StartCoroutine(Init());
        } else {
            StartCoroutine(MakeRequest());
        }
    }

    IEnumerator Init(){
        using (WWW www = new WWW("http://192.168.43.113:3000/users"))
        {
            yield return www;
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

                string tmpStar = "";
                int nbStar = 0;
                GameObject skill1Star = item.transform.Find("Profile Panel/Skill/Rate").gameObject;
                var labelTextSkill1Star = skill1Star.GetComponent<Text>();
                nbStar = int.Parse(profil["skills"][0]["rate"].ToString());
                tmpStar = MakeStar(nbStar);
                labelTextSkill1Star.text = tmpStar;

                GameObject skill2Star = item.transform.Find("Profile Panel/Skill 2/Rate").gameObject;
                var labelTextSkill2Star = skill2Star.GetComponent<Text>();
                nbStar = int.Parse(profil["skills"][1]["rate"].ToString());
                tmpStar = MakeStar(nbStar);
                labelTextSkill2Star.text = tmpStar;

                GameObject skill3Star = item.transform.Find("Profile Panel/Skill 3/Rate").gameObject;
                var labelTextSkill3Star = skill3Star.GetComponent<Text>();
                nbStar = int.Parse(profil["skills"][2]["rate"].ToString());
                tmpStar = MakeStar(nbStar);
                labelTextSkill3Star.text = tmpStar;
            }
            profileCard.SetActive(false);
        }
    }

    IEnumerator MakeRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            url = "http://192.168.43.113:3000/users/filter?";
            if (www.isNetworkError || www.isHttpError)
            {
                textMesh.text = "Code : " + www.responseCode.ToString() + " " + www.error.ToString();
            }
            else
            {
                string text = www.downloadHandler.text;
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
            StopAllCoroutines();
        }

    }

    string MakeStar(int nbStar)
    {
        string Tmp = "";
        for (int i = 0; i < nbStar; i++)
        {
            Tmp += "★";
        }
        for (int i = 0; i < 5 - nbStar; i++)
        {
            Tmp += "☆";
        }
        return Tmp;
    }
}
