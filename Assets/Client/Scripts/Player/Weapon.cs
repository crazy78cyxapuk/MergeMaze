using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Info.WeaponId weaponId;
    [SerializeField] private float addLevel;
    [SerializeField] private TextMeshProUGUI textAddLvl;

    private void Start()
    {
        if (textAddLvl != null)
        {
            textAddLvl.text = "+" + addLevel.ToString();
        }
    }

    public Info.WeaponId GetWeaponId()
    {
        return weaponId;
    }

    public void DeactivateWeapon()
    {
        gameObject.SetActive(false);
    }

    public float GetAddLevel()
    {
        return addLevel;
    }
}
