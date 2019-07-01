using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelConfirmSpeakerQuestion : MonoBehaviour {

    [SerializeField]
    private GameObject panelLoading;
    [SerializeField]
    private Text textLog;
    [SerializeField]
    private InputField inputFieldQuestion;

    SpeakerQuestions speakerQuestions;

    public int intSpeakerId;
    public string question;
    public string username;

    private void Update()
    {
        panelLoading.transform.GetChild(0).transform.Rotate(0, 0, 5);
    }

    public void OnPressButtonCancel()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inputFieldQuestion.text = "";
    }

    public void OnPressButtonAcccept()
    {
        if (inputFieldQuestion.text != "" && inputFieldQuestion.text != " ")
        {
            panelLoading.SetActive(true);

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");//Here you should change for you base data link!!!!
            // Get the root reference location of the database.
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            question = inputFieldQuestion.text;
            speakerQuestions = new SpeakerQuestions(intSpeakerId, question, username);
            string json = JsonUtility.ToJson(speakerQuestions);
            reference.Child("SpeakerQuestions").Child(Session.auth.CurrentUser.UserId).SetRawJsonValueAsync(json).ContinueWith(task =>
            {

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
                            textLog.text = fireBaseException.Message;
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
                            textLog.text = fireBaseException.Message;
                        }
                    }
                    return;
                }
                if (task.IsCompleted)
                {
                    panelLoading.SetActive(false);
                    textLog.text = "Done!";
                    gameObject.SetActive(false);
                }
            });
        }
    }

    public void SetValues(int intSpeakerId, string question, string username)
    {
        this.intSpeakerId = intSpeakerId;
        this.question = question;
        this.username = username;
    }
}
