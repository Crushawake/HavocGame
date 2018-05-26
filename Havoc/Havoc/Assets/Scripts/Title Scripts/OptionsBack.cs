using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsBack : MonoBehaviour {


    void OnMouseDown()
    {
        Camera.main.GetComponent<LimitedCamera>().SetNewTarget(200.0f, 196.5f);
    }
}
