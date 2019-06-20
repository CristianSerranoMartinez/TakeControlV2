using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PanelUserManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] arrayPanels;

    [SerializeField]
    private Text username;

    [SerializeField]
    private Text email;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { OnPressButtonOutPanel(); }
    }

    private void OnEnable()
    {
        username.text = Session.currentUser.username;

        email.text = Session.currentUser.email;
    }

    public void OnPressButtonOutPanel()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void OnPressButtonBucketList() 
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelBucketList": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
        OnPressButtonOutPanel();
    }

    public void OnPressButtonWorkShopList()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelWorkShopsTaken": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
        OnPressButtonOutPanel();
    }

    public void OnPressButtonSpeakersList()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelConversationsTaken": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
        OnPressButtonOutPanel();
    }

    private void OnApplicationQuit()
    {
        if (Session.auth != null)
            Session.auth.SignOut();
    }
}
