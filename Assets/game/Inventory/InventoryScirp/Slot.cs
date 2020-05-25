using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Items slotItem;
    public Image slotImg;
    public GameObject iteminslot;
    public string slotinfo;
    public void PointEnter() { InventoryManager.RefreshInfo(slotinfo); }
    public void PointOut()
    {
        InventoryManager.Infoclose();
    }

    public void SetupSlot(Items item)
    {
        if (item == null)
        {
            slotImg.gameObject.SetActive(false);
            return;
        }
        slotImg.sprite = item.itemimg;
        slotinfo = item.itmeinfo;
        slotItem = item;

    }
}
