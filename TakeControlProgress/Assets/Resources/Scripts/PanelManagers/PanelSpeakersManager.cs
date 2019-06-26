using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSpeakersManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] arrayPanels;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) { OnPressButtonOutPanel(); }
    }

    private void OnEnable()
    {
        SetInitialState();
    }

    public void OnPressButtonOutPanel()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void OnPressMasterDetail()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelMasterDetailSpeakers": gameObject.SetActive(true); break;
            }
        }
    }

    public void OnPressButtonLookFor()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "InputFieldLookFor": gameObject.SetActive(true); break;
            }
        }
    }

    private void SetInitialState()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "InputFieldLookFor": gameObject.SetActive(false); break;

                    /**
                     * Escribir todo lo que se necesite inicializar
                     */
            }
        }
    }
}
