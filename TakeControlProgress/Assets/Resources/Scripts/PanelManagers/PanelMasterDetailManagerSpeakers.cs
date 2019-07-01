using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine.UI;
using UnityEngine;

public class PanelMasterDetailManagerSpeakers : MonoBehaviour {

    [SerializeField]
    private GameObject[] arrayPanels;

    [SerializeField]
    private Text textLogError;

    [SerializeField]
    private GameObject buttonRegister;

    [SerializeField]
    private GameObject buttonDoQuestion;

    public int idSpeaker;

    SpeakersSubcriptions speakersSubcriptions;

    public bool[] boolArray = new bool[3];

    // Update is called once per frame
    void Update() {
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
                case "PanelSpeakers": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
    }


    public void OnPressButtonRegister()
    {
       switch (idSpeaker)
        {
            case 0: { boolArray[0] = true; } break;
            case 1: { boolArray[1] = true; } break;
            case 2: { boolArray[2] = true; } break;
            default: break;

        }

        speakersSubcriptions = new SpeakersSubcriptions(boolArray[0], boolArray[1], boolArray[2]);

        UpdateBase();
    }

    private void OnEnable()
    {
        buttonDoQuestion.SetActive(false);
        buttonRegister.SetActive(false);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.GetReference("SpeakersSubcriptions/" + Session.auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task => {

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

                speakersSubcriptions = JsonUtility.FromJson<SpeakersSubcriptions>(snapshot.GetRawJsonValue());
                /* En el one or two or tree son true y es igual a cualquiera de estos que est en verdarero cambiar el boton*/

                boolArray[0] = speakersSubcriptions.one;
                boolArray[1] = speakersSubcriptions.two;
                boolArray[2] = speakersSubcriptions.three;

                switch (idSpeaker)
                {
                    case 0: { buttonRegister.SetActive(!boolArray[0]); buttonDoQuestion.SetActive(boolArray[0]); } break;
                    case 1: { buttonRegister.SetActive(!boolArray[1]); buttonDoQuestion.SetActive(boolArray[1]); } break;
                    case 2: { buttonRegister.SetActive(!boolArray[2]); buttonDoQuestion.SetActive(boolArray[2]); } break;
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

        string json = JsonUtility.ToJson(speakersSubcriptions);
        reference.Child("SpeakersSubcriptions").Child(Session.auth.CurrentUser.UserId).SetRawJsonValueAsync(json).ContinueWith(task => {

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
                buttonRegister.SetActive(false);
                buttonDoQuestion.SetActive(true);
                textLogError.text = "Done!";
            }
        });
    }

    public void OnPressButtonQuestion()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelConfirmSpeakerQuestion": gameObject.SetActive(true); gameObject.GetComponent<PanelConfirmSpeakerQuestion>().SetValues(idSpeaker, "", Session.currentUser.username); break;
            }
        }
    }
}
