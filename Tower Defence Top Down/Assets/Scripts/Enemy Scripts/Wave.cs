using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int waveNumber;
    public GameObject [] enemies;
    public int amountSpawnable;
    public int totalWaveSpawnCount;
    public int pointForWaveCompletion;
}
