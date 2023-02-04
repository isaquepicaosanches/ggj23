using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //global vars
    public float _rotationSpeed;
    public float _walkSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        InputManager();
    }

    void InputManager()
    {
        float t = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
            t += 1;
        if (Input.GetKey(KeyCode.RightArrow))
            t -= 1;

        transform.rotation *= Quaternion.Euler(0, 360 * t * Time.deltaTime * _rotationSpeed, 0);

        t = 0;
        if (Input.GetKey(KeyCode.UpArrow))
            t += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            t -= 1;

        transform.position += transform.forward * t * Time.deltaTime * _walkSpeed;
    }
}
