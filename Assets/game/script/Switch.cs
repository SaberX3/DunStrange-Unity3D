using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject off;
    public GameObject on;
    public GameObject chest;
    public GameObject door;
    public bool ison = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ison)
        {
            door.transform.position = new Vector3(door.transform.position.x, Mathf.Lerp(door.transform.position.y, -2.45f, 0.5f), door.transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "summon1")
        {
            off.SetActive(false);
            on.SetActive(true);
            if (!ison)
            {
                ison = true;
                chest.SetActive(true);
            }
        }
    }
}
