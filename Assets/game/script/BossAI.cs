using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class BossAI : MonoBehaviour
{
    [Header("Character")]
    public Rigidbody2D BossRD;
    public Animator bossanime;
    [Header("moveParams")]
    public float speed;

    [Header("hurtparam")]
    public float hurttime;
    public int hp;
    [Header("attackparam")]
    public GameObject attackarea;
    public GameObject skill2area;
    [Header("state")]
    public bool ishurt = false;
    public bool isdead = false;
    public bool isleft = false;
    public bool canmove = true;
    public bool isattack = false;
    public bool isskill = false;
    [Header("skill")]
    public GameObject summon1;
    public GameObject summon2;
    public GameObject damageuiprefab;
    public float cdtime;
    public float lasttime = 0;
    public float waittime;
    public GameObject endui;
    // Start is called before the first frame update
    void Start()
    {
        BossRD = GetComponent<Rigidbody2D>();
        bossanime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canmove && !isdead)
        {
            sidecheck();
            move();
            if (Time.time - lasttime >= cdtime)
            {
                lasttime = Time.time;
                int type = Random.Range(0, 2);
                switch (type)
                {
                    case 0:
                        skill1();
                        break;
                    case 1:
                        skill3();
                        break;
                    default:
                        break;
                }
            }
        }

    }

    public void move()
    {
        if (isleft)
        {
            BossRD.velocity = new Vector2(-speed, 0);
            bossanime.SetBool("isrun", true);
        }
        else
        {
            BossRD.velocity = new Vector2(speed, 0);
            bossanime.SetBool("isrun", true);
        }
    }

    public void sidecheck()
    {
        if (transform.position.x - GameObject.FindWithTag("Player").transform.position.x > 0)
        {
            isleft = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            isleft = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    //冲刺

    //受伤
    public void dead()
    {

    }
    public void attack()
    {
        if (!isattack && !isskill && canmove && !isdead)
        {
            bossanime.SetTrigger("attack");
            bossanime.SetBool("isrun", false);
            BossRD.velocity = new Vector2(0, 0);
            canmove = false;
            lasttime = Time.time;
            isattack = true;
        }
    }
    public void Onattackarea()
    {
        attackarea.SetActive(true);
        // attackarea.transform.localPosition = new Vector3(1.48f, 0, 0);
    }
    public void Offattackarea()
    {
        attackarea.SetActive(false);
    }
    public void endattack()
    {

    }
    //技能1
    private void skill1()
    {
        BossRD.velocity = new Vector2(0, 0);
        canmove = false;
        isskill = true;
        bossanime.SetBool("isrun", false);
        StartCoroutine(endwait(waittime));
        bossanime.SetTrigger("skill1");
    }

    private void skll1summon()
    {
        GameObject newone = Instantiate(summon1);
        newone.transform.position = new Vector3(transform.position.x + 1.68f * transform.localScale.x / Mathf.Abs(transform.localScale.x), transform.position.y, transform.position.z);
        newone.transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
        newone.GetComponent<Rigidbody2D>().velocity = new Vector3(transform.localScale.x / Mathf.Abs(transform.localScale.x), 0f, 0f) * 10f;
    }


    private void skill2()
    {
        sidecheck();
        if (isleft)
            BossRD.velocity = new Vector2(-1, 0);
        else
            BossRD.velocity = new Vector2(1, 0);
        bossanime.SetTrigger("skill2");
        lasttime = Time.time;
    }

    public void Onskill2area()
    {
        skill2area.SetActive(true);
        // skill2area.transform.localPosition = new Vector3(1.48f, 0, 0);
    }
    public void Offskill2area()
    {
        skill2area.SetActive(false);
        lasttime = Time.time;
        StartCoroutine(attackwait(waittime));
        isattack = false;
        BossRD.velocity = new Vector2(0, 0);
    }
    private void skill3()
    {
        BossRD.velocity = new Vector2(0, 0);
        canmove = false;
        isskill = true;
        bossanime.SetBool("isrun", false);
        StartCoroutine(attackwait(waittime));
        bossanime.SetTrigger("skill3");
    }
    private void skll2summon()
    {
        GameObject newone = Instantiate(summon2);
        newone.transform.position = new Vector3(transform.position.x + 1.47f * transform.localScale.x / Mathf.Abs(transform.localScale.x), transform.position.y - 0.17f, transform.position.z);
        newone.transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
        newone.GetComponent<Rigidbody2D>().velocity = new Vector3(transform.localScale.x / Mathf.Abs(transform.localScale.x), 0f, 0f) * 10f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!ishurt)
        {
            if (other.transform.tag == "attackarea" && !isdead && !ishurt)
            {
                ishurt = true;
                StartCoroutine(endhurt(hurttime));
                endattack();
                int damage = 0;
                BossRD.velocity = new Vector2(0, 0);
                GameObject damageui = Instantiate(damageuiprefab);
                damageui.transform.position = transform.position;
                damage = PlayerManager.TakeAttack();
                hp -= damage;
                damageui.GetComponent<DamageUIControl>().SetText(damage);

                if (hp <= 0)
                {
                    isdead = true;
                    bossanime.SetTrigger("dead");
                    endui.SetActive(true);
                    gameover.happyend();
                    if (transform.parent.GetComponent<BatterControl>())
                    {
                        transform.parent.GetComponent<BatterControl>().whenenemyddead();
                    }
                }

            }
            if (other.transform.tag == "summon1" && !isdead && !ishurt)
            {
                ishurt = true;
                StartCoroutine(endhurt(hurttime));
                endattack();
                int damage = 20;
                BossRD.velocity = new Vector2(0, 0);
                GameObject damageui = Instantiate(damageuiprefab);
                damageui.transform.position = transform.position;
                hp -= damage;
                damageui.GetComponent<DamageUIControl>().SetText(damage);
                if (hp <= 0)
                {
                    isdead = true;
                    bossanime.SetTrigger("dead");
                    endui.SetActive(true);
                    gameover.happyend();
                    if (transform.parent.GetComponent<BatterControl>())
                    {
                        transform.parent.GetComponent<BatterControl>().whenenemyddead();
                    }
                }

            }
        }
    }

    IEnumerator endhurt(float time)
    {
        yield return new WaitForSeconds(time);
        ishurt = false;
    }
    IEnumerator endwait(float time)
    {
        yield return new WaitForSeconds(time);
        canmove = true;
        isskill = false;
    }
    IEnumerator attackwait(float time)
    {
        yield return new WaitForSeconds(time);
        canmove = true;
        lasttime = Time.time;
    }

}
