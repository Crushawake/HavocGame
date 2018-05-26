using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

    void OnMouseDown()
    {
        Camera.main.GetComponent<LimitedCamera>().SetNewTarget(225.0f, 196.5f);
    }
}
