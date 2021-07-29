using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAttack : MonoBehaviour
{
    [SerializeField] private DamageInfo damageInfo;

    private Evolution _evolution;
    private PlayerMover _playerMover;
    private AnimationState _animationState;

    private bool _isFightWin = true;

    private float damage, protection, damageTrap;

    private int lvlEnemy;

    [SerializeField] private GameObject fightFX;
    private FollowTarget _followTarget;

    private void Start()
    {
        _evolution = GetComponent<Evolution>();
        _playerMover = GetComponent<PlayerMover>();
        _animationState = GetComponent<AnimationState>();

        damage = damageInfo.startDamage;
        protection = damageInfo.startProtection;
        damageTrap = damageInfo.damageTrap;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            AttackOnEnemy(enemy);
        }

        if (other.gameObject.TryGetComponent(out Trap trap))
        {
            
            _evolution.TakeHit(damageTrap);
            _playerMover.ContinueRun();
            trap.enabled = true;
        }
    }

    public void AttackOnEnemy(Enemy enemy)
    {
        bool isBoss = enemy.IsBoss();

        _animationState.StartFight(isBoss);

        if (isBoss)
        {
            _playerMover.StopRun();

            GetComponent<CheckInput>().enabled = false;
            _playerMover.enabled = false;

            TurnZoom();

            StartCoroutine(TimerCheckBoss());
        }

        StartCoroutine(TurnFightFX());

        enemy.StartFight(transform);

        lvlEnemy = enemy.GetLevel();

        if (enemy.GetLevel() > _evolution.GetHumanLevel())
        {
            _animationState.OnDead();

            enemy.IsWinFight();
            _isFightWin = false;

            GetComponent<CheckInput>().enabled = false;
            _playerMover.enabled = false;
            Camera.main.GetComponent<UIController>().GameLose();

            CameraZoom cameraZoom = Camera.main.GetComponent<CameraZoom>();
            cameraZoom.enabled = true;
            cameraZoom.SetTarget(transform);

            return;
        }
        else
        {
            _evolution.UpdateLevel(lvlEnemy);
            enemy.IsLoseFight();
            _isFightWin = true;
        }
    }

    public void EndFight()
    {
        _animationState.EndFight();

        if (_isFightWin)
        {
            _playerMover.SetupRotation();
            _playerMover.ContinueRun();
        }

        _isFightWin = true;
    }

    IEnumerator TimerCheckBoss()
    {
        yield return new WaitForSeconds(1f);

        CheckWinFightWithBoss();
    }

    public void CheckWinFightWithBoss()
    {
        if (_isFightWin)
        {
            GetComponent<CheckInput>().enabled = false;
            _playerMover.enabled = false;

            Camera.main.GetComponent<UIController>().GameWin();
        }
    }

    public void CheckEvolution()
    {
        if (_isFightWin)
        {
            _evolution.EvolutionNext(true, lvlEnemy, true);
        }
        else
        {
            _evolution.OnLose();
        }
    }

    public void TurnZoom()
    {
        CameraZoom cameraZoom = Camera.main.GetComponent<CameraZoom>();
        cameraZoom.enabled = true;
        cameraZoom.SetTarget(transform);
    }

    public void FinishFight()
    {
        if (_isFightWin)
        {
            GetComponent<CheckInput>().EndGame();
            Camera.main.GetComponent<UIController>().GameWin();
        }
        else
        {
            _evolution.OnLose();
        }
}

    public void ElrageDamage(float elrageDamage)
    {
        damage += elrageDamage;
    }

    public void ElrageProtection(float elrageProtection)
    {
        protection += elrageProtection;
    }

    IEnumerator TurnFightFX()
    {
        yield return new WaitForSeconds(0.15f);

        if (fightFX.GetComponent<FollowTarget>() != null)
        {
            _followTarget = fightFX.GetComponent<FollowTarget>();
        }

        fightFX.transform.position = transform.position;
        fightFX.SetActive(true);
        _followTarget.SetTarget(transform);
    }
}
