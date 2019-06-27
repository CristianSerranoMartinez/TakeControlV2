using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RetrieveData : MonoBehaviour
{
    [SerializeField]
    private Text textLogError;

    [SerializeField]
    private string reference;

    // Use this for initialization


    /*private void OnApplicationQuit()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://cats-no-like-banana.firebaseio.com/");//Here you should change for you base data link!!!!

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (player.GetComponent<Player>().donuts > Session.currentUser.donuts)
        {
            Session.currentUser.donuts = (uint)player.GetComponent<Player>().donuts;
        }

        if (Score.GetComponent<Score>().score > Session.currentUser.score)
        {
            Session.currentUser.score = (uint)Score.GetComponent<Score>().score;
        }

        Session.currentUser.generalTimePlayed = Session.currentUser.generalTimePlayed + player.GetComponent<Player>().time;

        string json = JsonUtility.ToJson(Session.currentUser);
        reference.Child("users").Child(Session.auth.CurrentUser.UserId).SetRawJsonValueAsync(json).ContinueWith(task => {

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
    }*/
}
