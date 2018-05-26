using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnHover : MonoBehaviour
{

    // Update is called once per frame
    Color m_OriginalColor;
    Color m_NewColor = Color.red;
    SpriteRenderer m_SprR;

    void Start()
    {
        m_SprR = GetComponent<SpriteRenderer>();
        m_OriginalColor = m_SprR.color;
    }

    void OnMouseOver()
    {
        m_SprR.color = m_NewColor;
    }

    void OnMouseExit()
    {
        m_SprR.color = m_OriginalColor;
    }
}
