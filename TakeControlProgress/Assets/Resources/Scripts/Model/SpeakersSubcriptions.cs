using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeakersSubcriptions {
    public bool one;
    public bool two;
    public bool three;

    public SpeakersSubcriptions() { }

    public SpeakersSubcriptions(bool one, bool two, bool three)
    {
        this.one = one;
        this.two = two;
        this.three = three;
    }
}
