using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSettingsManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] arrayPanels;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) { OnPressButtonOutPanel(); }
    }

    public void OnPressButtonLogOut() 
    {
        Session.auth.SignOut();
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelSignIn": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
    }

    public void OnPressButtonOutPanel()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
