using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkShopsSubcriptions {
    public bool one;
    public bool two;
    public bool three;
    public bool four;

    public WorkShopsSubcriptions() { }

    public WorkShopsSubcriptions(bool one, bool two, bool three, bool four)
    {
        this.one = one;
        this.two = two;
        this.three = three;
        this.four = four;
    }
}
