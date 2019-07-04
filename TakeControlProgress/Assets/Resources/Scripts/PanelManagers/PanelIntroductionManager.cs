using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelIntroductionManager : MonoBehaviour {
    [SerializeField]
    GameObject[] arrayPanels;

    public void OnPressButtonEntrar()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelMain": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (Session.auth != null)
            Session.auth.SignOut();
    }
}
