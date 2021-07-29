using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIConfig uiConfig;

    [SerializeField] private GameObject ScreenWin;
    [SerializeField] private GameObject ScreenLose;
    [SerializeField] private GameObject ScreenGame;
    [SerializeField] private GameObject ScreenCoins;

    [SerializeField] private LabelLevel labelLevel;
    [SerializeField] private CountCoins countCoins;

    [SerializeField] private string currentLevel;
    [SerializeField] private string nextLevel;

    private float timer;

    private void Awake()
    {
        ScreenCoins.SetActive(true);
        ScreenGame.SetActive(true);

        ScreenLose.SetActive(false);
        ScreenWin.SetActive(false);

        timer = uiConfig.timer;
    }

    private void OnEnable()
    {
        GA.instance.OnLevelStart(currentLevel);
    }

    public void GameWin()
    {
        if (ScreenLose.activeSelf == false)
        {
            GA.instance.OnLevelComplete(currentLevel);
            StartCoroutine(TimerGameWin());
        }
    }

    public void GameLose()
    {
        if (ScreenWin.activeSelf == false)
        {
            GA.instance.OnLevelFailed(currentLevel);
            StartCoroutine(TimerGameLose());
        }
    }

    IEnumerator TimerGameWin()
    {
        PlayerPrefs.SetString("lastScene", nextLevel);

        labelLevel.NextLevel();

        yield return new WaitForSeconds(timer);

        ScreenWin.SetActive(true);
        ScreenGame.SetActive(false);
    }

    IEnumerator TimerGameLose()
    {
        GameObject obj = GameObject.FindWithTag("Finish");

        if(obj != null)
        {
            obj.SetActive(false);
        }

        yield return new WaitForSeconds(timer);

        ScreenLose.SetActive(true);
        ScreenGame.SetActive(false);
        ScreenCoins.SetActive(false);
    }

    #region Buttons

    public void NextLevel()
    {
        countCoins.EnlargeCoins();
        SceneManager.LoadScene(nextLevel);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    #endregion
}
