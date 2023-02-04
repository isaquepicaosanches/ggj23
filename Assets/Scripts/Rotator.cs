using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 _rotationDirection;

    void Update()
    {
        transform.localEulerAngles += _rotationDirection * Time.deltaTime;
    }
}
