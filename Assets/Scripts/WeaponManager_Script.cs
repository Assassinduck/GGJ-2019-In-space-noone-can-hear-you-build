using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WeaponManager_Script : MonoBehaviour
{
    public int WeaponRef;
    public List<Transform> TargetList;
    public float FireRate;
    public float MinFireRate;
    public float Damange;
    public float MaxDamage;
    public GameObject Bullet_Prefab;
    public Transform[] Guns;
    public float AimSpeed;
    private bool _runOnce;
    public Transform[] Pistons;
    public float BulletSpeed;

    void Update()
    {
        if (WeaponRef == 0)
        {
            if (TargetList.Count == 0)
            {
                CancelInvoke("Target");
                _runOnce = false;
            }
            if (TargetList.Count > 0)
            {
                if (!_runOnce)
                {
                    _runOnce = true;
                    Invoke("Target", 1f);
                }
                Aim();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            if (!TargetList.Contains(col.transform))
            {
                TargetList.Add(col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            if (TargetList.Contains(col.transform))
            {
                TargetList.Remove(col.transform);
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (WeaponRef == 1)
        {
            if (col.tag == "Enemy")
            {
                col.GetComponent<EnemyBehavoire_Script>().TakeDamage((Damange * Time.deltaTime));
            }
        }
    }

    void Target()
    {
        if (TargetList[0] == null)
        {
            if (TargetList.Count > 0)
            {
                TargetList.Remove(TargetList[0]);
            }
        }
        if (TargetList.Count > 0)
        {
            Shoot();
        }
        //Debug.Log("Target is: " +TargetList[0].name);
    }

    void Shoot()
    {
        if (TargetList.Count > 0)
        {
            foreach (Transform g in Guns)
            {
                //Debug.Log(g.GetChild(1).name);
                GameObject b = Instantiate(Bullet_Prefab, g.GetChild(1).position, Quaternion.identity);

                b.GetComponent<Rigidbody>().velocity = g.GetChild(1).forward * BulletSpeed;
                b.GetComponent<Bullet_Script>().Damange = Damange;
                if (Pistons != null)
                {
                    foreach (Transform p in Pistons)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(p.DOLocalMoveZ(0.680f, (FireRate/3)));
                        s.AppendInterval((FireRate / 3));
                        s.Append(p.DOLocalMoveZ(0.92f, (FireRate / 4)));
                    }
                }

                //Debug.Log("Shoot");
            }
        }
        Invoke("Target", FireRate);
    }

    void Aim()
    {
        float step = Time.time * AimSpeed;

        if (TargetList.Count > 0 && TargetList[0] != null)
        {
            foreach (Transform g in Guns)
            {
                g.transform.LookAt(TargetList[0].position);
            }
        }
    }
}
