using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected Animator anime;


    protected virtual void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame

    public void dead()
    {
        Destroy(gameObject);
    }


}
