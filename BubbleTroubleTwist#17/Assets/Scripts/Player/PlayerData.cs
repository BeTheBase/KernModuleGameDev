using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : StatsBase<PlayerData>
{
    [SerializeField] private string id;
    public string Id { get => id; set => id = value; }

    [SerializeField] private string horizontalAxis;
    public string HorizontalAxis { get => horizontalAxis; set => horizontalAxis = value; }

    [SerializeField] private string verticalAxis;
    public string VerticalAxis { get => verticalAxis; set => verticalAxis = value; }

    [SerializeField] private KeyCode fireKey;
    public KeyCode FireKey { get => fireKey; set => fireKey = value; }

    [SerializeField] private int level;
    public int Level { get => level; set => level = value; }

    [SerializeField] private Vector3 spawnVector;
    public Vector3 SpawnVector { get => spawnVector; set => spawnVector = value; }
}
