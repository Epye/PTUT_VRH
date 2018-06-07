using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class InitializeExplodedSkills : MonoBehaviour {
    public GameObject camera;
    public GameObject skill;
    public GameObject profile;
    public int radius;

    private int maxNumberOfSkills = 38;
    // Use this for initialization
    IEnumerator Start()
    {
        using (WWW www = new WWW("http://192.168.43.113:3000/users/" + OnClickHandler.clicked))
        {
            yield return www;
            string text = www.text;

            JObject profiles = JObject.Parse(text);

            GameObject surname = profile.transform.Find("Profile Panel/Name").gameObject;
            var labelTextName = surname.GetComponent<Text>();
            labelTextName.text = profiles["profile"]["name"].ToString() + " " + profiles["profile"]["surname"].ToString();

            GameObject industry = profile.transform.Find("Profile Panel/Industry").gameObject;
            var labelTextIndustry = industry.GetComponent<Text>();
            labelTextIndustry.text = profiles["profile"]["industry"].ToString();

            GameObject location = profile.transform.Find("Profile Panel/Location").gameObject;
            var labelTextLocation = location.GetComponent<Text>();
            labelTextLocation.text = profiles["profile"]["location"].ToString();

            GameObject age = profile.transform.Find("Profile Panel/Birthdate").gameObject;
            var labelTextAge = age.GetComponent<Text>();
            labelTextAge.text = profiles["profile"]["age"].ToString();

            int numberOfSkills = 0;
            foreach (var tmp in profiles["profile"]["skills"])
            {
                numberOfSkills++;
            }
            int count = 0;
            int initj = -2;
            int maxj = 3;
            if (numberOfSkills <= 26)
            {
                initj = -1;
                maxj = 2;
                maxNumberOfSkills = 26;
            }
            skill.SetActive(true);
            for (int j = initj; j < maxj; j++)
            {
                int maxSkillsOnCircle = 0;


                switch (j)
                {
                    case -2:
                        maxSkillsOnCircle = numberOfSkills * 6 / maxNumberOfSkills;
                        break;
                    case -1:
                        maxSkillsOnCircle = numberOfSkills * 8 / maxNumberOfSkills;
                        break;
                    case 0:

                        int numberOfLastSkills = numberOfSkills - ((int)numberOfSkills * 10 / maxNumberOfSkills + 2 * ((int)numberOfSkills * 8 / maxNumberOfSkills) + 2 * ((int)numberOfSkills * 6 / maxNumberOfSkills));
                        if (numberOfSkills <= 26)
                        {
                            numberOfLastSkills = numberOfSkills - ((int)numberOfSkills * 10 / maxNumberOfSkills + 2 * ((int)numberOfSkills * 8 / maxNumberOfSkills));
                        }
                        maxSkillsOnCircle = numberOfSkills * 10 / maxNumberOfSkills + numberOfLastSkills;
                        break;
                    case 1:
                        maxSkillsOnCircle = numberOfSkills * 8 / maxNumberOfSkills;
                        break;
                    case 2:
                        maxSkillsOnCircle = numberOfSkills * 6 / maxNumberOfSkills;
                        break;
                }
                for (int i = 0; i < maxSkillsOnCircle; i++)
                {

                    Vector3 origin = camera.gameObject.transform.position;


                    //Vector3 onSphere = Random.onUnitSphere * radius;
                    Vector3 onSphere = RandomCircle(origin, maxSkillsOnCircle, j, i);

                    GameObject item = Instantiate(skill, onSphere, Quaternion.identity) as GameObject;
                    item.transform.parent = skill.transform.parent;

                    item.transform.position = onSphere;
                    item.transform.LookAt(camera.transform);
                    item.transform.localRotation = item.transform.rotation * Quaternion.Euler(0, 180, 0);
                    item.transform.localScale = new Vector3((float)0.4, (float)0.4, (float)0.4);

                    GameObject skillObject = item.transform.Find("Skill Panel/Name").gameObject;
                    var labelTextSkill = skillObject.GetComponent<Text>();
                    labelTextSkill.text = profiles["profile"]["skills"][count]["skill"].ToString();

                    string tmpStar = "";
                    int nbStar = 0;
                    GameObject skillStar = item.transform.Find("Skill Panel/Rate").gameObject;
                    var labelTextSkillStar = skillStar.GetComponent<Text>();
                    nbStar = int.Parse(profiles["profile"]["skills"][count]["rate"].ToString());
                    tmpStar = MakeStar(nbStar);
                    labelTextSkillStar.text = tmpStar;


                    count++;
                    if (count >= numberOfSkills)
                    {
                        break;
                    }
                }
            }
            skill.SetActive(false);
        }
    }

    Vector3 RandomCircle(Vector3 center, int maxSkillOnCircle, int j, int i)
    {
        
        int a = 360 / maxSkillOnCircle * i;
        //   Debug.Log(a);
        float ang = a;
        if(j == 0)
        {
            a = 270 / maxSkillOnCircle * i;
            ang = a + 60;
        }
        Vector3 pos;
        pos.x = center.x + (radius - Mathf.Abs(j) * 100) * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + j * 300;
        pos.z = center.z + (radius - Mathf.Abs(j) * 100) * Mathf.Cos(ang * Mathf.Deg2Rad);

        return pos;
    }

    // Update is called once per frame
    void Update () {
		
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
