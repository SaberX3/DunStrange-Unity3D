using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogai : Enemy
{
    private Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    private float leftx;
    private float rightx;
    // public Animator anime;
    private bool isleft = true;
    public float speed, jumpforce;
    public LayerMask ground;
    private Collider2D coll;
    private int side = 1;
    public bool isground;
    public GameObject damageuiprefab;
    public bool canmove = false;

    public int damage = 20;
    public int hp = 40;

    public bool ishurt = false;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.instance.isbatter)
        {
            Statuscheck();
            isground = coll.IsTouchingLayers(ground);
            rb.gravityScale = 1;
            if (!canmove)
            {
                Move();
                canmove = true;
            }
        }
        else
        {
            canmove = false;
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
        }
    }
    void Move()
    {
        if (isleft)
        {
            if (isground)
            {
                rb.velocity = new Vector2(-speed, jumpforce);
                anime.SetBool("isjump", true);
            }

        }
        else
        {
            if (isground)
            {
                rb.velocity = new Vector2(speed, jumpforce);
                anime.SetBool("isjump", true);
            }
        }
    }
    public void sidecheck()
    {
        if (transform.position.x > rightx)
        {
            isleft = true;
            side = 1;
            transform.localScale = new Vector3(side, 1, 1);
        }
        if (transform.position.x < leftx)
        {
            isleft = false;
            side = -1;
            transform.localScale = new Vector3(side, 1, 1);
        }
    }
    void Statuscheck()
    {
        if (anime.GetBool("isjump"))
        {
            if (rb.velocity.y < 0.1)
            {
                anime.SetBool("isjump", false);
                anime.SetBool("isfall", true);
            }
        }
        if (coll.IsTouchingLayers(ground) && anime.GetBool("isfall"))
        {
            anime.SetBool("isfall", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "attackarea" && !ishurt)
        {
            int damage = 0;
            GameObject damageui = Instantiate(damageuiprefab);
            damageui.transform.position = transform.position;
            damage = PlayerManager.TakeAttack();
            damageui.GetComponent<DamageUIControl>().SetText(damage);
            hp -= damage;
            ishurt = true;
            StartCoroutine(hurt());
            if (hp <= 0)
            {
                anime.SetBool("isdead", true);
                if (transform.parent.GetComponent<BatterControl>())
                {
                    transform.parent.GetComponent<BatterControl>().whenenemyddead();
                }
            }
        }
        if (other.transform.tag == "summon1" && !ishurt)
        {
            int damage = 20;
            GameObject damageui = Instantiate(damageuiprefab);
            damageui.transform.position = transform.position;
            hp -= damage;
            damageui.GetComponent<DamageUIControl>().SetText(damage);
            ishurt = true;
            StartCoroutine(hurt());
            if (hp <= 0)
            {
                anime.SetBool("isdead", true);
                if (transform.parent.GetComponent<BatterControl>())
                {
                    transform.parent.GetComponent<BatterControl>().whenenemyddead();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerManager.GetDamage(damage);
        }
    }

    IEnumerator hurt()
    {
        yield return new WaitForSeconds(0.5f);
        ishurt = false;
    }
}
