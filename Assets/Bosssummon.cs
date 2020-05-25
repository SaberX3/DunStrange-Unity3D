using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosssummon : MonoBehaviour
{
    public int damge;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void end()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.GetDamage(damge);
            Destroy(this.gameObject);
        }

    }
}
