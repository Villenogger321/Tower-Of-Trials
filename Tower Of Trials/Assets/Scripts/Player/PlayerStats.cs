using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float[] baseStats = new float[5];
    public float[] statModifier = new float[5];
    public static Transform Player;

    void Awake()
    {
        Player = transform;
    }
    public void ApplyStats(List<TrinketStat> _newStats)
    {
        for (int i = 0; i < _newStats.Count; i++)
        {
            statModifier[(int)_newStats[i].type] += _newStats[i].statModifier;
        }
    }
    public void RemoveStats(List<TrinketStat> _newStats)
    {
        for (int i = 0; i < _newStats.Count; i++)
        {
            statModifier[(int)_newStats[i].type] -= _newStats[i].statModifier;
        }
    }
}
