using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkShopsQuestions  {
    public int intSpeakerId;
    public string question;
    public string username;

    public WorkShopsQuestions(int intSpeakerId, string question, string username)
    {
        this.intSpeakerId = intSpeakerId;
        this.question = question;
        this.username = username;
    }
}
