using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Itembase", menuName = "Inventory/New Itembase")]
public class Itembase : ScriptableObject
{
    public List<GameObject> itemslist = new List<GameObject>();
}
