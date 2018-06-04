using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeSearchLocations : MonoBehaviour {
    public GameObject content;
    public GameObject toggle;

    private static ArrayList locations = new ArrayList(new string[] { "Ain", "Aisne", "Allier", "Alpes de Haute-Provence", "Alpes-Maritimes", "Ardèche", "Ardennes", "Ariège", "Aube", "Aude", "Aveyron", "Bas-Rhin", "Bouches du Rhône", "Calvados", "Cantal", "Charente", "Charente Maritime", "Cher", "Corrèze", "Corse du Sud", "Côte d'Or", "Côtes d'Armor", "Creuse", "Deux-Sèvres", "Dordogne", "Doubs", "Drôme", "Essonne", "Eure", "Eure-et-Loir", "Finistère", "Gard", "Gers", "Gironde", "Haute-Corse", "Haute-Garonne", "Haute-Loire", "Haute-Marne", "Hautes-Alpes", "Haute-Saône", "Haute-Savoie", "Hautes-Pyrénées", "Haute-Vienne", "Haut-Rhin", "Hauts-de-Seine", "Hérault", "Ille-et-Vilaine", "Indre", "Indre-et-Loire", "Isère", "Jura", "Landes", "Loire", "Loire-Atlantique", "Loiret", "Loir-et-Cher", "Lot", "Lot-et-Garonne", "Lozère", "Maine-et-Loire", "Manche", "Marne", "Mayenne", "Meurthe-et-Moselle", "Meuse", "Morbihan", "Moselle", "Nièvre", "Nord", "Oise", "Orne", "Paris", "Pas-de-Calais", "Puy-de-Dôme", "Pyrénées-Atlantiques", "Pyrénées-Orientales", "Rhône", "Saône-et-Loire", "Sarthe", "Savoie", "Seine-et-Marne", "Seine-Maritime", "Seine-St-Denis", "Somme", "Tarn", "Tarn-et-Garonne", "Territoire-de-Belfort", "Val-de-Marne", "Val-d'Oise", "Var", "Vaucluse", "Vendée", "Vienne", "Vosges", "Yonne", "Yvelines" });

    // Use this for initialization
    void Start()
    {

        foreach (string location in locations)
        {
            GameObject item = Instantiate(toggle);
            item.transform.parent = content.transform;


            item.transform.localRotation = toggle.transform.localRotation;
            item.transform.localScale = toggle.transform.localScale;
            item.transform.localPosition = Vector3.zero;

            item.name = location;
            GameObject label = item.transform.Find("Label").gameObject;
            var labelText = label.GetComponent<Text>();
            labelText.text = location;
        }
        toggle.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
