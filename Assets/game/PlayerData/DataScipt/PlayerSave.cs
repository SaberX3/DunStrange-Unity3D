using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerSave", menuName = "PlayerData/New PlayerSave")]
public class PlayerSave : ScriptableObject
{
    //基础数据
    [Header("基础数据---------------------")]
    public int hpmax;
    public int hpnow;
    public int attack;
    //体力
    public int physicalstrength;
    //防御
    public int defense;
    //暴击率
    public int criticalrate;
    //闪避率
    public int dodgerate;
    //等级
    public int level;
    //剩余技能点
    public int skillpoint;
    //跳跃次数
    public int jumpnum;
    public int levelpoint;
    //游戏进度数据
    [Header("游戏进度数据---------------------")]
    public bool havesave;
    public bool isselected;
}
