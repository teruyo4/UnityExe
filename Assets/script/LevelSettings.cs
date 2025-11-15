using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetting", menuName = "ScriptableObjects/LevelSetting")]

public class LevelSetting : ScriptableObject {
    static public int level;
    public List<LevelElement> settings = new List<LevelElement>();
}

[System.Serializable]
public class LevelElement {
    public string levelName;
    // 以下はしきい値となる秒数
    public float super;
    public float good;
    public float normal;
    public float bad;
}
