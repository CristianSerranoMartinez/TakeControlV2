using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpeakers : MonoBehaviour {
    [SerializeField]
    string nameSpeaker;
    [SerializeField]
    string titleSpeaker;
    [SerializeField]
    string timeSpeaker;
    [SerializeField]
    string descSpeaker;
    [SerializeField]
    Sprite panelSpeaker;
    [SerializeField]
    int idSpeaker;

    public void OnButtonClick()
    {

        GameObject.Find("PanelSpeakers").GetComponent<PanelSpeakersManager>().OnPressMasterDetail();
        GameObject.Find("PanelMasterDetailSpeakers").GetComponent<Image>().sprite = panelSpeaker;
        GameObject.Find("PanelMasterDetailSpeakers").transform.GetChild(2).GetComponent<Text>().text = nameSpeaker;
        GameObject.Find("PanelMasterDetailSpeakers").transform.GetChild(4).GetComponent<Text>().text = titleSpeaker;
        GameObject.Find("PanelMasterDetailSpeakers").transform.GetChild(5).GetComponent<Text>().text = descSpeaker;
        GameObject.Find("PanelMasterDetailSpeakers").transform.GetChild(6).GetComponent<Text>().text = timeSpeaker;
        GameObject.Find("PanelMasterDetailSpeakers").GetComponent<PanelMasterDetailManagerSpeakers>().idSpeaker = idSpeaker;

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
