using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fxmanager : MonoBehaviour
{
    public static Fxmanager instance;

    public GameObject dust;
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        //初始化单例
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Setdust()
    {
        GameObject newdust = Instantiate(dust);
        newdust.transform.position = player.transform.position;
        newdust.transform.localScale = player.transform.localScale;
        newdust.transform.SetParent(player.transform.parent);

    }
}
