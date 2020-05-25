using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{

    [SerializeField] float m_speed = 1.0f;
    [SerializeField] float m_jumpForce = 2.0f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = true;
    private bool m_combatIdle = false;
    private bool m_isDead = false;
    public Transform leftpoint, rightpoint;
    private float leftx;
    private float rightx;
    // public Animator anime;
    private bool isleft = true;
    private bool isdead = false;
    public GameObject damageuiprefab;
    public int hp = 80;
    public bool isattack = false;
    public GameObject attackarea;
    public bool ishurt = false;
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
        m_animator.SetBool("Grounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.instance.isbatter)
        {
            if (isdead == true)
            {
                m_body2d.velocity = new Vector2(0, 0);
                return;
            }
            sidecheck();
            if (!isattack && !ishurt)
            {
                move();
            }

        }
        else
        {
            m_body2d.velocity = new Vector2(0, 0);
            return;
        }

    }
    public void sidecheck()
    {
        if (!ishurt)
        {
            if (transform.position.x > rightx)
            {
                isleft = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (transform.position.x < leftx)
            {
                isleft = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        if (ishurt)
        {
            if (transform.position.x - GameObject.FindWithTag("Player").transform.position.x > 0)
            {
                isleft = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                isleft = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!ishurt)
        {
            if (other.transform.tag == "attackarea" && !isdead)
            {
                ishurt = true;
                endattack();
                sidecheck();
                int damage = 0;
                m_animator.SetTrigger("Hurt");
                m_body2d.velocity = new Vector2(0, 0);
                GameObject damageui = Instantiate(damageuiprefab);
                damageui.transform.position = transform.position;
                damage = PlayerManager.TakeAttack();
                hp -= damage;
                damageui.GetComponent<DamageUIControl>().SetText(damage);

                if (hp <= 0)
                {
                    isdead = true;
                    m_animator.SetBool("isdead", true);
                    m_animator.SetTrigger("Death");

                    if (transform.parent.GetComponent<BatterControl>())
                    {
                        transform.parent.GetComponent<BatterControl>().whenenemyddead();
                    }
                }

            }
            if (other.transform.tag == "summon1" && !isdead)
            {
                ishurt = true;
                endattack();
                sidecheck();
                int damage = 20;
                m_animator.SetTrigger("Hurt");
                m_body2d.velocity = new Vector2(0, 0);
                GameObject damageui = Instantiate(damageuiprefab);
                damageui.transform.position = transform.position;
                hp -= damage;
                damageui.GetComponent<DamageUIControl>().SetText(damage);
                if (hp <= 0)
                {
                    isdead = true;
                    m_animator.SetBool("isdead", true);
                    m_animator.SetTrigger("Death");

                    if (transform.parent.GetComponent<BatterControl>())
                    {
                        transform.parent.GetComponent<BatterControl>().whenenemyddead();
                    }
                }

            }
        }
    }
    private void move()
    {
        if (isleft)
        {
            if (m_grounded)
            {
                m_body2d.velocity = new Vector2(-m_speed, 0);
                m_animator.SetInteger("AnimState", 2);
            }

        }
        else
        {
            if (m_grounded)
            {
                m_body2d.velocity = new Vector2(m_speed, 0);
                m_animator.SetInteger("AnimState", 2);
            }
        }
    }

    public void attack()
    {
        if (!isdead && !ishurt && !isattack)
        {
            m_body2d.velocity = new Vector2(0, 0);
            m_animator.SetTrigger("Attack");
            isattack = true;
        }

    }
    public void takedamage()
    {
        attackarea.SetActive(true);
    }
    private void endattack()
    {
        isattack = false;
        attackarea.SetActive(false);
    }
    private void endhurt()
    {
        ishurt = false;
        isattack = false;
    }
}
