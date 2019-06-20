using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMainManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] arrayPanels;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnPressButtonSettings()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelSettings": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
    }

    public void OnPressButtonUser()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelUser": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
    }

    public void OnPressButtonSpeakers()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelSpeakers": gameObject.SetActive(true); break;
            }
        }
    }

    public void OnPressButtonWorkShop()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelWorkShops": gameObject.SetActive(true); break;
            }
        }
    }

    public void OnPressButtonPanelIntroduction()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelIntroduction": gameObject.SetActive(true); break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (Session.auth != null)
            Session.auth.SignOut();
    }
}
