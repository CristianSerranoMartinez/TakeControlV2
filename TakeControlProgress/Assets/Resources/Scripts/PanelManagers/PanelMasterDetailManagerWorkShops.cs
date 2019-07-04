using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelMasterDetailManagerWorkShops : MonoBehaviour {
    [SerializeField]
    private GameObject[] arrayPanels;

    [SerializeField]
    private Text textLogError;

    [SerializeField]
    private GameObject buttonRegister;

    [SerializeField]
    private GameObject buttonDoQuestion;

    public int idWorkShops;

    WorkShopsSubcriptions workShopsSubcriptions;

    [SerializeField]
    private GameObject panelLoading;

    public bool[] boolArray = new bool[3];

    // Update is called once per frame
    void Update()
    {
        panelLoading.transform.GetChild(0).transform.Rotate(0, 0, 5);
        if (Input.GetKeyDown(KeyCode.Escape)) { OnPressButtonOutPanel(); }
    }

    public void OnPressButtonOutPanel()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void ClosePanel()
    {

        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelWorkShops": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
    }


    public void OnPressButtonRegister()
    {
        switch (idWorkShops)
        {
            case 0: { boolArray[0] = true; } break;
            case 1: { boolArray[1] = true; } break;
            case 2: { boolArray[2] = true; } break;
            default: break;

        }

        workShopsSubcriptions = new WorkShopsSubcriptions(boolArray[0], boolArray[1], boolArray[2]);

        UpdateBase();
    }

    private void OnEnable()
    {
        buttonDoQuestion.SetActive(false);
        buttonRegister.SetActive(false);
        panelLoading.SetActive(true);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.GetReference("WorkShopsSubcriptions").Child(Session.auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task => {

            if (task.IsFaulted)
            {
                
                AggregateException exception = task.Exception as AggregateException;
                if (exception != null)
                {
                    FirebaseException fireBaseException = null;
                    foreach (Exception e in exception.InnerExceptions)
                    {
                        fireBaseException = e as FirebaseException;
                        if (fireBaseException != null)
                            break;
                    }

                    if (fireBaseException != null)
                    {
                        Debug.LogError("FirebaseDatabase defaultInstance getReference encountered an error: " + fireBaseException.Message);
                        textLogError.text = fireBaseException.Message;
                    }
                }
                return;
            }

            if (task.IsCanceled)
            {
                AggregateException exception = task.Exception as AggregateException;
                if (exception != null)
                {
                    FirebaseException fireBaseException = null;
                    foreach (Exception e in exception.InnerExceptions)
                    {
                        fireBaseException = e as FirebaseException;
                        if (fireBaseException != null)
                            break;
                    }

                    if (fireBaseException != null)
                    {
                        Debug.LogError("FirebaseDatabase defaultInstance getReference encountered an error: " + fireBaseException.Message);
                        textLogError.text = fireBaseException.Message;
                    }
                }
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                workShopsSubcriptions = JsonUtility.FromJson<WorkShopsSubcriptions>(snapshot.GetRawJsonValue());
                /* En el one or two or tree son true y es igual a cualquiera de estos que est en verdarero cambiar el boton*/

                boolArray[0] = workShopsSubcriptions.one;
                boolArray[1] = workShopsSubcriptions.two;
                boolArray[2] = workShopsSubcriptions.three;

                switch (idWorkShops)
                {
                    case 0: { buttonRegister.SetActive(!boolArray[0]); buttonDoQuestion.SetActive(boolArray[0]); } break;
                    case 1: { buttonRegister.SetActive(!boolArray[1]); buttonDoQuestion.SetActive(boolArray[1]); } break;
                    case 2: { buttonRegister.SetActive(!boolArray[2]); buttonDoQuestion.SetActive(boolArray[2]); } break;
                }
                panelLoading.SetActive(false);
            }
        });
    }


    public void UpdateBase()
    {
        panelLoading.SetActive(true);
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");//Here you should change for you base data link!!!!

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        string json = JsonUtility.ToJson(workShopsSubcriptions);
        reference.Child("WorkShopsSubcriptions").Child(Session.auth.CurrentUser.UserId).SetRawJsonValueAsync(json).ContinueWith(task => {

            if (task.IsCanceled)
            {
                Debug.LogError("SetRawJsonValueAsync was canceled.");
                AggregateException exception = task.Exception as AggregateException;
                if (exception != null)
                {
                    FirebaseException fireBaseException = null;
                    foreach (Exception e in exception.InnerExceptions)
                    {
                        fireBaseException = e as FirebaseException;
                        if (fireBaseException != null)
                            break;
                    }

                    if (fireBaseException != null)
                    {
                        Debug.LogError("SetRawJsonValueAsync encountered an error: " + fireBaseException.Message);
                        textLogError.text = fireBaseException.Message;
                    }
                }
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SetRawJsonValueAsync was faulted.");
                AggregateException exception = task.Exception as System.AggregateException;
                if (exception != null)
                {
                    FirebaseException fireBaseException = null;
                    foreach (System.Exception e in exception.InnerExceptions)
                    {
                        fireBaseException = e as FirebaseException;
                        if (fireBaseException != null)
                            break;
                    }

                    if (fireBaseException != null)
                    {
                        Debug.LogError("SetRawJsonValueAsync encountered an error: " + fireBaseException.Message);
                        textLogError.text = fireBaseException.Message;
                    }
                }
                return;
            }
            if (task.IsCompleted)
            {
                buttonRegister.SetActive(false);
                buttonDoQuestion.SetActive(true);
                textLogError.text = "Done!";
                panelLoading.SetActive(false);
            }
        });
    }

    public void OnPressButtonQuestion()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelConfirmWorkShopQuestion": gameObject.SetActive(true); gameObject.GetComponent<PanelConfirmWorkShopQuestion>().SetValues(idWorkShops, "", Session.currentUser.username); break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (Session.auth != null)
            Session.auth.SignOut();
    }
}
