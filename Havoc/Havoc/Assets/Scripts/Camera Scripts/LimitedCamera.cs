using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LimitedCamera : MonoBehaviour
{
    [Header("Camera limits")]
    public Vector2 m_TopLeftLimit;       //Top Left Limit
    public Vector2 m_BottomRightLimit;   //Bottom Right Limit
    public bool m_HorizontallyLocked;   //Horizontal movement lock
    public bool m_VerticallyLocked;     //Vertical movement lock
    [Space(10)]

    [Header("Targetting settings")]
    public bool m_AutoTargetPlayer; //Automatic player targetting built in

    [Range(0.80f, 1.20f)]
    public float m_AccelRate;       //Acceleration modifier
    [Range(0.80f, 1.20f)]
    public float m_DeccelRate;      //Decceleration modifier

    [Range(0.00f, 0.20f)]
    public float m_DistanceLeeway;  //Leeway % from the centre of the screen

    private Transform m_Transform;
    private Vector2 m_Target;
    private Vector2 m_Speed;

    public void SetNewTarget(float x, float y)
    {
        if (x != 0.0f) m_Target.x = x;
        if (y != 0.0f) m_Target.y = y;
    }

    public void SetNewTLLimit(float x, float y)
    {
        if (x != 0.0f) m_TopLeftLimit.x = x;
        if (y != 0.0f) m_TopLeftLimit.y = y;
    }

    public void SetNewBRLimit(float x, float y)
    {
        if (x != 0.0f) m_BottomRightLimit.x = x;
        if (y != 0.0f) m_BottomRightLimit.y = y;
    }

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
    }

    void LateUpdate()
    {

        if (m_Target.x != 0.0f || m_Target.y != 0.0f)                  //If the target isn't 0,0 we've got to assess how we're doing this
        {
            Vector2 dist;
            if (!m_HorizontallyLocked) dist.x = m_Target.x - m_Transform.position.x;      //Calculate the Hdistance to the target position if we're not horizontally locked
            else dist.x = 0;
            if (!m_VerticallyLocked) dist.y = m_Target.y - m_Transform.position.y;      //Same for Vdistance
            else dist.y = 0;

            dist.x = Mathf.Abs(dist.x);
            dist.y = Mathf.Abs(dist.y);

            Vector2 abspd;
            abspd.x = Mathf.Abs(m_Speed.x);
            abspd.y = Mathf.Abs(m_Speed.y);

            if (abspd.x < dist.x / (2.0f * m_AccelRate))
            {
                if (m_Transform.position.x < m_Target.x) m_Speed.x += dist.x / 100.0f;
                else m_Speed.x -= dist.x / 100.0f;
            }
            else if (abspd.x > dist.x / (2.0f * m_AccelRate)) m_Speed.x /= (2.0f * m_DeccelRate);

            if (abspd.y < dist.y / (2.0f * m_AccelRate))
            {
                if (m_Transform.position.y < m_Target.y) m_Speed.y += dist.y / 100.0f;
                else m_Speed.y -= dist.y / 100.0f;
            }

            else if (abspd.y > dist.y / (2.0f * m_AccelRate)) m_Speed.y /= (2.0f * m_DeccelRate);

            Debug.Log("Current X: " + transform.position.x + "\n");
            Debug.Log("Current Y: " + transform.position.y + "\n");

            if (Mathf.Abs(m_Speed.x) < 0.1f) m_Speed.x = 0.0f;
            if (Mathf.Abs(m_Speed.y) < 0.1f) m_Speed.y = 0.0f;

            m_Transform.Translate(m_Speed);                        //Move to the target location

        }

        VerifyInBounds();                                   //Make sure the camera hasn't moved out of bounds.
    }

    void VerifyInBounds()
    {
        Camera cam = Camera.main;                                 //Get access to the current camera, and figure out the width and height to a border
        float halfheight = cam.orthographicSize;
        float halfwidth = halfheight * cam.aspect;

        if (m_Transform.position.x - halfwidth < m_TopLeftLimit.x)
        {
            Vector3 trans;
            trans.x = m_TopLeftLimit.x - (m_Transform.position.x - halfwidth);
            trans.y = 0.0f;
            trans.z = 0.0f;
            m_Transform.Translate(trans);
        }

        if (m_Transform.position.x + halfwidth > m_BottomRightLimit.x)
        {
            Vector3 trans;
            trans.x = m_BottomRightLimit.x - (m_Transform.position.x + halfwidth);
            trans.y = 0.0f;
            trans.z = 0.0f;
            m_Transform.Translate(trans);
        }

        if (m_Transform.position.y - halfheight < m_TopLeftLimit.y)
        {
            Vector3 trans;
            trans.x = 0.0f;
            trans.y = m_TopLeftLimit.y - (m_Transform.position.y - halfheight);
            trans.z = 0.0f;
            m_Transform.Translate(trans);
        }

        if (m_Transform.position.y + halfheight > m_BottomRightLimit.y)
        {
            Vector3 trans;
            trans.x = 0.0f;
            trans.y = m_BottomRightLimit.y - (m_Transform.position.y + halfheight);
            trans.z = 0.0f;
            m_Transform.Translate(trans);
        }
    }
}

