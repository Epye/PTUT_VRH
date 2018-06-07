using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class InitializeWebProfiles : MonoBehaviour
{

    public GameObject camera;
    public GameObject profile;
    public int radius;
    private int maxNumberOfPofiles = 40;
    // Use this for initialization
    IEnumerator Start()
    {
        using (WWW www = new WWW("http://192.168.43.113:3000/users/" + OnClickHandler.clicked))
        {
            yield return www;
            string text = www.text;

            JObject profiles = JObject.Parse(text);
            int numberOfProfile = 0;
            foreach (var tmp in profiles["profile"]["contacts"])
            {
                numberOfProfile++;
            }
            int count = 0;
            int initj = -2;
            int maxj = 3;
            if (numberOfProfile <= 28)
            {
                initj = -1;
                maxj = 2;
                maxNumberOfPofiles = 28;
            }
            profile.SetActive(true);
            for (int j = initj; j < maxj; j++)
            {

                int maxProfileOnCircle = 0;

                switch (j)
                {
                    case -2:
                        maxProfileOnCircle = numberOfProfile * 6 / maxNumberOfPofiles;
                        break;
                    case -1:
                        maxProfileOnCircle = numberOfProfile * 8 / maxNumberOfPofiles;
                        break;
                    case 0:
                        int numberOfLastPofiles = numberOfProfile - ((int)numberOfProfile * 12 / maxNumberOfPofiles + 2 * ((int)numberOfProfile * 8 / maxNumberOfPofiles) + 2 * ((int)numberOfProfile * 6 / maxNumberOfPofiles));
                        if (maxNumberOfPofiles <= 28)
                        {
                            numberOfLastPofiles = numberOfProfile - ((int)numberOfProfile * 12 / maxNumberOfPofiles + 2 * ((int)numberOfProfile * 8 / maxNumberOfPofiles));
                        }
                        maxProfileOnCircle = (int)numberOfProfile * 12 / maxNumberOfPofiles + numberOfLastPofiles;
                        break;
                    case 1:
                        maxProfileOnCircle = numberOfProfile * 8 / maxNumberOfPofiles;
                        break;
                    case 2:
                        maxProfileOnCircle = numberOfProfile * 6 / maxNumberOfPofiles;
                        break;
                }
                for (int i = 0; i < maxProfileOnCircle; i++)
                {
                    Vector3 origin = camera.gameObject.transform.position;


                    //Vector3 onSphere = Random.onUnitSphere * radius;
                    Vector3 onSphere = RandomCircle(origin, maxProfileOnCircle, j, i);

                    GameObject item = Instantiate(profile, onSphere, Quaternion.identity) as GameObject;
                    item.transform.parent = profile.transform.parent;

                    item.transform.position = onSphere;
                    item.transform.LookAt(camera.transform);
                    item.transform.localRotation = item.transform.rotation * Quaternion.Euler(0, 180, 0);
                    item.transform.localScale = profile.transform.localScale;

                    GameObject surname = item.transform.Find("Profile Panel/Name").gameObject;
                    var labelTextName = surname.GetComponent<Text>();
                    labelTextName.text = profiles["profile"]["contacts"][count]["name"].ToString() + " " + profiles["profile"]["contacts"][count]["surname"].ToString();

                    GameObject age = item.transform.Find("Profile Panel/Birthdate").gameObject;
                    var labelTextAge = age.GetComponent<Text>();
                    labelTextAge.text = profiles["profile"]["contacts"][count]["age"].ToString();

                    GameObject skill1 = item.transform.Find("Profile Panel/Skill/Name").gameObject;
                    var labelTextSkill1 = skill1.GetComponent<Text>();
                    labelTextSkill1.text = profiles["profile"]["contacts"][count]["skills"][0]["skill"].ToString();

                    GameObject skill2 = item.transform.Find("Profile Panel/Skill 2/Name").gameObject;
                    var labelTextSkill2 = skill2.GetComponent<Text>();
                    labelTextSkill2.text = profiles["profile"]["contacts"][count]["skills"][1]["skill"].ToString();

                    GameObject skill3 = item.transform.Find("Profile Panel/Skill 3/Name").gameObject;
                    var labelTextSkill3 = skill3.GetComponent<Text>();
                    labelTextSkill3.text = profiles["profile"]["contacts"][count]["skills"][2]["skill"].ToString();

                    string tmpStar = "";
                    int nbStar = 0;
                    GameObject skill1Star = item.transform.Find("Profile Panel/Skill/Rate").gameObject;
                    var labelTextSkill1Star = skill1Star.GetComponent<Text>();
                    nbStar = int.Parse(profiles["profile"]["contacts"][count]["skills"][0]["rate"].ToString());
                    tmpStar = MakeStar(nbStar);
                    labelTextSkill1Star.text = tmpStar;

                    GameObject skill2Star = item.transform.Find("Profile Panel/Skill 2/Rate").gameObject;
                    var labelTextSkill2Star = skill2Star.GetComponent<Text>();
                    nbStar = int.Parse(profiles["profile"]["contacts"][count]["skills"][1]["rate"].ToString());
                    tmpStar = MakeStar(nbStar);
                    labelTextSkill2Star.text = tmpStar;

                    GameObject skill3Star = item.transform.Find("Profile Panel/Skill 3/Rate").gameObject;
                    var labelTextSkill3Star = skill3Star.GetComponent<Text>();
                    nbStar = int.Parse(profiles["profile"]["contacts"][count]["skills"][2]["rate"].ToString());
                    tmpStar = MakeStar(nbStar);
                    labelTextSkill3Star.text = tmpStar;

                    count++;
                    if (count >= numberOfProfile)
                    {
                        break;
                    }
                }
            }
            profile.SetActive(false);
        }
    }


    Vector3 RandomCircle(Vector3 center, int maxProfileOnCircle, int j, int i)
    {
        int a = 360 / maxProfileOnCircle * i;
     //   Debug.Log(a);
        float ang = a;
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
