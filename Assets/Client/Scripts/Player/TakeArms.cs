using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeArms : MonoBehaviour
{
    [SerializeField] private DamageInfo damageInfo;

    [SerializeField] private GameObject blueSword;
    [SerializeField] private GameObject goldSword;
    [SerializeField] private GameObject woodenShield;

    private float damageGoldSword, damageBlueSword, protectionShield;

    [Header("FX")]
    [SerializeField] private GameObject swordFX;
    [SerializeField] private GameObject shieldFX;

    [SerializeField] private ShowWeapons showWeapons; 
    private bool isShowFX;

    private AnimationState _animationState;

    private Info.WeaponId _currentWeapon;

    private List<Info.WeaponId> weaponIds = new List<Info.WeaponId>();

    private TakeAttack _takeAttack;

    [SerializeField] private bool isFirstStage = false;

    private Evolution _evolution;

    private void Awake()
    {
        _takeAttack = GetComponent<TakeAttack>();
        _evolution = GetComponent<Evolution>();

        isShowFX = showWeapons.isShowFX;

        _currentWeapon = Info.WeaponId.Hands;
        _animationState = GetComponent<AnimationState>();

        damageBlueSword = damageInfo.addDamageBlueSword;
        damageGoldSword = damageInfo.addDamageGoldSword;
        protectionShield = damageInfo.addProtectionShield;
    }

    private void Start()
    {
        if (isFirstStage)
        {
            goldSword.SetActive(true);
            blueSword.SetActive(false);

            weaponIds.Add(Info.WeaponId.GoldSword);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Weapon weapon))
        {
            TakeUpArms(weapon);
        }
    }

    private void TakeUpArms(Weapon weapon)
    {
        _currentWeapon = weapon.GetWeaponId();
        weapon.DeactivateWeapon();

        switch (_currentWeapon)
        {
            case (Info.WeaponId.BlueSword):

                if (isShowFX == false)
                {
                    goldSword.SetActive(false);
                    blueSword.SetActive(true);

                    _animationState.TakeArm();
                }
                else
                {
                    swordFX.SetActive(true);
                }

                weaponIds.Add(Info.WeaponId.BlueSword);
                weaponIds.Remove(Info.WeaponId.GoldSword);

                _takeAttack.ElrageDamage(damageBlueSword);

                _evolution.AddLevel(weapon.GetAddLevel());

                break;

            case (Info.WeaponId.GoldSword):

                if (isShowFX == false)
                {
                    goldSword.SetActive(true);
                    blueSword.SetActive(false);

                    _animationState.TakeArm();
                }
                else
                {
                    swordFX.SetActive(true);
                }

                weaponIds.Remove(Info.WeaponId.BlueSword);
                weaponIds.Add(Info.WeaponId.GoldSword);

                _takeAttack.ElrageDamage(damageGoldSword);

                _evolution.AddLevel(weapon.GetAddLevel());

                break;

            case (Info.WeaponId.WoodenShield):

                if (isShowFX == false)
                {
                    woodenShield.SetActive(true);

                }
                else
                {
                    shieldFX.SetActive(true);
                }

                weaponIds.Add(Info.WeaponId.WoodenShield);

                _takeAttack.ElrageProtection(protectionShield);

                _evolution.AddLevel(weapon.GetAddLevel());

                break;
        }
    }

    public List<Info.WeaponId> GetAllWeapons()
    {
        return weaponIds;
    }

    public void Initialization(List<Info.WeaponId> oldWeapons)
    {
        for (int i = 0; i < oldWeapons.Count; i++)
        {
            switch (oldWeapons[i])
            {
                case (Info.WeaponId.BlueSword):
                    if (isShowFX == false)
                    {
                        goldSword.SetActive(false);
                        blueSword.SetActive(true);

                        _animationState.TakeArm();
                    }
                    else
                    {
                        swordFX.SetActive(true);
                    }

                    weaponIds.Add(Info.WeaponId.BlueSword);
                    weaponIds.Remove(Info.WeaponId.GoldSword);

                    break;

                case (Info.WeaponId.GoldSword):

                    if (isShowFX == false)
                    {
                        goldSword.SetActive(true);
                        blueSword.SetActive(false);

                        _animationState.TakeArm();
                    }
                    else
                    {
                        swordFX.SetActive(true);
                    }

                    weaponIds.Remove(Info.WeaponId.BlueSword);
                    weaponIds.Add(Info.WeaponId.GoldSword);

                    break;

                case (Info.WeaponId.WoodenShield):

                    if (isShowFX == false)
                    {
                        woodenShield.SetActive(true);

                    }
                    else
                    {
                        shieldFX.SetActive(true);
                    }

                    weaponIds.Add(Info.WeaponId.WoodenShield);

                    break;
            }
        }
    }
}
