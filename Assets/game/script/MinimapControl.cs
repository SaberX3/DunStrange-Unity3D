using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapControl : MonoBehaviour
{
    public GameObject minimapnow;
    public GameObject minimapold;
    public GameObject minimapnew;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changenow(bool visible)
    {
        minimapnow.SetActive(visible);
    }
    public void changenew(bool visible)
    {
        minimapnew.SetActive(visible);
    }
    public void changeold(bool visible)
    {
        minimapold.SetActive(visible);
    }
}
