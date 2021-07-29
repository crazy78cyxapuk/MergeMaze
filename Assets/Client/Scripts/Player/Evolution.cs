using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Evolution : MonoBehaviour
{
    [SerializeField] private float currentLevel;
    [SerializeField] private float currentHeal;
    private int _maxLevel = 4;
    private float _startLevel;

    [SerializeField] private GameObject nextHuman;
    [SerializeField] private GameObject previousHuman;

    private Evolution _evolutionNextHuman, _evolutionPreviousHuman;
    private TakeArms _takeArmsNextHuman;

    private float _percentEnlarge;
    private float _maxPercentEnlarge;
    private float _firstScale;

    private TakeArms _takeArms;

    [SerializeField] private GameObject levelUpFX;

    private PlayerMover _playerMoverNextHuman, _playerMoverPreviousHuman;
    private PlayerMover _playerMover;

    [SerializeField] private EvolutionInfo evolutionInfo;
    private bool isEvolutionLvl;

    [SerializeField] private int stage;
    private int firstEvolution, secondEvolution, thirdEvolution, lastEvolution, addHeal;
    private float startHeal;

    private bool _isEnlarging = true;
    private bool _isNext = true;

    [SerializeField] private TextMeshProUGUI textLvl;

    //[SerializeField] private Trail trail;
    //[SerializeField] private LabelPlayerLevel labelPlayer;

    [SerializeField] private RotateCanvas rotateCanvas;

    [SerializeField] private Transform head;

    private AnimationState _animationStateNextHuman;

    [SerializeField] private CheckForward _checkForward;

    private void Awake()
    {
        _takeArms = GetComponent<TakeArms>();
        _playerMover = GetComponent<PlayerMover>();

        isEvolutionLvl = evolutionInfo.isEvolutionLvl;

        firstEvolution = evolutionInfo.firstEvolution;
        secondEvolution = evolutionInfo.secondEvolution;
        thirdEvolution = evolutionInfo.thirdEvolution;

        switch (stage)
        {
            case (1):
                currentLevel = 1;
                break;

            case (2):
                currentLevel = firstEvolution;
                break;

            case (3):
                currentLevel = secondEvolution;
                break;

            case (4):
                currentLevel = thirdEvolution;
                break;
        }

        _startLevel = currentLevel;

        addHeal = evolutionInfo.addHeal;

        startHeal = currentHeal;
    }

    private void OnDisable()
    {
        currentHeal = startHeal;
    }

    private void OnEnable()
    {
        rotateCanvas.SetTarget(head);

        UpdateText();
        _isNext = true;

        StartCoroutine(CheckEnemies());
    }

    private void Start()
    {
        if (currentLevel == _maxLevel)
        {
            _firstScale = transform.localScale.x;
            _maxPercentEnlarge = transform.localScale.x / 5f;
            _percentEnlarge = transform.localScale.x / 20f;

            _playerMover = GetComponent<PlayerMover>();
        }

        UpdateText();
    }

    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject obj = GameObject.FindGameObjectWithTag("Enemy");

        if(obj == null)
        {
            GetComponent<AnimationState>().OnDance();

            CameraZoom cameraZoom = Camera.main.GetComponent<CameraZoom>();
            cameraZoom.enabled = true;
            cameraZoom.SetTarget(transform);

            GetComponent<CheckInput>().enabled = false;
            _playerMover.enabled = false;

            Camera.main.GetComponent<UIController>().GameWin();
        }
    }

    public void TakeHit(float damage)
    {
        if (isEvolutionLvl)
        {
            EvolutionBack(damage);
        }
        else
        {
            currentHeal -= damage;

            switch (currentLevel)
            {
                case (1):
                    if (currentHeal < startHeal)
                    {
                        Debug.LogError("GAME OVER!!!");
                    }
                    break;

                default:
                    if (currentHeal <= startHeal)
                    {
                        EvolutionBack(damage);
                    }
                    break;
            }
        }
    }

    public void EvolutionNext(bool isRun, int addLvl, bool isEnemy = (default))
    {
        if (isEvolutionLvl)
        {
            Next(isRun, addLvl, isEnemy);
        }
        else
        {
            currentHeal += addHeal;

            switch (currentLevel)
            {
                case (1):
                    if (currentHeal > firstEvolution)
                    {
                        Next(isRun, addLvl);
                    }
                    break;

                case (2):
                    if (currentHeal > secondEvolution)
                    {
                        Next(isRun, addLvl);
                    }
                    break;

                case (3):
                    if (currentHeal > lastEvolution)
                    {
                        Next(isRun, addLvl);
                    }
                    break;
            }
        }
    }

    public void UpdateLevel(int addLvl)
    {
        currentLevel += addLvl;
        UpdateText();
    }

    private void Next(bool isRun, int addLvl, bool isEnemy = (default))
    {
        if (_isNext)
        {
            _isNext = false;
            StartCoroutine(TimerNext());

            //currentLevel += addLvl;
            //UpdateText();

            bool isNextEvolution = false;

            switch (stage)
            {
                case (1):
                    if (currentLevel < firstEvolution)
                    {
                        ShowLevelUpFX();
                    }
                    else
                    {
                        isNextEvolution = true;
                    }
                    break;

                case (2):
                    if (currentLevel < secondEvolution)
                    {
                        ShowLevelUpFX();
                    }
                    else
                    {
                        isNextEvolution = true;
                    }
                    break;

                case (3):
                    if (currentLevel < thirdEvolution)
                    {
                        ShowLevelUpFX();
                    }
                    else
                    {
                        isNextEvolution = true;
                    }
                    break;

                default:
                    isNextEvolution = true;
                    break;
            }


            if (isNextEvolution)
            {
                if (nextHuman != null)
                {
                    nextHuman.transform.localPosition = gameObject.transform.localPosition;
                    nextHuman.transform.rotation = gameObject.transform.rotation;

                    nextHuman.SetActive(true);

                    if (_takeArmsNextHuman == null)
                    {
                        _takeArmsNextHuman = nextHuman.GetComponent<TakeArms>();
                    }

                    _takeArmsNextHuman.Initialization(_takeArms.GetAllWeapons());

                    if (_evolutionNextHuman == null)
                    {
                        _evolutionNextHuman = nextHuman.GetComponent<Evolution>();
                    }

                    _evolutionNextHuman.ShowLevelUpFX();

                    if (_playerMoverNextHuman == null)
                    {
                        _playerMoverNextHuman = nextHuman.GetComponent<PlayerMover>();
                    }

                    if (isRun && _playerMoverNextHuman != null && _checkForward.IsForwardClearing())
                    {
                        _playerMoverNextHuman.ContinueRun();
                    }

                    _evolutionNextHuman.SetCurrentLvl(currentLevel, false);

                    //if (isEnemy)
                    //{
                    //    if (_animationStateNextHuman == null)
                    //    {
                    //        _animationStateNextHuman = nextHuman.GetComponent<AnimationState>();
                    //    }

                    //    _animationStateNextHuman.StartFight();
                    //}

                    switch (stage)
                    {
                        case (1):
                            currentLevel = firstEvolution - 1;
                            break;

                        case (2):
                            currentLevel = secondEvolution - 1;
                            break;

                        case (3):
                            currentLevel = thirdEvolution - 1;
                            break;
                    }

                    //trail.parent = nextHuman.transform;
                    //_evolutionNextHuman.SetTrail(trail);
                    //trail.SetTarget(nextHuman.transform);
                    //labelPlayer.SetTarget(nextHuman.transform, GetComponent<Evolution>());
                    /////////////////////rotateCanvas.SetTarget(nextHuman.transform);

                    gameObject.SetActive(false);
                }
                else
                {
                    EnlargeScale(isRun);
                }
            }
        }
    }

    public void EvolutionBack(float _damage)
    {
        if (_isNext)
        {
            currentLevel -= _damage;
            UpdateText();

            if (currentLevel < 1)
            {
                OnLose();
                return;
            }

            StartCoroutine(TimerNext());

            bool isBackEvolution = false;

            switch (stage)
            {
                case (2):
                    if (currentLevel < firstEvolution)
                    {
                        isBackEvolution = true;
                    }
                    break;

                case (3):
                    if (currentLevel < secondEvolution)
                    {
                        isBackEvolution = true;
                    }
                    break;

                case (4):
                    if(currentLevel < thirdEvolution)
                    {
                        isBackEvolution = true;
                    }
                    else
                    {
                        if (stage == 4 && transform.localScale.x > _firstScale)
                        {
                            float newScale = transform.localScale.x - _percentEnlarge;
                            transform.localScale = new Vector3(newScale, newScale, newScale);
                            return;
                        }
                    }
                    break;
            }





            if (isBackEvolution)
            {
                if (previousHuman != null)
                {
                    previousHuman.transform.localPosition = gameObject.transform.localPosition;
                    previousHuman.transform.rotation = gameObject.transform.rotation;

                    previousHuman.SetActive(true);

                    if(_playerMoverPreviousHuman == null)
                    {
                        _playerMoverPreviousHuman = previousHuman.GetComponent<PlayerMover>();
                    }

                    _playerMoverPreviousHuman.ContinueRun();

                    if(_evolutionPreviousHuman == null)
                    {
                        _evolutionPreviousHuman = previousHuman.GetComponent<Evolution>();
                    }

                    _evolutionPreviousHuman.SetCurrentLvl(currentLevel, true);

                    //trail.SetTarget(previousHuman.transform);
                    //labelPlayer.SetTarget(previousHuman.transform, GetComponent<Evolution>());
                    /////////////////////rotateCanvas.SetTarget(previousHuman.transform);

                    gameObject.SetActive(false);
                }
            }
        }
    }

    public float GetHumanLevel()
    {
        return currentLevel;
    }

    private void EnlargeScale(bool isRun)
    {
        if (_isEnlarging)
        {
            if (_maxPercentEnlarge < transform.localScale.x)
            {
                _isEnlarging = false;

                ShowLevelUpFX();

                float newScale = transform.localScale.x + _percentEnlarge;
                transform.localScale = new Vector3(newScale, newScale, newScale);

                StartCoroutine(TimerEnlarged());
            }

            _playerMover.ContinueRun();
        }
    }

    IEnumerator TimerEnlarged()
    {
        yield return new WaitForSeconds(0.75f);

        _isEnlarging = true;
    }

    IEnumerator TimerNext()
    {
        yield return new WaitForSeconds(0.25f);
        _isNext = true;
    }

    public void ShowLevelUpFX()
    {
        levelUpFX.SetActive(true);
    }

    private void UpdateText()
    {
        if (isEvolutionLvl)
        {
            textLvl.text = currentLevel.ToString();
        }
        else
        {
            textLvl.text = currentHeal.ToString();
        }
    }

    public void AddLevel(float addLvl)
    {
        currentLevel += addLvl;
        UpdateText();

        bool isNextEvolution = false;

        switch (stage)
        {
            case (1):
                if (currentLevel < firstEvolution)
                {
                    ShowLevelUpFX();
                }
                else
                {
                    isNextEvolution = true;
                }
                break;

            case (2):
                if (currentLevel < secondEvolution)
                {
                    ShowLevelUpFX();
                }
                else
                {
                    isNextEvolution = true;
                }
                break;

            case (3):
                if (currentLevel < thirdEvolution)
                {
                    ShowLevelUpFX();
                }
                else
                {
                    isNextEvolution = true;
                }
                break;

            default:
                isNextEvolution = true;
                break;
        }


        if (isNextEvolution)
        {
            if (nextHuman != null)
            {
                nextHuman.transform.localPosition = gameObject.transform.localPosition;
                nextHuman.transform.rotation = gameObject.transform.rotation;

                nextHuman.SetActive(true);

                if (_takeArmsNextHuman == null)
                {
                    _takeArmsNextHuman = nextHuman.GetComponent<TakeArms>();
                    _takeArmsNextHuman.Initialization(_takeArms.GetAllWeapons());
                }
                else
                {
                    _takeArmsNextHuman.Initialization(_takeArms.GetAllWeapons());
                }

                if (_evolutionNextHuman == null)
                {
                    _evolutionNextHuman = nextHuman.GetComponent<Evolution>();
                }

                _evolutionNextHuman.ShowLevelUpFX();

                if (_playerMoverNextHuman == null)
                {
                    _playerMoverNextHuman = nextHuman.GetComponent<PlayerMover>();
                }

                _playerMoverNextHuman.ContinueRun();
                _evolutionNextHuman.SetCurrentLvl(currentLevel, false);

                switch (stage)
                {
                    case (1):
                        currentLevel = firstEvolution - 1;
                        break;

                    case (2):
                        currentLevel = secondEvolution - 1;
                        break;

                    case (3):
                        currentLevel = thirdEvolution - 1;
                        break;
                }

                //trail.SetTarget(nextHuman.transform);
                //labelPlayer.SetTarget(nextHuman.transform, GetComponent<Evolution>());
                /////////////////////rotateCanvas.SetTarget(nextHuman.transform);

                gameObject.SetActive(false);
            }
            else
            {
                EnlargeScale(true);
            }
        }
    }

    public void SetCurrentLvl(float lvl, bool isBack)
    {
        currentLevel = lvl;
        UpdateText();


        if (isBack)
        {
            bool isNextEvolution = false;

            switch (stage)
            {
                case (1):
                    if (currentLevel < firstEvolution)
                    {
                        ShowLevelUpFX();
                    }
                    else
                    {
                        isNextEvolution = true;
                    }
                    break;

                case (2):
                    if (currentLevel < secondEvolution)
                    {
                        ShowLevelUpFX();
                    }
                    else
                    {
                        isNextEvolution = true;
                    }
                    break;

                case (3):
                    if (currentLevel < thirdEvolution)
                    {
                        ShowLevelUpFX();
                    }
                    else
                    {
                        isNextEvolution = true;
                    }
                    break;

                default:
                    isNextEvolution = true;
                    break;
            }

            if (isNextEvolution)
            {
                AddLevel(0);
            }
        }
        else
        {
            //evolution back

            if (currentLevel < 1)
            {
                OnLose();
                return;
            }

            bool isBackEvolution = false;

            switch (stage)
            {
                case (2):
                    if (currentLevel < firstEvolution)
                    {
                        isBackEvolution = true;
                    }
                    break;

                case (3):
                    if (currentLevel < secondEvolution)
                    {
                        isBackEvolution = true;
                    }
                    break;

                case (4):
                    if (currentLevel < thirdEvolution)
                    {
                        isBackEvolution = true;
                    }
                    else
                    {
                        if (stage == 4 && transform.localScale.x > _firstScale)
                        {
                            float newScale = transform.localScale.x - _percentEnlarge;
                            transform.localScale = new Vector3(newScale, newScale, newScale);
                            return;
                        }
                    }
                    break;
            }

            if (isBackEvolution)
            {
                EvolutionBack(0);
            }
        }
    }

    public void OnLose()
    {
        GetComponent<AnimationState>().OnDead();

        GetComponent<CheckInput>().enabled = false;
        Camera.main.GetComponent<UIController>().GameLose();

        CameraZoom cameraZoom = Camera.main.GetComponent<CameraZoom>();
        cameraZoom.enabled = true;
        cameraZoom.SetTarget(transform);
    }

    public int GetStage()
    {
        return stage;
    }
}
