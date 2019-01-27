using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    public float Damange;

    void Start()
    {
        Destroy(this.gameObject,0.1f);
    }
}
