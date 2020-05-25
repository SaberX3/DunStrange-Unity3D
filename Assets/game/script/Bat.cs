using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public int hp;
    public bool ishurt = false;
    public Animator anime;
    public GameObject damageuiprefab;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "attackarea" && !ishurt)
        {
            int damage = 0;
            GameObject damageui = Instantiate(damageuiprefab);
            damageui.transform.localPosition = transform.position;
            damage = PlayerManager.TakeAttack();
            damageui.GetComponent<DamageUIControl>().SetText(damage);
            hp -= damage;
            ishurt = true;
            StartCoroutine(hurt());
            if (hp <= 0)
            {
                anime.SetTrigger("dead");
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
            damageui.transform.localPosition = transform.position;
            hp -= damage;
            damageui.GetComponent<DamageUIControl>().SetText(damage);
            ishurt = true;
            StartCoroutine(hurt());
            if (hp <= 0)
            {
                anime.SetTrigger("dead");
                if (transform.parent.GetComponent<BatterControl>())
                {
                    transform.parent.GetComponent<BatterControl>().whenenemyddead();
                }

            }
        }
    }

    IEnumerator hurt()
    {
        yield return new WaitForSeconds(0.5f);
        ishurt = false;
    }

    private void dead()
    {
        Destroy(this.gameObject);
    }
}
