using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
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

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

            string key = reference.Child("SpeakerQuestions").Child(Session.auth.CurrentUser.UserId).Push().Key;
            question = inputFieldQuestion.text;
            SpeakerQuestions workShopsQuestions = new SpeakerQuestions(intSpeakerId, question, username);

            Dictionary<string, System.Object> entryValues = workShopsQuestions.ToDictionary();

            Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();

            childUpdates["/SpeakerQuestions/" + Session.auth.CurrentUser.UserId + "/" + key] = entryValues;

            reference.UpdateChildrenAsync(childUpdates);
            panelLoading.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void SetValues(int intSpeakerId, string question, string username)
    {
        this.intSpeakerId = intSpeakerId;
        this.question = question;
        this.username = username;
    }
}
