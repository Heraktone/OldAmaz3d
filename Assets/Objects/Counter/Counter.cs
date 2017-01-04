using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {
    public int value;
    public Animator animator;

    public void SetValue(int val)
    {
        value = val;
        animator.SetInteger("count", value);
    }

    public void Increment()
    {
        SetValue(value + 1);
    }

    public void Decrement()
    {
        SetValue(value - 1);
    }
}
