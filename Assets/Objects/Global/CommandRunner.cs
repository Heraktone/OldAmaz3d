using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class CommandRunner : MonoBehaviour {
    public delegate void Command(string cmd, string arguments);
    private Dictionary<string, Command> commands;

    public CommandRunner() : base()
    {
        commands = new Dictionary<string, Command>();
    }

    public void AddCommand(string name, Command func)
    {
        commands.Add(name, func);
        Debug.Log("Added command " + name);
    }

    public void UnknownCommand(string cmd, string arguments)
    {
        Debug.LogError("Unknown command : " + cmd);
    }


    public void RunCommand(string cmd)
    {
        Debug.Log("> " + cmd, null);
        string[] parts = cmd.Split(null, 2);
        Command func = this.UnknownCommand;
        try
        {
            func = commands[parts[0]];
        }
        catch(KeyNotFoundException) { }
        if (parts.Length == 2)
        {
            func(cmd, parts[1]);
        }
        else
        {
            func(cmd, "");
        }
    }

    public void RunCommands(string cmds)
    {
        foreach(string line in cmds.Split('\n'))
        {
            RunCommand(line);
        }
    }

    public void LoadResource(string cfgname)
    {
        Debug.Log("Loading built-in config " + cfgname);
        TextAsset config = Resources.Load(cfgname) as TextAsset;
        if (config == null)
        {
            Debug.LogError("Config " + cfgname + " failed to load.");
        }
        else
        {
            RunCommands(config.text);
        }
    }

    public void LoadUserFile(string cfgname, bool silentFail)
    {
        Debug.Log("Loading user config " + cfgname);
        string path = Path.Combine(Application.persistentDataPath, cfgname);
        try {
            using (StreamReader sr = new StreamReader(path))
            {
                while(sr.Peek() >= 0)
                {
                    RunCommand(sr.ReadLine());
                }
            }
        }
        catch(Exception e)
        {
            if(!silentFail)
            {
                Debug.LogError("Error reading " + cfgname + " : " + e.Message);
            }
        }
    }

    public void LoadUserFile(string cfgname)
    {
        LoadUserFile(cfgname, false);
    }
}
