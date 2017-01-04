using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Collections.Specialized;

public class KeyMgr : MonoBehaviour {
    public CommandRunner cmdRunner;
    [Tooltip("Amount of seconds after which the unibutton will trigger walking when it would normally interact.")]
    public float uniWalkTime = .5f;

    public enum Action
    {
        STEP_FORWARD,
        STEP_BACKWARD,
        STEP_LEFT,
        STEP_RIGHT,
        JUMP,
        INTERACT,
        PAUSE,
        UNIBUTTON,
    }

    private Dictionary<Action, List<KeyCode>> keyBindings;

    public KeyMgr()
    {
        UnbindAll();
    }

    public void Awake() {
        cmdRunner.AddCommand("unbindall", (c, a) => UnbindAll());
        cmdRunner.AddCommand("bind", BindKey);
    }

    public void UnbindAll()
    {
        keyBindings = new Dictionary<Action, List<KeyCode>>();
        foreach(Action action in Enum.GetValues(typeof(Action)))
        {
            keyBindings[action] = new List<KeyCode>();
        }
    }

    public void BindKey(string cmd, string arguments)
    {
        string[] args = arguments.Trim().Split();
        if(args.Length != 2)
        {
            Debug.LogError("Usage: bind KEY ACTION");
            return;
        }
        KeyCode kc;
        Action action;
        try
        {
            kc = (KeyCode)Enum.Parse(typeof(KeyCode), args[0]);
            action = (Action)Enum.Parse(typeof(Action), args[1].ToUpper());
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e);
            return;
        }
        keyBindings[action].Add(kc);
    }

    public bool IsKeyPressed(Action action)
    {
        if(action == Action.STEP_FORWARD && IsUniButtonPressedAndNotInteracting())
            return true;
        foreach(KeyCode key in keyBindings[action])
        {
            if(Input.GetKey(key))
            {
                return true;
            }
        }
        return false;
    }

    public bool OnKeyJustPressed(Action action)
    {
        if (action == Action.INTERACT && IsUniButtonPressedForInteracting())
            return true;
        foreach (KeyCode key in keyBindings[action])
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    private float uniPressedSince = 0;
    private bool uniPressed = false;
    private bool interactionRequested = false;

    void Update()
    {
        if (IsKeyPressed(Action.UNIBUTTON))
        {
            if (!uniPressed)
            {
                uniPressedSince = Time.time;
                uniPressed = true;
            }
        }
        else if(uniPressed)
        {
            uniPressed = false;
        }
        else
        {
            uniPressedSince = 0;
            interactionRequested = false;
        }
    }

    private bool IsUniButtonPressedForInteracting()
    {
        interactionRequested = true;
        return uniPressed == false && uniPressedSince != 0 && Time.time - uniPressedSince < uniWalkTime;
    }

    private bool IsUniButtonPressedAndNotInteracting()
    {
        return uniPressed == true && uniPressedSince != Time.time && (!interactionRequested || Time.time - uniPressedSince > uniWalkTime);
    }
}
