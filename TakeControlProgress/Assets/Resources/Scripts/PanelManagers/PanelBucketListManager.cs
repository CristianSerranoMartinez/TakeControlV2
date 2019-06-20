using UnityEngine.UI;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class PanelBucketListManager : MonoBehaviour {
    [SerializeField]
    private Toggle[] arrayToggles;

    [SerializeField]
    private Text textLogError;

    BucketList bucketList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { OnPressButtonOutPanel(); }
    }

    private void OnEnable()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.GetReference("BucketList/" + Session.auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task => {

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


                bucketList = JsonUtility.FromJson<BucketList>(snapshot.GetRawJsonValue());

                arrayToggles[0].isOn = bucketList.one;
                arrayToggles[1].isOn = bucketList.two;
                arrayToggles[2].isOn = bucketList.three;
                arrayToggles[3].isOn = bucketList.four;
                arrayToggles[4].isOn = bucketList.five;
                arrayToggles[5].isOn = bucketList.six;
                arrayToggles[6].isOn = bucketList.seven;
                arrayToggles[7].isOn = bucketList.eight;
                arrayToggles[8].isOn = bucketList.nine;
                arrayToggles[9].isOn = bucketList.ten;
            }
        });
    }


    public void OnPressButtonAccept()
    {
        bucketList = new BucketList(arrayToggles[0].isOn,
            arrayToggles[1].isOn,
            arrayToggles[2].isOn,
            arrayToggles[3].isOn,
            arrayToggles[4].isOn,
            arrayToggles[5].isOn,
            arrayToggles[6].isOn,
            arrayToggles[7].isOn,
            arrayToggles[8].isOn,
            arrayToggles[9].isOn
            );
        UpdateBase();
    }

    public void OnPressButtonOutPanel()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void UpdateBase()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");//Here you should change for you base data link!!!!

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        string json = JsonUtility.ToJson(bucketList);
        reference.Child("BucketList").Child(Session.auth.CurrentUser.UserId).SetRawJsonValueAsync(json).ContinueWith(task => {

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
