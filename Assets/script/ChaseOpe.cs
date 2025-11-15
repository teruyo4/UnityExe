using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseOpe", menuName = "ScriptableObjects/ChaseOperation")]

public class ChaseOpe : ScriptableObject {
    public List<ChaseOpeElem> Opes = new List<ChaseOpeElem>();
}

[System.Serializable]
public class ChaseOpeElem {
    [SerializeField]
    public float sceneScale;
    public float posY;
    public float minDist;
    public float maxDist;
    public float bpm;
}
