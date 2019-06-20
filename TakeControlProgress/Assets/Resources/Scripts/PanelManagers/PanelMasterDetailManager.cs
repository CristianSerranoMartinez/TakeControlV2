using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMasterDetailManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] arrayPanels;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) { GetComponent<Animator>().SetTrigger("Close"); }
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
