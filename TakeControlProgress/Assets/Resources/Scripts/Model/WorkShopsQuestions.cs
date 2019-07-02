using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkShopsQuestions  {
    public int intWorkShopId;
    public string question;
    public string username;

    public WorkShopsQuestions(int intWorkShopId, string question, string username)
    {
        this.intWorkShopId = intWorkShopId;
        this.question = question;
        this.username = username;
    }

    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["intWorkShopId"] = intWorkShopId;
        result["question"] = question;
        result["username"] = username;

        return result;
    }
}
