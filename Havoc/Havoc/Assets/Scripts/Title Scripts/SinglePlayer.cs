using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{
    void OnMouseDown()
    {
        Camera.main.GetComponent<LimitedCamera>().SetNewTarget(200.0f, 181.5f);
    }
}
