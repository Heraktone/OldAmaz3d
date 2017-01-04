using UnityEngine;
using System.Collections;
using System;

public class Game : MonoBehaviour {
    public static Game g {
        get
        {
            return GameObject.FindObjectOfType<Game>();
        }
    }

    public Behaviour menuCanvas;
    public Behaviour pointerCanvas;

    public KeyMgr keys;
    public CommandRunner cmdRunner;

    private bool _isPaused = false;

    private bool isGamePaused {
        get
        {
            return _isPaused;
        }
        set
        {
            if(_isPaused != value)
            {
                if (_isPaused) EndPause();
                else StartPause();
            }
        }
    }
    
	void Awake () {
        cmdRunner.LoadResource("Config/defaults");
        cmdRunner.LoadUserFile("Config/autoexec.txt", true);

        EndPause();
	}

    
    public void StartPause()
    {
        SetPause(true);
    }

    public void EndPause()
    {
        SetPause(false);
    }

    private void SetPause(bool paused)
    {
        Debug.Log("Paused");
        Time.timeScale = paused? 0 : 1;
        Cursor.lockState = paused? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;
        _isPaused = paused;
        menuCanvas.enabled = paused;
        pointerCanvas.enabled = !paused;
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (keys.OnKeyJustPressed(KeyMgr.Action.PAUSE))
        {
            isGamePaused = !isGamePaused;
        }
    }
}
