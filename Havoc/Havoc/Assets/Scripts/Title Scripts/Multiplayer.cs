using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplayer : MonoBehaviour {

    void OnMouseDown()
    {
        Camera.main.GetComponent<LimitedCamera>().SetNewTarget(200.0f, 215.0f);
    }
}
