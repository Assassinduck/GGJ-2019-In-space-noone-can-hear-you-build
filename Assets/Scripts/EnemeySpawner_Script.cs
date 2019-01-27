using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawner_Script : MonoBehaviour
{
    public List<Transform> _spawnpointTransforms;
    public Transform ActiveLayout;
    private Transform _coreTransform;
    public Transform EnemyHolder;
    public GameObject[] EnemyPrefabs;
    private int _ecount;
    private LevelMananger_Script _levelManangerScript;

    [System.Serializable]
    public class WaveClass
    {
        public List<int> MobList;
    }

    public WaveClass[] WaveList;
    public int WaveNr;
    public int MaxWaves;
    private UIMananger_Script _uiManangerScript;

    public int EnemyCounter;


    void Awake()
    {
        _levelManangerScript = GetComponent<LevelMananger_Script>();
        _uiManangerScript = GetComponent<UIMananger_Script>();
        MaxWaves = WaveList.Length - 1;
        //GetSpawnpoints();
    }

    void Update()
    {

    }

    public void GetSpawnpoints()
    {
        /*for (int i = 0; i < ActiveLayout.GetChild(0).childCount; i++)
        {
            _spawnpointTransforms.Add(ActiveLayout.GetChild(0).GetChild(i));
        }*/

        _coreTransform = ActiveLayout.GetChild(1).transform;
        int sp = Random.Range(0, _spawnpointTransforms.Count);
        StartCoroutine(Spawnenemies(sp));
    }
    

    IEnumerator Spawnenemies(int sp)
    {
        Debug.Log("Wave Nr " + WaveNr);
        Debug.Log("Spawn Indi" +sp);
        _uiManangerScript.SpawnIndicators(sp);
        for (int n = 0; n < WaveList[WaveNr].MobList[0]; n++)
        {
            GameObject e = Instantiate(EnemyPrefabs[0], _spawnpointTransforms[sp].position, Quaternion.identity, EnemyHolder);
            e.GetComponent<EnemyBehavoire_Script>().LinkUp(_levelManangerScript, _coreTransform);
            _levelManangerScript.EnemyCounter++;
            yield return new WaitForSeconds(1f);
        }

        for (int f = 0; f < WaveList[WaveNr].MobList[1]; f++)
        {
            GameObject e = Instantiate(EnemyPrefabs[1], _spawnpointTransforms[sp].position, Quaternion.identity, EnemyHolder);
            e.GetComponent<EnemyBehavoire_Script>().LinkUp(_levelManangerScript, _coreTransform);
            _levelManangerScript.EnemyCounter++;
            yield return new WaitForSeconds(1f);
        }

        for (int s = 0; s < WaveList[WaveNr].MobList[2]; s++)
        {
            GameObject e = Instantiate(EnemyPrefabs[2], _spawnpointTransforms[sp].position, Quaternion.identity, EnemyHolder);
            e.GetComponent<EnemyBehavoire_Script>().LinkUp(_levelManangerScript, _coreTransform);
            _levelManangerScript.EnemyCounter++;
            yield return new WaitForSeconds(1f);
        }

    }
}
