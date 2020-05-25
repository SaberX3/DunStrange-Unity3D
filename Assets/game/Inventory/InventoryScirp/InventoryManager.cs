using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory mybag;
    public Inventory myequipment;
    public GameObject slotGrid;
    public GameObject equipmentGrid;
    public GameObject emptyslot;
    public GameObject emptyequipslot;
    public Text iteminfo;
    public GameObject infoBG;
    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> equipmentlist = new List<GameObject>();
    public List<Items> equipmetitemlist = new List<Items>();
    public List<Items> itemlist = new List<Items>();
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        instance.itemlist = instance.mybag.itemslist;
    }
    private void Start()
    {
        instance.equipmetitemlist = instance.myequipment.itemslist;
        RefreshequipmentList();
        Whenitemequiped();

    }
    private void OnEnable()
    {
        RefreshItem();
        Refreshequipment();
        RefreshequipmentList();
        instance.iteminfo.text = "";
    }

    public static void RefreshInfo(string info)
    {
        instance.iteminfo.gameObject.SetActive(true);
        instance.infoBG.SetActive(true);
        instance.iteminfo.text = info;
    }
    public static void Infoclose()
    {
        instance.iteminfo.gameObject.SetActive(false);
        instance.infoBG.SetActive(false);
    }
    // public static void CreateNewItem(Items item)
    // {
    //     Slot newItem = Instantiate(instance.slotprefab, instance.slotGrid.transform.position, Quaternion.identity);
    //     newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
    //     newItem.slotItem = item;
    //     newItem.slotImg.sprite = item.itemimg;
    // }
    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        for (int i = 0; i < instance.itemlist.Count; i++)
        {
            instance.slots.Add(Instantiate(instance.emptyslot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].transform.localScale = new Vector3(1f, 1f, 1f);
            instance.slots[i].GetComponentInChildren<Slot>().SetupSlot(instance.itemlist[i]);
        }
    }
    public static void Refreshequipment()
    {
        for (int i = 0; i < instance.equipmentGrid.transform.childCount; i++)
        {
            if (instance.equipmentGrid.transform.childCount == 0)
                break;
            Destroy(instance.equipmentGrid.transform.GetChild(i).gameObject);
            instance.equipmentlist.Clear();
        }
        for (int i = 0; i < instance.myequipment.itemslist.Count; i++)
        {
            instance.equipmentlist.Add(Instantiate(instance.emptyequipslot));
            instance.equipmentlist[i].transform.SetParent(instance.equipmentGrid.transform);
            instance.equipmentlist[i].transform.localScale = new Vector3(1f, 1f, 1f);
            instance.equipmentlist[i].GetComponentInChildren<Slot>().SetupSlot(instance.myequipment.itemslist[i]);
        }
    }

    public static void RefreshequipmentList()
    {
        instance.equipmetitemlist.Clear();
        foreach (Transform child in instance.equipmentGrid.transform)
        {
            if (child.GetComponentInChildren<Slot>().slotItem != null)
                instance.equipmetitemlist.Add(child.GetComponentInChildren<Slot>().slotItem);
            else
                instance.equipmetitemlist.Add(null);
        }
        PlayerManager.ChangeAttribute(instance.equipmetitemlist);
        instance.myequipment.itemslist = instance.equipmetitemlist;
    }
    public static void Whenitemequiped()
    {
        instance.itemlist.Clear();
        foreach (Transform child in instance.slotGrid.transform)
        {
            if (child.GetComponentInChildren<Slot>().slotItem != null)
                instance.itemlist.Add(child.GetComponentInChildren<Slot>().slotItem);
            else
                instance.itemlist.Add(null);
        }

    }
    public static void cleanbag()
    {
        instance.mybag.itemslist.Clear();
        for (int i = 0; i < 15; i++)
        {
            instance.mybag.itemslist.Add(null);
        }
        instance.myequipment.itemslist.Clear();
        for (int i = 0; i < 4; i++)
        {
            instance.myequipment.itemslist.Add(null);
        }
    }
}
