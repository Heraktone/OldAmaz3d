using UnityEngine;
using System.Collections;

public class WeighingScale : MonoBehaviour
{
    public Counter counter;
    public Behaviour button;
    public BalanceLogic scale1;
    public Animator anim1;
    public BalanceLogic scale2;
    public Animator anim2;

    public void Compare()
    {
        if (ConsumeRemainingAttempt())
        {
            var cmp = scale1.weightOnScale.CompareTo(scale2.weightOnScale);

            anim1.SetInteger("level", -cmp);
            anim2.SetInteger("level", cmp);
        }
    }

    private bool ConsumeRemainingAttempt()
    {
        if (counter != null)
        {
            if (counter.value < 1)
                return false;
            counter.Decrement();
            if (counter.value == 0)
                button.enabled = false;
        }
        return true;
    }
}
