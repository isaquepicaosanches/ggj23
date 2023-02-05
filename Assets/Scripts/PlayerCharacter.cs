using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //editor 
    public Animator _animator;
    public SkinnedMeshRenderer _mesh;
    public static SkinnedMeshRenderer _staticMesh;
    public static Camera _alternativeCamera;

    //global vars
    public float _rotationSpeed;
    public float _walkSpeed;

    //constants
    private float _minRadius = 28;
    private float _maxRadius = 123;

    //private vars
    private static Transform _targetTransform;
    bool isMoving;

    void Start()
    {
        _staticMesh = _mesh;
        _targetTransform = new GameObject().transform;
        _targetTransform.position = transform.position;
        _targetTransform.rotation = transform.rotation;
        StartCoroutine(PlayFootsteps());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ChangeCamera(null);
        }

        InputManager();

        Adapt();
    }

    void Adapt()
    {
        transform.position = Vector3.Lerp(transform.position, _targetTransform.position, Time.deltaTime * 7f);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetTransform.rotation, Time.deltaTime * 10f);
    }

    static void Flip()
    {
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
        if (Camera.main.depth < 0) //if cutscene, stop
            t = 0;

        _targetTransform.rotation *= Quaternion.Euler(0, 360 * t * Time.deltaTime * _rotationSpeed, 0);

        //animate head
        _animator.SetInteger("head", (int)t);

        //transposition
        t = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            t += 1;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            t -= 1;
        if (Camera.main.depth < 0) //if cutscene, stop
            t = 0;

        _targetTransform.position += _targetTransform.forward * t * Time.deltaTime * _walkSpeed;
        isMoving = (t != 0);

        //bounds
        if (_targetTransform.position.magnitude < _minRadius)
            _targetTransform.position = _targetTransform.position.normalized* _minRadius;

        if (_targetTransform.position.magnitude > _maxRadius)
            _targetTransform.position = _targetTransform.position.normalized * _maxRadius;

        //animate body
        _animator.SetInteger("channel", (int)(Mathf.Abs(t)));
    }

    IEnumerator PlayFootsteps()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (isMoving)
                AudioManager.ShootStep();
        }
    }

    public static void ChangeCamera(Camera target)
    {
        if (target != null)
        {
            _alternativeCamera = target;
            _staticMesh.enabled = false;
            Camera.main.depth = -5;
        }
        else
        {
            _staticMesh.enabled = true;
            Camera.main.depth = 5;
            Flip();
        }
    }
}
