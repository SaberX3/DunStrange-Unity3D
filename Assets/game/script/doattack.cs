using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doattack : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject skuruman;
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
            skuruman.GetComponent<Bandit>().attack();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            skuruman.GetComponent<Bandit>().attack();
        }
    }
}
