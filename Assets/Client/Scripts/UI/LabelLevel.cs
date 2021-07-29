using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LabelLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLevel;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        if(PlayerPrefs.GetInt("currentLevel") == 0)
        {
            PlayerPrefs.SetInt("currentLevel", 1);
        }

        nameLevel.text = "LEVEL " + PlayerPrefs.GetInt("currentLevel").ToString();
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel") + 1);
    }
}
