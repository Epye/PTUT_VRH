using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeSearchSkills : MonoBehaviour {

    public GameObject content;
    public GameObject toggle;

    private static ArrayList skills = new ArrayList(new string[] {"PHP", "Java", "Javascript", "Comptabilité", "C++", "Gestion", "SQL", "MongoDB", "UnitTest", "Design Pattern", "Relationnel", "NodeJs", "Unity", "Réalité Virtuelle", "Réalité Augmentée", "Swift", "Objective-C", "Android", "Kotlin", "Méthode Agile", "Symfony", "HTML/CSS", "React", "Dart", "Flutter", "Xamarin", "C#", "Travail d'équipe", "Ergonomie", "Marketing", "Design", "Embarqué", "Linux",
    "DevOps", "Cubernets", "Docker", "Machine Learning", "Deep Learning" });

    // Use this for initialization
    void Start () {
       
        foreach (string skill in skills)
        {
            GameObject item = Instantiate(toggle);
            item.transform.SetParent(content.transform);

            item.transform.localRotation = toggle.transform.localRotation;
            item.transform.localScale = toggle.transform.localScale;
            item.transform.localPosition = Vector3.zero;

            item.name = skill;
            GameObject label = item.transform.Find("Label").gameObject;
            var labelText = label.GetComponent<Text>();
            labelText.text = skill;
        }
        toggle.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
