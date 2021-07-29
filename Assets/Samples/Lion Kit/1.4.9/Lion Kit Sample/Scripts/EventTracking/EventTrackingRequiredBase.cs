using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Text.RegularExpressions;
using LionStudios;

public class EventTrackingRequiredBase : LionMenu
{
    private void Start()
    {
        MethodInfo[] methodInfos = typeof(EventTrackingRequired_v1).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        VerticalLayoutGroup verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        while (verticalLayoutGroup.transform.childCount > 0)
            DestroyImmediate(verticalLayoutGroup.transform.GetChild(0).gameObject);

        foreach (MethodInfo methodInfo in methodInfos)
        {
            AddButton(verticalLayoutGroup.transform, methodInfo);
        }
    }

    void AddButton(Transform parent, MethodInfo methodInfo)
    {
        GameObject buttonGo = new GameObject(methodInfo.Name, typeof(RectTransform));
        buttonGo.transform.SetParent(parent);
        buttonGo.AddComponent<Image>();

        Button button = buttonGo.AddComponent<Button>();
        button.onClick.AddListener(() => { Debug.Log("Invoking " + methodInfo.Name); methodInfo.Invoke(this, new object[0]); });

        GameObject textGo = new GameObject("Text", typeof(RectTransform));
        textGo.transform.SetParent(buttonGo.transform);

        RectTransform rt = textGo.transform as RectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;

        buttonGo.transform.localScale = Vector3.one;

        Text text = textGo.AddComponent<Text>();
        // Set the text name to that of the method being called.
        // Regex adds spaces between words for camel case
        text.text = Regex.Replace(methodInfo.Name, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");

        text.color = Color.black;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 45;
        text.alignment = TextAnchor.MiddleCenter;
        text.fontStyle = FontStyle.Bold;
    }

    protected string GetRandomTestGroup()
    {
        if (Random.value < 0.5f)
            return "test_grp_a";

        return "test_grp_b";
    }

    protected Analytics.CurrencyTransaction GetTransaction(string currencyName = "coins")
    {
        return new Analytics.CurrencyTransaction { currency = currencyName, amount = Random.Range(-100, 100), remaining = Random.Range(1, 1000) };
    }

    protected Analytics.CurrencyTransaction[] GetTransactions()
    {
        int numTransactions = Random.Range(1, 4);

        var transactions = new Analytics.CurrencyTransaction[numTransactions];
        for (int i = 0; i < transactions.Length; i++)
            transactions[i] = GetTransaction("coins_" + i);

        return transactions;
    }

    protected Analytics.Currency[] GetCurrencies()
    {
        int totalEarnedGems = Random.Range(1, 1000);
        int totalEarnedCoins = Random.Range(1, 1000);
        return new Analytics.Currency[]
        {
            new Analytics.Currency { name = "gems", totalEarned = totalEarnedGems, totalSpent = Random.Range(1, totalEarnedGems)},
            new Analytics.Currency { name = "coins", totalEarned = totalEarnedCoins, totalSpent = Random.Range(1f, totalEarnedCoins) }
        };
    }
} 
