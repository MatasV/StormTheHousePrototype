using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyWaveData : ScriptableObject
{
    public List<EnemyData> enemies = new List<EnemyData>();
    public float waveTime;
}
