using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{
    public Camera _camera;
    public GameObject fakePc;
    private PlayerCharacter pc;

    void Start()
    {
        pc = FindObjectOfType<PlayerCharacter>();
    }

    void Update()
    {
        bool thisIsTheCamera = (PlayerCharacter._alternativeCamera == _camera);

        if (Vector3.Distance(pc.transform.position, transform.position) < 10f && !thisIsTheCamera) // trigger cutscene
            PlayerCharacter.ChangeCamera(_camera);

        thisIsTheCamera = (PlayerCharacter._alternativeCamera == _camera);//in case this just changed
        fakePc.SetActive(Camera.main.depth < 0);
        _camera.depth = thisIsTheCamera ? 1 : -1; //become the chosen camera
    }
}
