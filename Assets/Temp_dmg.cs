using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp_dmg : MonoBehaviour
{
    public float enehealth;
    public Image health;
    public float maxenehealth;

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "weapon")
        {
           if (collision.transform.parent.parent.GetComponent<Player_behavior>().attack) { 
            enehealth -= 2;
            health.fillAmount = enehealth / maxenehealth;

            
            Debug.Log("dmg");
            }
        }
    }

    void Update()
    {

        
    }
}
