using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageInfo", menuName = "ScriptableObjects/DamageInfo", order = 1)]
public class DamageInfo : ScriptableObject
{
    [Header("Стартовый дамаг")]
    public float startDamage;

    [Header("Стартовая защита")]
    public float startProtection;

    [Header("Золотой меч добавляет урона")]
    public float addDamageGoldSword;

    [Header("Синий меч добавляет урона")]
    public float addDamageBlueSword;

    [Header("Щит добавляет защиты")]
    public float addProtectionShield;

    [Header("Сколько отнимает ловушка")]
    public float damageTrap;
}
