using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Savelist", menuName = "PlayerData/New Savelist")]
public class Savelist : ScriptableObject
{
    public List<PlayerSave> List = new List<PlayerSave>();
}
