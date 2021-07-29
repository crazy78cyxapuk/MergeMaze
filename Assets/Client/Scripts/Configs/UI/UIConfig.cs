using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIConfig", menuName = "ScriptableObjects/UIConfig", order = 1)]
public class UIConfig : ScriptableObject
{
    [Header("Через сколько секунд должен появляться экран вин/луз ?")]
    public float timer;
}
