using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelWorkShopsTakenManager : MonoBehaviour {
    [SerializeField]
    private Toggle[] arrayToggles;

    [SerializeField]
    private Text textLogError;

    WorkShopsSubcriptions workShopsSubcriptions;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { OnPressButtonOutPanel(); }
    }

    public void OnPressButtonOutPanel()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void OnPressButtonAccept()
    {
        workShopsSubcriptions = new WorkShopsSubcriptions(arrayToggles[0].isOn,
            arrayToggles[1].isOn
            );
        UpdateBase();
    }

    private void OnEnable()
    {
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
                arrayToggles[0].isOn = workShopsSubcriptions.one;
                arrayToggles[1].isOn = workShopsSubcriptions.two;
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
            if (task.IsCompleted)
            {
                textLogError.text = "Done!";
            }
        });
    }
}
