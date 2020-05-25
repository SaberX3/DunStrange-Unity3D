using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originparent;
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        originparent = transform.parent;
        transform.SetParent(transform.parent.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        // Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }
    //结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            transform.position = originparent.position;
            transform.SetParent(originparent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            return;
        }
        if (eventData.pointerCurrentRaycast.gameObject.name == "Itemimage")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originparent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originparent);
            InventoryManager.RefreshequipmentList();
            InventoryManager.Whenitemequiped();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            eventData.pointerCurrentRaycast.gameObject.transform.Find("Item").position = originparent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.Find("Item").SetParent(originparent);
            InventoryManager.RefreshequipmentList();
            InventoryManager.Whenitemequiped();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        if (eventData.pointerCurrentRaycast.gameObject.name == "EquipSlot(Clone)")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            eventData.pointerCurrentRaycast.gameObject.transform.Find("Item").position = originparent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.Find("Item").SetParent(originparent);
            InventoryManager.RefreshequipmentList();
            InventoryManager.Whenitemequiped();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        transform.SetParent(originparent);
        transform.position = originparent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    // Start is called before the first frame update

}
