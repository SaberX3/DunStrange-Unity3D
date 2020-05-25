using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public bool isbatter = true;
    public int clearroomnum = -1;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        isbatter = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void Clearroomcount()
    {
        instance.clearroomnum++;

    }
}
