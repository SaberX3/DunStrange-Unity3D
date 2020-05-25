using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public Itembase itembase;
    public bool canopen = false;
    public Sprite close;
    public Sprite opened;
    private Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        anime.SetTrigger("create");

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            if (Input.GetKeyDown(KeyCode.E) && canopen)
            {
                int i = Random.Range(0, itembase.itemslist.Count);
                GameObject newobj = Instantiate(itembase.itemslist[i]);
                newobj.transform.position = transform.position;
                canopen = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = opened;
            }
        }
    }

    public void iscanopen()
    {
        canopen = true;
    }
}
