using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BehaviorOfChaseScene")]

public class BehaviorOfChaseSceneList : ScriptableObject {
    public List<BehaviorOfChaseScene> behaviorOfChaseScenes = new List<BehaviorOfChaseScene>();
}

[System.Serializable]
public class BehaviorOfChaseScene {
    [SerializeField]
    public float sceneScale;
    public float posY;
    public float minDist;
    public float maxDist;
    public float bpm;
}

