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

    public bool[] boolArray = new bool[2];

    // Update is called once per frame
    void Update()
    {
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


    public void OnPressButtonRegister()
    {
        switch (idWorkShops)
        {
            case 0: { boolArray[0] = true; } break;
            case 1: { boolArray[1] = true; } break;
            default: break;

        }

        workShopsSubcriptions = new WorkShopsSubcriptions(boolArray[0], boolArray[1]);

        UpdateBase();
    }

    private void OnEnable()
    {
        buttonDoQuestion.SetActive(false);
        buttonRegister.SetActive(false);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.GetReference("WorkShopsSubcriptions/" + Session.auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task => {

            if (task.IsFaulted)
            {
                //panelLoading.SetActive(false);
                AggregateException exception = task.Exception as AggregateException;
                if (exception != null)
                {
                    FirebaseException fireBaseException = null;
                    foreach (Exception e in exception.InnerExceptions)
                    {
                        //panelLoading.SetActive(false);
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

                switch (idWorkShops)
                {
                    case 0: { buttonRegister.SetActive(!boolArray[0]); buttonDoQuestion.SetActive(boolArray[0]); } break;
                    case 1: { buttonRegister.SetActive(!boolArray[1]); buttonDoQuestion.SetActive(boolArray[1]); } break;
                }
            }
        });
    }


    public void UpdateBase()
    {
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
            }
        });
    }
}
