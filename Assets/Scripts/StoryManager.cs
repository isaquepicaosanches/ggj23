using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public List<LevelScriptableObject> levels;
    //editor
    public GameObject blackBars;
    public TextMeshProUGUI clue;
    public TextMeshProUGUI leftDialog;
    public TextMeshProUGUI rightDialog;
    //screens
    public GameObject screen1, screen2, screen3, screen4;

    public enum State { Thanks, Credits, Game, Intro, Start};
    public static State state;

    void Start()
    {
        
    }

    void Update()
    {
        bool cutsceneMode = (Camera.main.depth < 0);
        blackBars.SetActive(cutsceneMode);
        clue.gameObject.SetActive(!cutsceneMode);

        ManageScreens();
    }

    void ManageScreens()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (state == State.Start)
                Application.Quit();
            else
                state++;
        }

        screen1.SetActive(state == State.Thanks);
        screen2.SetActive(state == State.Credits);
        screen3.SetActive(state == State.Intro);
        screen4.SetActive(state == State.Start);
    }
}
