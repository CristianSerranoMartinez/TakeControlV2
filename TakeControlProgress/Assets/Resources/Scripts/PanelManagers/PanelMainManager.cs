using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelMainManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] arrayPanels;

    /*[SerializeField]
    Text textLogError;*/

    [SerializeField]
    GameObject gameObjectPanelLoading;


    private void Update()
    {
        gameObjectPanelLoading.transform.GetChild(0).transform.Rotate(0, 0, 5);
    }

    void OnEnable()
    {
        gameObjectPanelLoading.SetActive(true);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.GetReference("users/" + Session.auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task => {

            if (task.IsFaulted)
            {
                //gameObjectPanelLoading.SetActive(false);
                AggregateException exception = task.Exception as AggregateException;
                if (exception != null)
                {
                    FirebaseException fireBaseException = null;
                    foreach (Exception e in exception.InnerExceptions)
                    {
                       // gameObjectPanelLoading.SetActive(false);
                        fireBaseException = e as FirebaseException;
                        if (fireBaseException != null)
                            break;
                    }

                    if (fireBaseException != null)
                    {
                        Debug.LogError("FirebaseDatabase defaultInstance getReference encountered an error: " + fireBaseException.Message);
                       // textLogError.text = fireBaseException.Message;
                    }
                }
                return;
            }

            if (task.IsCanceled)
            {
               // gameObjectPanelLoading.SetActive(false);
                AggregateException exception = task.Exception as AggregateException;
                if (exception != null)
                {
                    FirebaseException fireBaseException = null;
                    foreach (Exception e in exception.InnerExceptions)
                    {
                        //gameObjectPanelLoading.SetActive(false);
                        fireBaseException = e as FirebaseException;
                        if (fireBaseException != null)
                            break;
                    }

                    if (fireBaseException != null)
                    {
                        Debug.LogError("FirebaseDatabase defaultInstance getReference encountered an error: " + fireBaseException.Message);
                        //textLogError.text = fireBaseException.Message;
                    }
                }
                return;
            }

            if (task.IsCompleted)
            {
                gameObjectPanelLoading.SetActive(false);
                DataSnapshot snapshot = task.Result;
                Session.currentUser = JsonUtility.FromJson<User>(snapshot.GetRawJsonValue());
                
            }
        });
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
