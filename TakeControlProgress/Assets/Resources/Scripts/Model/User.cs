using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class User  {
    public string username;
    public string email;
    public string password;

    public string uriPhoto;

    public User(string email, string password, string username)
    {
        this.username = username;
        this.email = email;
        this.password = password;

    }
}
