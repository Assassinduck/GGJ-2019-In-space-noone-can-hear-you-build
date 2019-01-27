using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player_behavior : MonoBehaviour
{
    public float vert_speed;
    public float horz_speed;
    public float health;
    public float maxhealth;
    private float inv_frames = 5f;
    private bool inv = false;
    public Image Healthbar;
    public bool attack_mode = false;
    public bool attack;
    Transform wrench;
    
    void Start()
    {
        wrench = transform.GetChild(0);   

    }

    // Update is called once per frame
    void Update()
    {
        
        player_attack();
        attack = Input.GetKeyDown(KeyCode.Space);
        float vert_move = Input.GetAxis("Vertical") * vert_speed * Time.deltaTime;
        float horz_move = Input.GetAxis("Horizontal") * horz_speed * Time.deltaTime;

        transform.Translate((-transform.forward * vert_move) + (-transform.right * horz_move));

    }
    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Cube" && !inv)
        {
            health -= 1;
            Healthbar.fillAmount = health / maxhealth;
            Debug.Log(health / maxhealth);
            inv = true;
            Invoke("inv_countdown", inv_frames);

        }
    }
    void inv_countdown()
    {
        inv = false;
    }

    void player_attack()
    {
        if (attack)
        {
            wrench.DORotate(new Vector3(0, 100,-10),2);
            attack_mode = true;
            Invoke("player_attackstate", 2);

        }
       

    }
    void player_attackstate()
    {
        attack_mode = false;
        wrench.DORotate(new Vector3(0, 100, 90), 1);
    }
}
