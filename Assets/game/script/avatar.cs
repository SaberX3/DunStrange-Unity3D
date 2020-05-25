using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avatar : MonoBehaviour
{
    public Image image;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = player.GetComponent<SpriteRenderer>().sprite;
    }
}
