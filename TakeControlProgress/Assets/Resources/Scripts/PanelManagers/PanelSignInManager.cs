using Firebase;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelSignInManager : MonoBehaviour {

    [SerializeField]
    private InputField inputFieldEmail;

    [SerializeField]
    private InputField inputFieldPassword;

    [SerializeField]
    private Text textLogError;

    [SerializeField]
    private Transform transformCircleLoading;

    [SerializeField]
    private GameObject panelLoading;


    [SerializeField]
    private GameObject[] arrayPanels;


    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void Update()
    {
        transformCircleLoading.transform.Rotate(0, 0, 5);
    }

    private void OnEnable()
    {
        panelLoading.SetActive(false);
        textLogError.text = "";
    }

    public void OnPressButtonSignIn()
    {
        if (TestEmail.IsEmail(inputFieldEmail.text) && inputFieldEmail.text != "" && inputFieldPassword.text != "" && inputFieldEmail.text != " " && inputFieldPassword.text != " ")
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

                        //if (Session.auth.CurrentUser.IsEmailVerified)
                        //{
                            Debug.Log("DefaultInstance");
                            Session.auth.SignInWithEmailAndPasswordAsync(inputFieldEmail.text, inputFieldPassword.text).ContinueWith(task2 =>
                            {
                                if (task2.IsCanceled)
                                {
                                    panelLoading.SetActive(false);
                                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
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
                                            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + fireBaseException.Message);
                                            textLogError.text = fireBaseException.Message;
                                        }
                                    }
                                    return;
                                }
                                if (task2.IsFaulted)
                                {
                                    panelLoading.SetActive(false);
                                    Debug.Log("IsFaulted");
                                    Debug.LogError("SignInWithEmailAndPasswordAsync was faulted.");
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
                                    UnityMainThread.wkr.AddJob(() =>
                                    {
                                        LoadNextScene();
                                    });

                                }
                            });
                       // }
                      //  else
                      //  {
                         //   UnityMainThread.wkr.AddJob(() =>
                       //     {
                             //   panelLoading.SetActive(false);
                        //        textLogError.text = "Please you has to verify your email";
                           // });
                      //  }
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
            panelLoading.SetActive(false);
            textLogError.text = "Please Check Your Email Or Password";
        }
    }

    public void OnPressButtonSignUp()
    {
        foreach (GameObject gameObject in arrayPanels)
        {
            switch (gameObject.name)
            {
                case "PanelSignUp": gameObject.SetActive(true); break;
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
}
