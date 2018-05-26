using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundToAffect { Master, Sounds, Music };

public class Slider : MonoBehaviour {

    [Header ("Slider Settings")]
    public float m_MinXPos;     //Should be set ahead of time.
    public SoundToAffect m_SoundOption;

    private float m_MaxXPos;
    private Transform m_Transform;
    private bool m_Held;

    void Start() {
        m_Transform = gameObject.GetComponent<Transform>();
        m_MaxXPos = m_Transform.position.x;

    }

    void OnMouseDown()
    {
        m_Held = true;
    }

    void OnMouseUp()
    {
        m_Held = false;
    }

    void Update() {
        if (m_Held)
        {
            HandlePosition();
            HandleSoundLevel();
        }
    }


private void HandlePosition()
    { 
        {
            var mousex = Input.mousePosition.x;

            Quaternion newrot = m_Transform.rotation;
            Vector3 newpos;
            newpos.z = m_Transform.position.z;
            newpos.y = m_Transform.position.y;

            if (mousex < m_MinXPos) newpos.x = m_MinXPos;
            else if (m_MaxXPos < mousex) newpos.x = m_MaxXPos;
            else newpos.x = mousex;

            m_Transform.SetPositionAndRotation(newpos, newrot);
        }
    }

    private void HandleSoundLevel()
    {
        float dist = m_MaxXPos - m_MinXPos;
        float relX = m_MaxXPos - m_Transform.position.x;
        float newvol = relX / dist;

        if (m_SoundOption == SoundToAffect.Master) GameController.GCInst.SetMasterVolume(newvol);
        else if (m_SoundOption == SoundToAffect.Sounds) GameController.GCInst.SetSoundVolume(newvol);
        else GameController.GCInst.SetMusicVolume(newvol);
    }
}