using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billy : MonoBehaviour {

    [Header("Initial settings")]
    public Direction m_Dir;

    private enum BillyState { Idle, Walking, BasicAttacking, Attack1, Attack2, Attack3, Dying };
    private BillyState m_State;
    private ControlScheme m_KbdControls;
    private ControlScheme m_ContControls;
    private Animator m_Anim;
    private Vector2 m_Speed;

    void Start() {
        m_KbdControls = GameController.GCInst.GetKbdControlScheme();        //Get the controls we're going to use
        m_ContControls = GameController.GCInst.GetContControlScheme();
        m_Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(m_KbdControls.LeftKeyInput) || Input.GetKeyDown(m_ContControls.LeftKeyInput))
        {

        }
        if (Input.GetKeyDown(m_KbdControls.RightKeyInput) || Input.GetKeyDown(m_ContControls.RightKeyInput))
        {

        }
        if (Input.GetKeyDown(m_KbdControls.UpKeyInput) || Input.GetKeyDown(m_ContControls.UpKeyInput))
        {

        }
        if (Input.GetKeyDown(m_KbdControls.DownKeyInput) || Input.GetKeyDown(m_ContControls.DownKeyInput))
        {

        }


    }
}