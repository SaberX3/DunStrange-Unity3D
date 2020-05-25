using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Items : ScriptableObject
{
    public string itemname;
    public Sprite itemimg;
    [TextArea]
    public string itmeinfo;
    //血上限
    public int hpmax;
    //攻击力
    public int attack;
    //体力
    public int physicalstrength;
    //防御
    public int defense;
    //暴击率
    public int criticalrate;
    //闪避率
    public int dodgerate;
    //跳跃次数
    public int jumpnum;
}
