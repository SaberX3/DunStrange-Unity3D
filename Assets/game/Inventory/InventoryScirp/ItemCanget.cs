using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCanget : MonoBehaviour
{
    public Items thisitem;
    public Inventory mybag;
    public bool canget = false;
    public Animator anime;
    void Start()
    {
        anime = GetComponent<Animator>();
        anime.SetTrigger("create");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && canget && this.tag != "hpplus")
        {
            AddnewItemTOmybag();
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("Player") && canget && this.tag == "hpplus")
        {
            PlayerManager.Recove(15);
            Destroy(gameObject);
        }
    }
    public void AddnewItemTOmybag()
    {
        // mybag.itemslist.Add(thisitem);


        for (int i = 0; i < mybag.itemslist.Count; i++)
        {
            if (mybag.itemslist[i] == null)
            {
                mybag.itemslist[i] = thisitem;
                break;
            }

        }
        InventoryManager.RefreshItem();
    }

    public void iscanget()
    {
        canget = true;
    }
}
