using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PPControl : MonoBehaviour
{
    static PPControl instance;
    public Image pp1;
    public Image pp2;
    public GameObject player;
    public int phypower = 0;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        phypower=player.GetComponent<Movecontrol>().ppnum;
        if (phypower == 0)
        {
            pp1.gameObject.SetActive(false);
            pp2.gameObject.SetActive(false);
        }
        if (phypower == 1)
        {
            pp1.gameObject.SetActive(true);
            pp2.gameObject.SetActive(false);
        }
        if (phypower == 2)
        {
            pp1.gameObject.SetActive(true);
            pp2.gameObject.SetActive(true);
        }
    }
}
