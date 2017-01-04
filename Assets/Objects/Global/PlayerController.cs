using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : RigidbodyFirstPersonController {
    public KeyMgr keyManager;

    override protected Vector2 GetInput()
    {
        Vector2 ret = new Vector2
        {
            x = GetXDisplacement(),
            y = GetYDisplacement()
        };
        return ret;
    }

    private float GetXDisplacement()
    {
        return GetDisplacement(KeyMgr.Action.STEP_RIGHT, KeyMgr.Action.STEP_LEFT);
    }

    private float GetYDisplacement()
    {
        return GetDisplacement(KeyMgr.Action.STEP_FORWARD, KeyMgr.Action.STEP_BACKWARD);
    }

    private float GetDisplacement(KeyMgr.Action pos, KeyMgr.Action neg)
    {
        float pos_mov = keyManager.IsKeyPressed(pos) ? 1.0f : 0;
        float neg_mov = keyManager.IsKeyPressed(neg) ? 1.0f : 0;
        return pos_mov - neg_mov;
    }

    new protected void Update()
    {
        RotateView();

        if (keyManager.IsKeyPressed(KeyMgr.Action.JUMP) && !m_Jump)
        {
            m_Jump = true;
        }
    }
}
