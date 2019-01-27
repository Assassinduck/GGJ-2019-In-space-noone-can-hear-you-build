using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehavoire_Script : MonoBehaviour
{

    private Transform _coreTransform;
    private NavMeshAgent _navMeshAgent;
    public float MaxHealth;
    private float _currentHealth;
    private Image _healthBar;
    private LevelMananger_Script _levelManangerScript;
    private bool _runOnce;
    public GameObject ScrapDrop;

    
    void Start()
    {
        _currentHealth = MaxHealth;
        _healthBar = transform.GetChild(1).GetChild(1).GetComponent<Image>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_coreTransform.position);

    }

    
    void Update()
    {
        /* if (Input.GetButtonDown("Jump")) //For debugging/testing
         {
             TakeDamage(5);
         }*/
        transform.LookAt(Camera.main.transform.position);
    }

    public void TakeDamage(float d)
    {

        _currentHealth -= d;
        _healthBar.fillAmount = _currentHealth / MaxHealth;
        if (_currentHealth <= 0)
        {
            _levelManangerScript.RemoveEnemy();
            Instantiate(ScrapDrop, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //Score score/drop cash...
        }
        //Debug.Log("Take dmg");
        //Debug.Log("Current" +_currentHealth);
        Invoke("InvFrames",3f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Core")
        {
            Debug.Log("Lose Life!");
            _levelManangerScript.LoseLife(1);
            _levelManangerScript.RemoveEnemy();
            Destroy(this.gameObject);
        }

        if (col.tag == "Bullet")
        {
            TakeDamage(col.GetComponent<Bullet_Script>().Damange);
            Destroy(col.gameObject);
        }

        if (col.tag == "weapon")
        {
            if (col.transform.parent.parent.GetComponent<Player_behavior>().attack_mode)
            {
                TakeDamage(5);
            }
        }
    }

    void InvFrames()
    {
        _runOnce = false;
    }

    public void LinkUp(LevelMananger_Script l, Transform c)
    {
        _levelManangerScript = l;
        _coreTransform = c;
    }
}
