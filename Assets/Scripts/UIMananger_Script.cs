using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMananger_Script : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public float Scrap;
    public GameObject BuyScreen;
    public GameObject UpgradeScreen;
    public TextMeshProUGUI[] BuyTexts;
    public TextMeshProUGUI[] UpgradeText;
    public TextMeshProUGUI ScrapText;
    public TextMeshProUGUI LifeText;
    public GameObject[] SpawnIndiImages;
    
    
    void Start()
    {
        UpdateScrapText(0);
    }

    public void UpdateTimer(string s)
    {
        TimerText.text = s;
    }

    public void ToggleBuyScreen(bool b,float t0, float t1, float t2, float t3)
    {
        BuyTexts[0].text = "#1 Gun \n" + t0.ToString("F0");
        BuyTexts[1].text = "#2 Gas \n" + t1.ToString("F0");
        /*BuyTexts[2].text = "#3 Shock \n" + t2.ToString("F0");
        BuyTexts[3].text = "#4 Gas \n" + t3.ToString("F0");*/
        BuyScreen.SetActive(b);
    }

    public void ToggleUpgradeScreen(bool b, bool fr, bool d, string s0, string s1)
    {
        UpgradeText[0].text = s0;
        UpgradeText[0].transform.parent.gameObject.SetActive(fr);
        UpgradeText[1].transform.parent.gameObject.SetActive(d);
        UpgradeText[1].text = s1;
        UpgradeScreen.SetActive(b);
    }

    public void UpdateScrapText(float s)
    {
        Scrap += s;
        ScrapText.text = "Scrap:" + Scrap.ToString("F0");
    }

    public void UpdateLifeText(int l)
    {
        LifeText.text = "Core Life: " + l;
    }

    public void SpawnIndicators(int sp)
    {
        SpawnIndiImages[sp].SetActive(true);
        Invoke("SpawnIndOff",2f);
    }

    void SpawnIndOff()
    {
        SpawnIndiImages[0].SetActive(false);
        SpawnIndiImages[1].SetActive(false);
        SpawnIndiImages[2].SetActive(false);
        SpawnIndiImages[3].SetActive(false);
    }
}
