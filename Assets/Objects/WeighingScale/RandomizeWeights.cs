using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class RandomizeWeights : BaseTrigger
{
    public int normalWeight = 10;
    public int specialWeight = 20;
    public bool printInConsole = false;

    new void Start()
    {
        base.Start();
        Randomize();
    }

    public void Randomize()
    {
        var objs = GetComponentsInChildren<HasWeight>();

        var random = Random.Range(0, objs.Length);

        Debug.Log(objs.Select((obj, i) =>
        {
            if (i == random)
            {
                obj.weight = specialWeight;
                if (printInConsole) { Debug.Log(obj.gameObject); }
                Trigger(obj.gameObject);
            }
            else obj.weight = normalWeight;
            return 0;
        }).ToList());
    }
}
