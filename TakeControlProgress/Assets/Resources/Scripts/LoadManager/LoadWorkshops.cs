using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadWorkshops : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform tContent;
    private RectTransform content;
    string path;
    string jsonString;
    private workShopsList workShopList = new workShopsList();
    private Sprite[] imgSpeakers;


    // Use this for initialization
    void Start()
    {
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        imgSpeakers = Resources.LoadAll<Sprite>("UI/Workshops");
        int desplazador = 0;

        //cargar archivo speakers.json
        TextAsset asset = Resources.Load("workshops") as TextAsset;
        workShopList = JsonUtility.FromJson<workShopsList>(asset.text);

        foreach (workshop s in workShopList.workshops )
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

            but.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(s.name, s.title, s.time, s.desc, imgSpeakers[index], int.Parse(s.id)));
        }

        //cambiar tamaño del content
        content = tContent.GetComponent<RectTransform>();
        desplazador += 100;
        content.sizeDelta = new Vector2(content.sizeDelta.x, desplazador);
    }

    private void OnButtonClick(string name, string title, string time, string desc, Sprite foto, int id)
    {
        //abrir detalles workshop
        GameObject.Find("PanelWorkShops").GetComponent<PanelWorkShopsManager>().OnPressMasterDetail();
        GameObject.Find("PanelMasterDetailWorkShops").transform.GetChild(2).GetComponent<Image>().sprite = foto;
        GameObject.Find("PanelMasterDetailWorkShops").transform.GetChild(3).GetComponent<Text>().text = name;
        GameObject.Find("PanelMasterDetailWorkShops").transform.GetChild(4).GetComponent<Text>().text = title;
        GameObject.Find("PanelMasterDetailWorkShops").transform.GetChild(5).GetComponent<Text>().text = desc;
        GameObject.Find("PanelMasterDetailWorkShops").transform.GetChild(6).GetComponent<Text>().text = time;
        GameObject.Find("PanelMasterDetailWorkShops").GetComponent<PanelMasterDetailManagerWorkShops>().idWorkShops = id;
    }

}

[System.Serializable]
public class workshop
{
    public string id;
    public string name;
    public string title;
    public string time;
    public string desc;

}

[System.Serializable]
public class workShopsList
{
    public List<workshop> workshops = new List<workshop>();
}






