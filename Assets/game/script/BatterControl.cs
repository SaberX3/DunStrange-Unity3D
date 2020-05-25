using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BatterControl : MonoBehaviour
{
    // Start is called before the first frame update

    public int monstercount;
    public GameObject chest;

    void Start()
    {
        monstercount = transform.childCount;
        checkisbatter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraControl.instance.ChangebackDamping();
            checkisbatter();
        }
    }

    public void checkisbatter()
    {
        if (monstercount <= 0)
        {
            if (GameMaster.instance.isbatter)
            {
                GameMaster.Clearroomcount();
            }
            GameMaster.instance.isbatter = false;
            if (chest)
                chest.SetActive(true);
        }
        else
            GameMaster.instance.isbatter = true;
    }

    public void whenenemyddead()
    {
        monstercount--;
        checkisbatter();
    }
}
