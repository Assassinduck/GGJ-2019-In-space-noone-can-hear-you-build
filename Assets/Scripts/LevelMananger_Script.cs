using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMananger_Script : MonoBehaviour
{
    public GameObject[] Layouts;
    private EnemyBehavoire_Script[] _enemyBehavoireScript;
    private EnemeySpawner_Script _enemeySpawnerScript;
    private UIMananger_Script _uiManangerScript;
    public int CoreHealth;
    public int EnemyCounter;
    public float StartTimer;
    private bool _runOnce;
    private float _activeTimer;

    void Awake()
    {
        _enemeySpawnerScript = GetComponent<EnemeySpawner_Script>();
        _uiManangerScript = GetComponent<UIMananger_Script>();
        HideAllLevels();
        int l = Random.Range(0, Layouts.Length);
        Layouts[l].SetActive(true);
        _enemeySpawnerScript.ActiveLayout = Layouts[l].transform;
        _uiManangerScript.UpdateLifeText(CoreHealth);
        //SetEnemeyCore(l);
       // _enemeySpawnerScript.GetSpawnpoints();

    }

    void Update()
    {
        if (_activeTimer > 0)
        {
            _activeTimer -= Time.deltaTime;
            _uiManangerScript.UpdateTimer("Next Wave In: "+_activeTimer.ToString("F2"));
        }
        if (_activeTimer <= 0 && !_runOnce )
        {
            if (_enemeySpawnerScript.WaveNr < _enemeySpawnerScript.MaxWaves)
            {
                _uiManangerScript.UpdateTimer("Attack Incoming!");
                _runOnce = true;
                _enemeySpawnerScript.GetSpawnpoints();
            }
            else
            {
                Debug.Log("All Waves Done!");
                _uiManangerScript.UpdateTimer("Game Over!");
            }

        }

    }

    void HideAllLevels()
    {
        foreach (GameObject go in Layouts)
        {
            go.SetActive(false);
        }
    }

    public void LoseLife(int i)
    {
        CoreHealth-= i;
        _uiManangerScript.UpdateLifeText(CoreHealth);
        if (CoreHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RemoveEnemy()
    {
        EnemyCounter--;
        if (EnemyCounter <= 0 && _enemeySpawnerScript.WaveNr < _enemeySpawnerScript.MaxWaves)
        {
            _enemeySpawnerScript.WaveNr++;
            _activeTimer = StartTimer;
            _runOnce = false;

        }
        else if (_enemeySpawnerScript.WaveNr >= _enemeySpawnerScript.MaxWaves)
        {
            Debug.Log("All Waves Done!");
            _uiManangerScript.UpdateTimer("Game Over!");

        }
    }

    /*void SetEnemeyCore(int l)        //for testing, set core in spawner script
    {
        _enemyBehavoireScript = FindObjectsOfType<EnemyBehavoire_Script>();
        foreach (EnemyBehavoire_Script eb in _enemyBehavoireScript)
        {
            eb.CoreTransform = Layouts[l].transform.GetChild(0);
        }

    }*/
}
