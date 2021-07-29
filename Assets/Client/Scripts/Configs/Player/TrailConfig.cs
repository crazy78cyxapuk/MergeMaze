using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrailConfig", menuName = "ScriptableObjects/TrailConfig", order = 1)]
public class TrailConfig : ScriptableObject
{
    [Header("Таймер исчезновения трэйла")]
    public float timer;
}
