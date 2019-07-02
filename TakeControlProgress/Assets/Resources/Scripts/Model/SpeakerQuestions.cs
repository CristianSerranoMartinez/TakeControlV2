using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeakerQuestions{
    public int intSpeakerId;
    public string question;
    public string username;

    public SpeakerQuestions(int intSpeakerId, string question, string username)
    {
        this.intSpeakerId = intSpeakerId;
        this.question = question;
        this.username = username;
    }

    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["intSpeakerId"] = intSpeakerId;
        result["question"] = question;
        result["username"] = username;

        return result;
    }
}
