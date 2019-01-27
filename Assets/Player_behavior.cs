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
    public UIMananger_Script UiManangerScript;
    private Animator _animator;

    void Start()
    {
        wrench = transform.GetChild(0);
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
        player_attack();
        attack = Input.GetKeyDown(KeyCode.Space);
        float vert_move = Input.GetAxis("Vertical") * vert_speed * Time.deltaTime;
        float horz_move = Input.GetAxis("Horizontal") * horz_speed * Time.deltaTime;
        transform.Translate((-transform.forward * vert_move) + (-transform.right * horz_move));
        if ((vert_move <0 || vert_move > 0) || (horz_move < 0 || horz_move > 0))
        {
            _animator.Play("mrManRun");
            return;
        }
        else if ( vert_move == 0 && horz_move == 0)
        {
            _animator.Play("mrManIdleanim");
        }



    }
    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy" && !inv)
        {
            health -= 1;
            Healthbar.fillAmount = health / maxhealth;
            Debug.Log(health / maxhealth);
            inv = true;
            Invoke("inv_countdown", inv_frames);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Scrap")
        {
            UiManangerScript.UpdateScrapText(col.transform.GetComponent<ScrapPickup_Script>().ScrapAmount);
            Destroy(col.gameObject);
        }
    }

    void inv_countdown()
    {
        inv = false;
    }

    void player_attack()
    {
        if (attack && !attack_mode)
        {
            wrench.DORotate(new Vector3(0, 100, 90),0.25f);
            attack_mode = true;
            Invoke("player_attackstate", 1);

        }
    }

    void player_attackstate()
    {
        attack_mode = false;
        wrench.DORotate(new Vector3(-100, 100, 90), 0.5f);
    }
}
