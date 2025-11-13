using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data2", menuName = "ScriptableObjects/PlayerStatus")]

public class PlayerStatuses : ScriptableObject {
    public List<PlayerStatus> playerStatuses = new List<PlayerStatus>();
}

[System.Serializable]
public class PlayerStatus {
    public float HP;
    public float test;
}

