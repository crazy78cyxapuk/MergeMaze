using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemies : MonoBehaviour
{
    private TakeAttack _takeAttack;

    private void Start()
    {
        _takeAttack = transform.parent.gameObject.GetComponent<TakeAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            //_takeAttack.AttackOnEnemy(enemy);
        }
    }
}
