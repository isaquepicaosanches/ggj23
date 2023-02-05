using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScriptableObject : ScriptableObject
{

    public string instruction;
    public List<Character> characters;

    [System.Serializable]
    public class Character
    {
        public bool isSolution;
        public bool missing;
        public List<DialogLine> lines;
    }

    [System.Serializable]
    public class DialogLine
    {
        public string line;
        public bool left;
    }
}
