using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeExplodedSkills : MonoBehaviour {
    public GameObject camera;
    public GameObject skill;
    public int numberOfSkills;
    public int radius;

    public int maxNumberOfSkills = 38;
    // Use this for initialization
    void Start()
    {
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
                /*
                item.name = skill;
                GameObject label = item.transform.Find("Label").gameObject;
                var labelText = xc  label.GetComponent<Text>();
                labelText.text = skill;*/

                count++;
                if (count >= numberOfSkills)
                {

                    break;
                }
            }
        }
        skill.SetActive(false);
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
}
