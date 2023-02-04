using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //editor 
    public Animator _animator;

    //global vars
    public float _rotationSpeed;
    public float _walkSpeed;
    public float _minRadius;
    public float _maxRadius;

    //private vars
    private Transform _targetTransform;


    void Start()
    {
        _targetTransform = new GameObject().transform;
        _targetTransform.position = transform.position;
        _targetTransform.rotation = transform.rotation;
    }

    void Update()
    {
        InputManager();
        DebugRotate();

        Adapt();
    }

     

    void Adapt()
    {
        transform.position = Vector3.Lerp(transform.position, _targetTransform.position, Time.deltaTime * 7f);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetTransform.rotation, Time.deltaTime * 10f);
    }

    void DebugRotate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            _targetTransform.rotation *= Quaternion.Euler(0, 180, 0);
    }

    void InputManager()
    {
        //rotation
        float t = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            t -= 1;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            t += 1;
        _targetTransform.rotation *= Quaternion.Euler(0, 360 * t * Time.deltaTime * _rotationSpeed, 0);

        //animate head
        _animator.SetInteger("head", (int)t);

        //transposition
        t = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            t += 1;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            t -= 1;
        _targetTransform.position += _targetTransform.forward * t * Time.deltaTime * _walkSpeed;

        //bounds
        if (_targetTransform.position.magnitude < _minRadius)
            _targetTransform.position = _targetTransform.position.normalized* _minRadius;

        if (_targetTransform.position.magnitude > _maxRadius)
            _targetTransform.position = _targetTransform.position.normalized * _maxRadius;

        //animate body
        _animator.SetInteger("channel", (int)(Mathf.Abs(t)));
    }
}
