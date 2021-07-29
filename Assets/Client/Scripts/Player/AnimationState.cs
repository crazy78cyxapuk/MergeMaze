using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState : MonoBehaviour
{
    [SerializeField] private string anim_run;
    [SerializeField] private string anim_hook;
    [SerializeField] private string anim_hit_sword;
    [SerializeField] private string anim_hit_sword_boss;
    [SerializeField] private string anim_end_fight;
    [SerializeField] private string anim_dance;
    [SerializeField] private string anim_dead;

    private Animator _animator;
    private PlayerMover _playerMover;

    [HideInInspector] public bool _isTakedSword = false;

    private bool isLose = false;
    private bool _isBoss;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _animator.SetBool(anim_run, false);
        _animator.SetBool(anim_hook, false);
        _animator.SetBool(anim_hit_sword, false);
        _animator.SetBool(anim_end_fight, false);
    }

    public void OnIdle()
    {
        _animator.SetBool(anim_run, false);
    }

    public void OnRun()
    {
        _animator.SetBool(anim_run, true);
    }

    //public void OnHit()
    //{
    //    //if (_isTakedSword == false)
    //    //{
    //    //    _animator.SetBool(anim_hook, true);
    //    //}
    //    //else
    //    //{
    //    //    _animator.SetBool(anim_hit_sword, true);
    //    //}
    //    //_animator.StopPlayback();
    //    //_animator.SetBool(anim_hit_sword, true);
    //}

    //public void OnEndHit()
    //{
    //    //_animator.SetBool(anim_hit_sword, false);
    //    //_animator.SetBool(anim_end_fight, true);
    //}

    public void TakeArm()
    {
        _isTakedSword = true;
    }

    public void StartFight(bool isBoss)
    {
        if (isBoss)
        {
            _animator.SetBool(anim_hit_sword_boss, true);

            _isBoss = true;
        }
        else
        {
            _animator.SetBool(anim_hit_sword, true);
        }
    }

    public void EndFight()
    {
        if (isLose == false)
        {
            _animator.SetBool(anim_hit_sword, false);
            _animator.SetBool(anim_end_fight, true);

            if (gameObject.TryGetComponent(out PlayerMover playerMover))
            {
                playerMover.SetupRotation();
            }
        }
    }

    IEnumerator TimerEndFight()
    {
        yield return new WaitForSeconds(0.75f);

        _animator.StopPlayback();
        _animator.SetBool(anim_hit_sword, false);
        _animator.SetBool(anim_end_fight, true);

        yield return new WaitForSeconds(1f);

        _animator.SetBool(anim_end_fight, false);
        _animator.SetBool(anim_hook, false);
        _animator.SetBool(anim_hit_sword, false);
    }

    public bool IsAnimationPlaying(string animationName)
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsName(animationName))
            return true;

        return false;
    }

    public void OnDead()
    {
        if (isLose == false)
        {
            StopAllCoroutines();

            isLose = true;
            _animator.SetBool(anim_dead, true);
            _playerMover.Dead();

            GetComponent<AnimationState>().enabled = false;
        }
    }

    public void OnDance()
    {
        //StartCoroutine(TimerForDance());
        //RotatePlayerToCamera();
        GetComponent<CheckInput>().enabled = false;
        _playerMover.enabled = false;

        Camera.main.GetComponent<UIController>().GameWin();

        GetComponent<RotatePlayerToTarget>().enabled = true;
        _animator.SetBool(anim_dance, true);
    }

    private void RotatePlayerToCamera()
    {
        Vector3 target = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = rotation;
    }
}
