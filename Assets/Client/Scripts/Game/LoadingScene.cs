using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    private void OnEnable()
    {
        string nextLvl = PlayerPrefs.GetString("lastScene");

        if (nextLvl == "")
        {
            SceneManager.LoadScene("lvl8");
        }
        else
        {
            SceneManager.LoadScene(nextLvl);
        }
    }
}
