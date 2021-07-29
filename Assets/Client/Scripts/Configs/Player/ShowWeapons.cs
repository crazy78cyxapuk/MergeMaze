using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShowWeapons", menuName = "ScriptableObjects/ShowWeaponsConfig", order = 0)]
public class ShowWeapons : ScriptableObject
{
    [Header("Показывать FX?")]
    public bool isShowFX;
}
