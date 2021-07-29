using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountCoins : MonoBehaviour
{
    [SerializeField] private Text coinsText;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        coinsText.text = PlayerPrefs.GetInt("coins").ToString();
    }

    public void EnlargeCoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 100);

        Initialization();
    }
}
