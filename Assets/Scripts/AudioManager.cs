using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    int currentIndex = 0;

    //editor
    public List<AudioSource> footstepsSFX;
    public List<AudioSource> barkSFX;
    public AudioSource music1, music2;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        music1.mute = StoryManager.state != StoryManager.State.Game;
        music2.mute = !music1.mute;
    }

    public static void ShootStep()
    {
        instance.NewStep();
    }

    public static void ShootBark()
    {
        instance.NewBark();
    }

    void NewStep()
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < footstepsSFX.Count; i++)
        {
            indexes.Add(i);
        }
        indexes.Remove(currentIndex);
        currentIndex = indexes[Random.Range(0, indexes.Count)];
        footstepsSFX[currentIndex].Play();
    }

    void NewBark()
    {
        barkSFX[Random.Range(0, barkSFX.Count)].Play();
        int rand = Random.Range(0, 3);
        if (rand == 0)
        StartCoroutine(Wait(Random.Range(0.33f, 0.66f), () =>{
            barkSFX[Random.Range(0, barkSFX.Count)].Play();
        }));

    }
    IEnumerator Wait(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }


}
