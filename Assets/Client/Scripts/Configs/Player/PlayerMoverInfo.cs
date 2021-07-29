using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoverInfo", menuName = "ScriptableObjects/PlayerMoverInfo", order = 2)]
public class PlayerMoverInfo : ScriptableObject
{
    [Header("Основная скорость игрока")]
    public float speed = 25;

    [Header("Ускорение")]
    public float acceleration = 1;
}
