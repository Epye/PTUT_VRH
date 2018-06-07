using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeWebProfiles : MonoBehaviour {

    public GameObject camera;
    public GameObject profile;
    public int numberOfProfile;
    public int radius;
    public int maxNumberOfPofiles = 40;
    // Use this for initialization
    void Start () {
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
        for (int j = initj; j < maxj; j++) { 

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
                    int numberOfLastPofiles = numberOfProfile -((int)numberOfProfile * 12 / maxNumberOfPofiles + 2 * ((int)numberOfProfile * 8 / maxNumberOfPofiles) + 2 * ((int)numberOfProfile * 6 / maxNumberOfPofiles));
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
                /*
                item.name = skill;
                GameObject label = item.transform.Find("Label").gameObject;
                var labelText = xc  label.GetComponent<Text>();
                labelText.text = skill;*/

                count++;
                if (count >= numberOfProfile)
                {

                    break;
                }
            }
        }
        profile.SetActive(false);
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
}
