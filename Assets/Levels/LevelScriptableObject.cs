using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScriptableObject : ScriptableObject
{

    public string instruction;
    public Character solution;
    public List<Character> decoy;

    [System.Serializable]
    public class Character
    {
        public Bench bench;
        public string instruction;
    }

    [System.Serializable]
    public class DialogLine
    {
        public string line;
        public bool left;
    }
}
