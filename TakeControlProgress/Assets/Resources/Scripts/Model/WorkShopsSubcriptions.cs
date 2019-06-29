using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkShopsSubcriptions {
    public bool one;
    public bool two;
    public bool three;

    public WorkShopsSubcriptions() { }

    public WorkShopsSubcriptions(bool one, bool two, bool three)
    {
        this.one = one;
        this.two = two;
        this.three = three;
    }
}
