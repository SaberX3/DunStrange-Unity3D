using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Uimanager : MonoBehaviour
{
    public GameObject minimapbig;
    public GameObject minimapsmall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            minimapbig.SetActive(true);
            minimapsmall.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            minimapbig.SetActive(false);
            minimapsmall.SetActive(true);
        }
    }
}
