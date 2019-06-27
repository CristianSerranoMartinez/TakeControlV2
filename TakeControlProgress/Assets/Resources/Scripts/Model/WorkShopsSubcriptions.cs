using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkShopsSubcriptions {
    public bool one;
    public bool two;

    public WorkShopsSubcriptions() { }

    public WorkShopsSubcriptions(bool one, bool two)
    {
        this.one = one;
        this.two = two;
    }
}
