using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class DoorController : MonoBehaviour
{
    public bool isOpened
    {
        get
        {
            return GetComponent<Animator>().GetBool("open");
        }
        set
        {
            GetComponent<Animator>().SetBool("open", value);
            print("door open " + value);
        }
    }
}
