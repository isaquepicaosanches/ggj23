using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 2f);
        transform.LookAt(target.position);
    }
}
