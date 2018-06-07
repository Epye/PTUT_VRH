using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class initView : MonoBehaviour {

	// Use this for initialization
    //private string url = "http://192.168.43.113:3000/users";
    private string url = "http://localhost:3000/users";
    public GameObject content;
    public GameObject profileCard;
    // Use this for initialization
    IEnumerator Start()
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            string text = www.text;

            JObject profiles = JObject.Parse(text);
            foreach(var profil in profiles["profiles"]){
                GameObject item = Instantiate(profileCard);
                item.transform.SetParent(content.transform);

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
	
	// Update is called once per frame
	void Update () {
		
	}

    string MakeStar(int nbStar){
        string tmp = "";
        for (int i = 0; i < nbStar; i++){
            tmp += "★";
        }
        for (int i = 0; i < 5 - nbStar; i++)
        {
            tmp += "☆";
        }
        return tmp;
    }
}
