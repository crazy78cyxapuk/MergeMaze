using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Текущий уровень")]
    [SerializeField] private int currentLevel;

    [Header("Количество ХП")]
    [SerializeField] private float currentHeal;

    [SerializeField] private TextMeshProUGUI textLvl;

    private AnimationState _animationState;

    private bool _isWin = false;

    [SerializeField] private GameObject partBodyFirst, partBodySecond;
    [SerializeField] private GameObject[] allDisableObjects;

    [SerializeField] private bool isBoss = false;

    private void Awake()
    {
        _animationState = GetComponent<AnimationState>();

        textLvl.text = currentLevel.ToString();
    }

    public void StartFight(Transform targetPlayer)
    {
        transform.LookAt(targetPlayer);

        //_animationState.OnHit();
        GetComponent<Animator>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public void IsWinFight()
    {
        _isWin = true;
    }

    public void IsLoseFight()
    {
        //_isWin = false;
        BodySlitting();
    }

    public void EndFight()
    {
        //if(_isWin == false)
        //{
        //    BodySlitting();
        //}
        //else
        //{
        //    Debug.Log("ENEMY FIGHT WIN");
        //}
    }

    public void BodySlitting()
    {
        gameObject.tag = "Untagged";

        partBodyFirst.SetActive(true);
        partBodySecond.SetActive(true);

        for (int i = 0; i < allDisableObjects.Length; i++)
        {
            allDisableObjects[i].SetActive(false);
        }        
    }

    public bool IsBoss()
    {
        return isBoss;
    }
}
