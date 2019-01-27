using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager_Script : MonoBehaviour
{
    public bool Buildt;
    public GameObject[] RoomPrefabs;
    public float[] RoomCosts;
    public float[] UpgradeCosts;
    public UIMananger_Script UiManangerScript;
    public float DamageIncrease;
    public float FireRateIncrease;
    public  WeaponManager_Script WeaponManagerScript;

    void Start()
    {
        //_weaponManagerScript = GetComponent<WeaponManager_Script>();
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!Buildt)
            {
                UiManangerScript.ToggleBuyScreen(true,RoomCosts[0], RoomCosts[1], RoomCosts[2], RoomCosts[3]);
            }
            else
            {

                ConfigUpgradeUI();
            }

        }

    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!Buildt)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && UiManangerScript.Scrap >= RoomCosts[0])
                {
                    Debug.Log("Buy 1");

                    UiManangerScript.UpdateScrapText(-RoomCosts[0]);
                    RoomCosts[0] *= 0.25f;
                    WeaponManagerScript = RoomPrefabs[1].GetComponent<WeaponManager_Script>();
                    ConfigUpgradeUI();
                    RoomPrefabs[0].SetActive(false);
                    RoomPrefabs[1].SetActive(true);
                    Buildt = true;
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) && UiManangerScript.Scrap >= RoomCosts[1])
                {
                    Debug.Log("Buy 2");
                    UiManangerScript.UpdateScrapText(-RoomCosts[1]);
                    RoomCosts[1] *= 0.25f;
                    WeaponManagerScript = RoomPrefabs[2].GetComponent<WeaponManager_Script>();
                    ConfigUpgradeUI();
                    RoomPrefabs[0].SetActive(false);
                    RoomPrefabs[2].SetActive(true);
                    Buildt = true;
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) && UiManangerScript.Scrap >= RoomCosts[2])
                {
                    Debug.Log("Buy 3");
                    UiManangerScript.UpdateScrapText(-RoomCosts[2]);
                    UiManangerScript.ToggleBuyScreen(false,0,0,0,0);
                    //ConfigUI();
                    RoomCosts[2] *= 0.25f;

                    Buildt = true;
                    
                }

                if (Input.GetKeyDown(KeyCode.Alpha4) && UiManangerScript.Scrap >= RoomCosts[3])
                {
                    Debug.Log("Buy 4");
                    UiManangerScript.UpdateScrapText(-RoomCosts[3]);
                    UiManangerScript.ToggleBuyScreen(false,0,0,0,0);
                    //ConfigUI();
                    RoomCosts[3] *= 0.25f;

                    Buildt = true;
                    
                }
                return;
            }
            else
            {
                if (WeaponManagerScript.WeaponRef == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1) && UiManangerScript.Scrap >= UpgradeCosts[0] && WeaponManagerScript.FireRate > WeaponManagerScript.MinFireRate)
                    {
                        Debug.Log("Upgrade 1");
                        UiManangerScript.UpdateScrapText(-UpgradeCosts[0]);
                        UpgradeCosts[0] *= 1.25f;
                        WeaponManagerScript.FireRate /= 1.10f;
                        ConfigUpgradeUI();
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha2) && UiManangerScript.Scrap >= UpgradeCosts[1] && WeaponManagerScript.Damange < WeaponManagerScript.MaxDamage)
                    {
                        Debug.Log("Upgrade 2");
                        UiManangerScript.UpdateScrapText(-UpgradeCosts[1]);
                        UpgradeCosts[1] *= 1.25f;
                        WeaponManagerScript.Damange *= 1.10f;
                        ConfigUpgradeUI();
                    }
                }
                if (WeaponManagerScript.WeaponRef == 1)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha2) && UiManangerScript.Scrap >= UpgradeCosts[1] && WeaponManagerScript.Damange < WeaponManagerScript.MaxDamage)
                    {
                        Debug.Log("Upgrade 2");
                        UiManangerScript.UpdateScrapText(-UpgradeCosts[1]);
                        UpgradeCosts[1] *= 1.25f;
                        WeaponManagerScript.Damange *= 1.10f;
                        ConfigUpgradeUI();
                    }
                }

            }

        }
    }

    void ConfigUpgradeUI()
    {
        UiManangerScript.ToggleBuyScreen(false, 0, 0, 0, 0);
        if (WeaponManagerScript.WeaponRef == 0)
        {
            if (WeaponManagerScript.FireRate <= WeaponManagerScript.MinFireRate && WeaponManagerScript.Damange >= WeaponManagerScript.MaxDamage)
            {
                UiManangerScript.ToggleUpgradeScreen(true, true, true, "#1 Fire Rate\nMax Upgraded", "#2 Damage\nMax Upgrade");
            }

            if (WeaponManagerScript.FireRate <= WeaponManagerScript.MinFireRate && WeaponManagerScript.Damange < WeaponManagerScript.MaxDamage)
            {
                UiManangerScript.ToggleUpgradeScreen(true, true, true, "#1 Fire Rate\nMax Upgraded", "#2 Damage\n"+UpgradeCosts[1].ToString("F0"));
            }

            if (WeaponManagerScript.FireRate >= WeaponManagerScript.MinFireRate && WeaponManagerScript.Damange >= WeaponManagerScript.MaxDamage)
            {
                UiManangerScript.ToggleUpgradeScreen(true, true, true, "#1 Fire Rate\n" +UpgradeCosts[0].ToString("F0"), "#2 Damage\nMax Upgrade");
            }

            UiManangerScript.ToggleUpgradeScreen(true, true, true, "#1 Fire Rate\n" +UpgradeCosts[0].ToString("F0"), "#2 Damage\n" +UpgradeCosts[1].ToString("F0"));
        }
        if (WeaponManagerScript.WeaponRef == 1)
        {
            if (WeaponManagerScript.Damange < WeaponManagerScript.MaxDamage)
            {
                UiManangerScript.ToggleUpgradeScreen(true, false, true, "", "#2 Damage\n " + UpgradeCosts[1].ToString("F0"));
            }
            if (WeaponManagerScript.Damange >= WeaponManagerScript.MaxDamage)
            {
                UiManangerScript.ToggleUpgradeScreen(true, false, true, "", "#2 Damage\nMax Upgraded");
            }
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            UiManangerScript.ToggleBuyScreen(false,0,0,0,0);
            UiManangerScript.ToggleUpgradeScreen(false,false,false,"0","0");
        }
    }
}
