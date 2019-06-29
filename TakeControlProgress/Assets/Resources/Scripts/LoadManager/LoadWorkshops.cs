using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadWorkshops : MonoBehaviour
{
    [SerializeField]
    string nameWorkShops;
    [SerializeField]
    string titleWorkShops;
    [SerializeField]
    string timeWorkShops;
    [SerializeField]
    string descWorkShops;
    [SerializeField]
    Sprite panelWorkShops;
    [SerializeField]
    int idWorkShops;

    public void OnButtonClick()
    {

        GameObject.Find("PanelWorkShops").GetComponent<PanelWorkShopsManager>().OnPressMasterDetail();
        GameObject.Find("PanelMasterDetailWorkShops").GetComponent<Image>().sprite = panelWorkShops;
        GameObject.Find("PanelMasterDetailWorkShops").transform.GetChild(2).GetComponent<Text>().text = nameWorkShops;
        GameObject.Find("PanelMasterDetailWorkShops").transform.GetChild(4).GetComponent<Text>().text = titleWorkShops;
        GameObject.Find("PanelMasterDetailWorkShops").transform.GetChild(5).GetComponent<Text>().text = descWorkShops;
        GameObject.Find("PanelMasterDetailWorkShops").transform.GetChild(6).GetComponent<Text>().text = timeWorkShops;
        GameObject.Find("PanelMasterDetailWorkShops").GetComponent<PanelMasterDetailManagerWorkShops>().idWorkShops = idWorkShops;
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






