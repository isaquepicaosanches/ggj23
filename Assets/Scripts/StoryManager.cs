using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    //singleton
    public static StoryManager instance;
    //
    public List<LevelScriptableObject> levels;
    public List<Bench> allBenches;
    //editor
    public GameObject blackBars;
    public TextMeshProUGUI clue;
    public TextMeshProUGUI leftDialog;
    public TextMeshProUGUI rightDialog;
    //screens
    public GameObject screen1, screen2, screen3, screen4;

    //flags
    int levelIndex = 0;
    public enum State { Thanks, Credits, Game, Intro, Start};
    public static State state;
    private bool waitForReturn;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public static void BenchTriggered(Bench bench)
    {
        int index = 0;
        if (instance.allBenches.Contains(bench))
            index = instance.allBenches.IndexOf(bench);
        else
            Debug.Log("error");

        instance.BenchTriggered(index);
    }

    public void BenchTriggered(int bench)
    {
        bool isSolution = (levels[levelIndex].characters[bench].isSolution);
        StartCoroutine(AllDialogLines(levels[levelIndex].characters[bench].lines, 
            () => {
                if (isSolution)
                {
                    levelIndex++;
                    if (levelIndex >= levels.Count)
                    {
                        state = State.Intro;
                        return;
                    }
                    else
                        clue.text = levels[levelIndex].instruction;
                }

                PlayerCharacter.ChangeCamera(null);
            }));
    }

    IEnumerator AllDialogLines(List<LevelScriptableObject.DialogLine> lines, System.Action callback)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            DisplayLine(lines[i].left, lines[i].line);
            waitForReturn = true;

            while (waitForReturn)
                yield return new WaitForEndOfFrame();
        }

        callback();
    }

    void DisplayLine(bool left, string dialog)
    {
        leftDialog.text = (left) ? dialog : "";
        rightDialog.text = (!left) ? dialog : "";
    }

    void Update()
    {
        bool cutsceneMode = (Camera.main.depth < 0);
        blackBars.SetActive(cutsceneMode);
        clue.gameObject.SetActive(!cutsceneMode);

        if (waitForReturn && state == State.Game && Input.GetKeyDown(KeyCode.Return))
            waitForReturn = false;

        ManageScreens();
    }

    void ManageScreens()
    {

        if (Input.GetKeyDown(KeyCode.Return) && state != State.Game)
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
