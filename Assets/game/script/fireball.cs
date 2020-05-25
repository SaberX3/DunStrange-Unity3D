using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dead());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ground" || other.tag == "attackarea" || other.tag == "summon1")
        {
            Destroy(this.gameObject);
        }
        if (other.tag == "Player")
        {
            PlayerManager.GetDamage(10);
            Destroy(this.gameObject);
        }
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
