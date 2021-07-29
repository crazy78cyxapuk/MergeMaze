using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvolutionInfo", menuName = "ScriptableObjects/EvolutionInfo", order = 3)]
public class EvolutionInfo : ScriptableObject
{
    [Header("Эволюция по уровню")]
    public bool isEvolutionLvl = true;

    [Header("1ая эволюция (1 -> 2)")]
    public int firstEvolution;

    [Header("2ая эволюция (2 -> 3)")]
    public int secondEvolution;

    [Header("3ая эволюция (3 -> 4)")]
    public int thirdEvolution;

    //[Header("Последние эволюции по количеству")]
    //public float lastEvolution;

    [Header("Сколько прибавлять за убийство врага")]
    public int addHeal;
}
