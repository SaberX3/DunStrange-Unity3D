using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossattackarea : MonoBehaviour
{
    // Start is called before the first frame update
    public BossAI boss;
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
            boss.attack();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            boss.attack();
        }
    }
}
