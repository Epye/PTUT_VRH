using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textScript : MonoBehaviour
{
    private string url = "https://opentdb.com/api.php?amount=10";
    // Use this for initialization
    IEnumerator Start()
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            string text = www.text;
            string[] values = text.Split('"');
            foreach (string value in values)
            {
                var theText = new GameObject();
                var textMesh = theText.AddComponent<TextMesh>();
                var meshRenderer = theText.AddComponent<MeshRenderer>();
                textMesh.text = value;
                theText.transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
