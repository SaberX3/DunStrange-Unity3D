using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossattack : MonoBehaviour
{
    public int damage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.GetDamage(damage);
        }
    }
}
