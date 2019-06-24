using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpeakers : MonoBehaviour {

	public GameObject buttonPrefab;
	public Transform tContent;
    private RectTransform content;
    string path;
    string jsonString;
    private speakersList speakerList = new speakersList();
    private Sprite[] imgSpeakers;


    // Use this for initialization
    void Start () {
        RefreshDisplay();
    }

    private void RefreshDisplay() {

        imgSpeakers = Resources.LoadAll<Sprite>("UI/Speakers");
        int desplazador = 0;
       
        //cargar archivo speakers.json
        TextAsset asset = Resources.Load("speakers") as TextAsset;
        speakerList = JsonUtility.FromJson<speakersList>(asset.text);

        foreach (speaker s in speakerList.speakers)
        {
            //creamos boton
            GameObject but = Instantiate(buttonPrefab) as GameObject;

            desplazador += 300;

            //añadirlo al content y cambiar tamaño
            but.transform.SetParent(tContent.transform);
            but.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            but.SetActive(true);
            but.transform.GetChild(0).GetComponent<Text>().text = s.time;
            but.transform.GetChild(1).GetComponent<Text>().text = s.title;
            but.transform.GetChild(2).GetComponent<Text>().text = s.name;

            //Obtener indice que coincida con el nombre de la imagen y el id del speaker
            int index = Array.FindIndex(imgSpeakers, img => img.name == s.id);

           // but.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(s.name, s.title, s.time, s.desc, imgSpeakers[index], (int)id));  //Haz el cast de string a int id porfas gustavo :D >:v

        }

        //cambiar tamaño del content
        content = tContent.GetComponent<RectTransform>();
        desplazador += 100;
        content.sizeDelta = new Vector2(content.sizeDelta.x, desplazador);

    }

    private void OnButtonClick(string name, string title, string time, string desc, Sprite foto, int id)
    {

        GameObject.Find("PanelSpeakers").GetComponent<PanelSpeakersManager>().OnPressMasterDetail();
        GameObject.Find("PanelMasterDetail").transform.GetChild(2).GetComponent<Image>().sprite = foto;
        GameObject.Find("PanelMasterDetail").transform.GetChild(3).GetComponent<Text>().text = name;
        GameObject.Find("PanelMasterDetail").transform.GetChild(4).GetComponent<Text>().text = title;
        GameObject.Find("PanelMasterDetail").transform.GetChild(5).GetComponent<Text>().text = desc;
        GameObject.Find("PanelMasterDetail").transform.GetChild(6).GetComponent<Text>().text = time;
        GameObject.Find("PanelMasterDetail").GetComponent<PanelMasterDetailManager>().idSpeaker = id;

    }

}

[System.Serializable]
public class speaker
{
    public string id;
    public string name;
    public string title;
    public string time;
    public string desc;

}

[System.Serializable]
public class speakersList
{
    public List<speaker> speakers = new List<speaker>();
}
