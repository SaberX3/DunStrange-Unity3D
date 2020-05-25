using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Movecontrol : MonoBehaviour
{
    [Header("Character")]
    public Rigidbody2D playerRd;
    public Collision coll;
    public Animator playeranime;
    public Animatiocontrol amcontriol;
    public GameObject mybag;
    public GameObject endui;
    [Header("moveParams")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;
    private float side = 0;
    [Header("DashParam")]
    private float dashstart;
    public float dashtime;
    private float dashlefttime;
    private float lastdashtime = -5f;
    public float cdtime;
    public float dashspeed;
    [Header("hurtparam")]
    public float hurttime;
    [Header("status")]
    public bool canjump = true;
    public bool canMove = true;
    public bool iswallGrab = false;
    public bool isdash = false;
    public bool iswallSlide = false;
    public bool ishurt;
    public bool wallJumped = false;
    public bool isbagopen = false;
    public bool isattack = false;
    public bool isskill1 = false;
    public bool isairjump = false;
    public bool isair = false;
    public bool isdead = false;
    [Header("attackparam")]
    public GameObject attackarea;
    [Header("physicalpower")]
    public int ppnum = 2;
    public float reloadtime = 0;
    [Header("skill")]
    public GameObject summon1;
    // Start is called before the first frame update
    void Start()
    {
        playerRd = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        playeranime = GetComponent<Animator>();
        amcontriol = GetComponent<Animatiocontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        //输入获取
        var keyboard = Keyboard.current;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);
        playeranime.SetFloat("Horizontal", Mathf.Abs(x));
        playeranime.SetFloat("Vertical", y);
        playeranime.SetFloat("VerticalVelocity", playerRd.velocity.y);

        if (PlayerManager.instance.isdead && isdead == false)
        {
            isdead = PlayerManager.instance.isdead;
            ishurt = false;
            playerRd.velocity = new Vector2(0, 0);
            playeranime.SetTrigger("dead");
        }
        if (isdead)
        {
            return;
        }
        //受伤效果
        if (ishurt)
        {
            if (Time.time - hurttime >= 0.2f)
            {
                ishurt = false;
            }
        }
        OpenBag();
        dash();
        if (isdash)
        {
            return;
        }
        attack();
        if (coll.onGround)
        {
            isairjump = false;
        }
        if (isair)
        {
            if (coll.onGround)
            {
                isair = false;
                Fxmanager.instance.Setdust();
            }
        }
        //跳跃
        if (canjump && Input.GetButtonDown("Jump"))
        {
            if (coll.onGround)
            {
                jump(Vector2.up);
            }
            if (coll.onWall && !coll.onGround)
            {
                WallJump();
            }
            if (!coll.onGround && !coll.onWall && isairjump == false)
            {
                airjump(Vector2.up);
            }
        }
        if (!wallJumped)
        {
            iswallGrab = coll.onWall && Input.GetKey(KeyCode.LeftShift);
        }

        //滑墙
        if (coll.onWall && !isattack && !isskill1 && !coll.onGround && !wallJumped && Mathf.Abs(x) > 0)
        {
            wallSlide();
            iswallSlide = true;
        }
        else
        {
            iswallSlide = false;
        }
        //弹墙跳和爬墙
        if (iswallGrab)
        {
            playerRd.gravityScale = 0;
            // if (x > .2f || x < -.2f)
            //     playerRd.velocity = new Vector2(playerRd.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            playerRd.velocity = new Vector2(0, y * (speed * speedModifier));
        }
        else
        {
            playerRd.gravityScale = 2;
        }
        //方向
        if (x > 0)
        {
            side = 1;
        }
        if (x < 0)
        {
            side = -1;

        }
        //移动
        if (!ishurt)
        {
            skill1();
            move(dir);
        }
        //冲刺
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (ppnum > 0)
            {
                if (Time.time >= (lastdashtime + cdtime))
                {
                    ppnum--;
                    Readydash();
                }
            }
        }
        //体力恢复
        if (ppnum < 2)
        {
            reloadtime += Time.deltaTime;
            if (reloadtime - 2f > 0.001)
            {
                ppnum++;
                reloadtime = 0f;
            }
        }
    }

    public void move(Vector2 dir)
    {
        if (!canMove)
        {
            return;
        }
        if (iswallGrab)
            return;
        float facedirection = Input.GetAxisRaw("Horizontal");
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        playerRd.velocity = (new Vector2(dir.x * speed, playerRd.velocity.y));
    }
    public void jump(Vector2 dir)
    {

        Fxmanager.instance.Setdust();
        playerRd.velocity = (new Vector2(playerRd.velocity.x, 0));
        playerRd.velocity += dir * jumpForce;
        isair = true;
    }
    public void airjump(Vector2 dir)
    {
        playerRd.velocity = (new Vector2(playerRd.velocity.x, 0));
        playerRd.velocity += dir * jumpForce;
        isairjump = true;
    }
    public void wallSlide()
    {
        playerRd.velocity = new Vector2(playerRd.velocity.x, -slideSpeed);
    }
    public void WallJump()
    {
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(1f));
        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;
        iswallGrab = false;
        jump((Vector2.up + wallDir));
        if (coll.onLeftWall)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        wallJumped = true;
    }
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
        wallJumped = false;
    }
    public void Readydash()
    {
        Fxmanager.instance.Setdust();
        isdash = true;
        dashlefttime = dashtime;
        lastdashtime = Time.time;
    }
    //冲刺
    public void dash()
    {
        if (isdash && !isskill1)
        {
            if (dashlefttime > 0)
            {

                playerRd.velocity = new Vector2(dashSpeed * side, playerRd.velocity.y);

                dashlefttime -= Time.deltaTime;
                ShadowControl.instance.GetFromPool();
            }
            if (dashlefttime <= 0)
            {
                isdash = false;
            }
        }
    }
    //受伤
    public void dead()
    {
        endui.SetActive(true);
        gameover.Badend();
    }
    public void attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!ishurt && !isskill1)
            {
                if (!isattack && coll.onGround)
                {
                    isattack = true;
                    playeranime.SetTrigger("attack");
                    canjump = false;
                    canMove = false;
                    playerRd.velocity = new Vector2(0, 0);
                }
                if (!isattack && !coll.onGround && !iswallSlide && !iswallGrab)
                {
                    isattack = true;
                    canjump = false;
                    canMove = false;
                    playeranime.SetTrigger("jumpattack");

                }
            }
        }
    }
    public void Onattackarea()
    {
        attackarea.SetActive(true);
        attackarea.transform.localPosition = new Vector3(1.48f, 0, 0);
    }
    public void Offattackarea()
    {
        attackarea.SetActive(false);
    }
    public void endattack()
    {
        isattack = false;
        canjump = true;
        canMove = true;
    }
    //技能1
    private void skill1()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isskill1)
        {
            if (ppnum > 0)
            {
                ppnum--;
                isskill1 = true;
                playeranime.SetBool("isskill1", isskill1);
                playeranime.SetTrigger("skill1");
                canjump = false;
                canMove = false;

                if (coll.onGround)
                {
                    playerRd.velocity = new Vector2(0, 0);
                }
            }



        }
    }

    private void endskill1()
    {
        isskill1 = false;
        playeranime.SetBool("isskill1", isskill1);
        canjump = true;
        canMove = true;
    }
    private void skll1summon()
    {
        GameObject newone = Instantiate(summon1);
        newone.transform.position = new Vector3(transform.position.x + 1.68f * transform.localScale.x / Mathf.Abs(transform.localScale.x), transform.position.y, transform.position.z);
        newone.transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
        newone.GetComponent<Rigidbody2D>().velocity = new Vector3(transform.localScale.x / Mathf.Abs(transform.localScale.x), 0f, 0f) * 10f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //受伤
        if (collision.gameObject.tag == "enemy" && !ishurt && !isdead)
        {
            if (transform.position.x > collision.transform.position.x)
            {
                endattack();
                Offattackarea();
                endskill1();
                ishurt = true;
                hurttime = Time.time;
                playerRd.velocity = new Vector2(10, playerRd.velocity.y);
            }
            else if (transform.position.x < collision.transform.position.x)
            {
                endattack();
                Offattackarea();
                endskill1();
                ishurt = true;
                hurttime = Time.time;
                playerRd.velocity = new Vector2(-10, playerRd.velocity.y);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "damage" && !ishurt && !isdead)
        {
            if (transform.position.x > other.transform.position.x)
            {
                endattack();
                Offattackarea();
                endskill1();
                ishurt = true;
                hurttime = Time.time;
                playerRd.velocity = new Vector2(10, playerRd.velocity.y);
            }
            else if (transform.position.x < other.transform.position.x)
            {
                endattack();
                Offattackarea();
                ishurt = true;
                endskill1();
                hurttime = Time.time;
                playerRd.velocity = new Vector2(-10, playerRd.velocity.y);
            }
        }
    }

    private void OpenBag()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            mybag.SetActive(!mybag.activeInHierarchy);
        }
    }

}
