using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelSignUpManager : MonoBehaviour
{

    [SerializeField]
    private InputField inputFieldNickName;

    [SerializeField]
    private InputField inputFieldEmail;

    [SerializeField]
    private InputField inputFieldPassword;

    [SerializeField]
    private InputField inputFieldConfirmPassword;

    [SerializeField]
    private Text textLogError;

    [SerializeField]
    private Transform transformCircleLoading;

    [SerializeField]
    private GameObject panelLoading;

    [SerializeField]
    private GameObject[] arrayPanels;

    private void Update()
    {
        transformCircleLoading.transform.Rotate(0, 0, 5);
        if (Input.GetKeyDown(KeyCode.Escape)) { OnPressButtonBack(); }
    }

    private void OnEnable()
    {
        panelLoading.SetActive(false);
        textLogError.text = "";
    }

    public void OnPressButtonSignUp()
    {
        if (TestEmail.IsEmail(inputFieldEmail.text) && inputFieldNickName.text != "" && inputFieldNickName.text != " " && inputFieldEmail.text != "" && inputFieldEmail.text != "" && inputFieldEmail.text != " " && inputFieldEmail.text != " " && inputFieldConfirmPassword.text != "" && inputFieldConfirmPassword.text != " " && inputFieldPassword.text == inputFieldConfirmPassword.text)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                textLogError.text = "Error. Check internet connection!";
            }
            else
            {
                panelLoading.SetActive(true);
                FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task1 => {
                    var dependencyStatus = task1.Result;
                    if (dependencyStatus == DependencyStatus.Available)
                    {
                        Session.auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
                        Session.auth.CreateUserWithEmailAndPasswordAsync(inputFieldEmail.text, inputFieldPassword.text).ContinueWith(task2 => { //This line creates a new user
                            if (task2.IsCanceled)
                            {
                                panelLoading.SetActive(false);
                                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                                AggregateException exception = task2.Exception as AggregateException;
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
                                        Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + fireBaseException.Message);
                                        textLogError.text = fireBaseException.Message;
                                    }
                                }
                                return;
                            }
                            if (task2.IsFaulted)
                            {
                                panelLoading.SetActive(false);
                                Debug.LogError("CreateUserWithEmailAndPasswordAsync was faulted.");
                                AggregateException exception = task2.Exception as AggregateException;
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
                                        Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + fireBaseException.Message);
                                        textLogError.text = fireBaseException.Message;
                                    }
                                }
                                return;
                            }
                            if (task2.IsCompleted)
                            {
                                Session.auth.SignInWithEmailAndPasswordAsync(inputFieldEmail.text, inputFieldPassword.text).ContinueWith(task3 => {
                                    if (task3.IsCanceled)
                                    {
                                        panelLoading.SetActive(false);
                                        Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                                        AggregateException exception = task3.Exception as AggregateException;
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
                                                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + fireBaseException.Message);
                                                textLogError.text = fireBaseException.Message;
                                            }
                                        }
                                        return;
                                    }
                                    if (task3.IsFaulted)
                                    {
                                        panelLoading.SetActive(false);
                                        Debug.LogError("SignInWithEmailAndPasswordAsync was faulted.");
                                        AggregateException exception = task3.Exception as AggregateException;
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
                                                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + fireBaseException.Message);
                                                textLogError.text = fireBaseException.Message;
                                            }
                                        }
                                        return;
                                    }
                                    if (task3.IsCompleted)
                                    {
                                        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");//Here you should change for you base data link!!!!
                                        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                                        string json = JsonUtility.ToJson(new User(inputFieldEmail.text, inputFieldPassword.text, inputFieldNickName.text));// This line creates a reference in the database for the current user 
                                        reference.Child("users").Child(Session.auth.CurrentUser.UserId).SetRawJsonValueAsync(json).ContinueWith(task4 => {
                                            if (task4.IsCanceled)
                                            {
                                                panelLoading.SetActive(false);
                                                Debug.LogError("SetRawJsonValueAsync was canceled.");
                                                AggregateException exception = task4.Exception as AggregateException;
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
                                            if (task4.IsFaulted)
                                            {
                                                panelLoading.SetActive(false);
                                                Debug.LogError("SetRawJsonValueAsync was faulted.");
                                                AggregateException exception = task4.Exception as AggregateException;
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
                                            if (task4.IsCompleted)
                                            {
                                                //LoadNextScene();
                                                CreateSpeakerSection();
                                            }
                                        });
                                    }
                                });
                            }
                        });
                    }
                    else
                    {
                        Debug.LogError(String.Format(
                          "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    }
                });
            }
        }
        else
        {
            textLogError.text = "Please Check Your data";

            if (inputFieldPassword.text != inputFieldConfirmPassword.text)
            {
                textLogError.text = "Your Password Does Not Match";
            }
        }
    }

    public void OnPressButtonBack()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelSignIn": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
    }


    public void LoadNextScene()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelIntroduction": gameObject.SetActive(true); break;
                default: gameObject.SetActive(false); break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (Session.auth != null)
            Session.auth.SignOut();
    }


    private void CreateSpeakerSection()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");//Here you should change for you base data link!!!!

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        SpeakersSubcriptions speakersSubcriptions = new SpeakersSubcriptions(false,false,false);
        string json = JsonUtility.ToJson(speakersSubcriptions);
        reference.Child("SpeakersSubcriptions").Child(Session.auth.CurrentUser.UserId).SetRawJsonValueAsync(json).ContinueWith(task => {

            if (task.IsCanceled)
            {
                panelLoading.SetActive(false);
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
                panelLoading.SetActive(false);
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
                CreateWorkShops();
            }
        });
    }

    public void CreateWorkShops()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tomaelcontrol-830dd.firebaseio.com/");//Here you should change for you base data link!!!!

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        WorkShopsSubcriptions workShopsSubcriptions = new WorkShopsSubcriptions(false,false,false);
        string json = JsonUtility.ToJson(workShopsSubcriptions);
        reference.Child("WorkShopsSubcriptions").Child(Session.auth.CurrentUser.UserId).SetRawJsonValueAsync(json).ContinueWith(task => {

            if (task.IsCanceled)
            {
                panelLoading.SetActive(false);
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
                panelLoading.SetActive(false);
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
                //panelLoading.SetActive(false);
                LoadNextScene();
            }
        });
    }
}