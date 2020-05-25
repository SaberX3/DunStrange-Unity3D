using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Savelist savelist;
    public int save;
    // Start is called before the first frame update
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
    public int oldhpmax;

    public bool isdead = false;
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        for (int i = 0; i < instance.savelist.List.Count; i++)
        {
            if (instance.savelist.List[i].isselected)
            {
                instance.save = i;
                break;
            }
        }
        instance.hpmax = instance.savelist.List[instance.save].hpmax;
        instance.attack = instance.savelist.List[instance.save].attack;
        instance.physicalstrength = instance.savelist.List[instance.save].physicalstrength;
        instance.defense = instance.savelist.List[instance.save].defense;
        instance.criticalrate = instance.savelist.List[instance.save].criticalrate;
        instance.dodgerate = instance.savelist.List[instance.save].dodgerate;
        instance.level = instance.savelist.List[instance.save].level;
        instance.skillpoint = instance.savelist.List[instance.save].skillpoint;
        instance.jumpnum = instance.savelist.List[instance.save].jumpnum;
        instance.hpnow = instance.savelist.List[instance.save].hpnow;
        instance.levelpoint = instance.savelist.List[instance.save].levelpoint;
        instance.oldhpmax = hpmax;



    }

    private void Start()
    {
        HpuiControl.changelevel(instance.level);
        HpuiControl.changenowhp(instance.hpnow);
        HpuiControl.changemaxhp(instance.hpmax);
    }
    public static void SaveData()
    {
        instance.savelist.List[instance.save].hpmax = instance.hpmax;
        instance.savelist.List[instance.save].attack = instance.attack;
        instance.savelist.List[instance.save].physicalstrength = instance.physicalstrength;
        instance.savelist.List[instance.save].defense = instance.defense;
        instance.savelist.List[instance.save].criticalrate = instance.criticalrate;
        instance.savelist.List[instance.save].dodgerate = instance.dodgerate;
        instance.savelist.List[instance.save].level = instance.level;
        instance.savelist.List[instance.save].skillpoint = instance.skillpoint;
        instance.savelist.List[instance.save].jumpnum = instance.jumpnum;
        instance.savelist.List[instance.save].hpnow = instance.hpnow;
        instance.savelist.List[instance.save].levelpoint = instance.levelpoint;
    }
    public static int TakeAttack()
    {

        int Attacknum = 0;
        if (Random.Range(0, 100) > instance.criticalrate)
        {
            Attacknum = instance.attack;
        }
        else
        {
            Attacknum = 2 * instance.attack;
        }
        return Attacknum;
    }
    public static void GetDamage(int damage)
    {
        if (Random.Range(0, 100) < instance.dodgerate)
        {
            return;
        }
        else
        {
            instance.hpnow -= (damage - instance.defense);
        }
        HpuiControl.changenowhp(instance.hpnow);
        if (instance.hpnow <= 0)
        {
            instance.isdead = true;
        }
    }

    public static void ChangeAttribute(List<Items> equipments)
    {
        Readsave();
        foreach (Items equip in equipments)
        {
            if (equip)
            {
                instance.hpmax += equip.hpmax;
                instance.attack += equip.attack;
                instance.physicalstrength += equip.physicalstrength;
                instance.defense += equip.defense;
                instance.criticalrate += equip.criticalrate;
                instance.dodgerate += equip.dodgerate;
                instance.jumpnum += equip.jumpnum;
            }
        }
        instance.hpnow += instance.hpmax - instance.oldhpmax;
        instance.oldhpmax = instance.hpmax;
        HpuiControl.changenowhp(instance.hpnow);
        HpuiControl.changemaxhp(instance.hpmax);
    }

    public static void Readsave()
    {
        instance.hpmax = instance.savelist.List[instance.save].hpmax;
        instance.attack = instance.savelist.List[instance.save].attack;
        instance.physicalstrength = instance.savelist.List[instance.save].physicalstrength;
        instance.defense = instance.savelist.List[instance.save].defense;
        instance.criticalrate = instance.savelist.List[instance.save].criticalrate;
        instance.dodgerate = instance.savelist.List[instance.save].dodgerate;
        instance.level = instance.savelist.List[instance.save].level;
        instance.skillpoint = instance.savelist.List[instance.save].skillpoint;
        instance.jumpnum = instance.savelist.List[instance.save].jumpnum;
        instance.levelpoint = instance.savelist.List[instance.save].levelpoint;
    }

    public static void Recove(int hp)
    {
        instance.hpnow += hp;
        if (instance.hpnow >= instance.hpmax)
        {
            instance.hpnow = instance.hpmax;
        }
        HpuiControl.changenowhp(instance.hpnow);
    }

    public static void Gameover(int point)
    {
        if (point + instance.savelist.List[instance.save].levelpoint >= 20)
        {
            instance.savelist.List[instance.save].level += 2;
            instance.savelist.List[instance.save].levelpoint = point + instance.savelist.List[instance.save].levelpoint - 20;
            point = 0;
        }
        if (point + instance.savelist.List[instance.save].levelpoint >= 10 && point + instance.savelist.List[instance.save].levelpoint < 20)
        {
            instance.savelist.List[instance.save].level += 1;
            instance.savelist.List[instance.save].levelpoint = point + instance.savelist.List[instance.save].levelpoint - 10;
        }
        else
        {
            instance.savelist.List[instance.save].levelpoint += point;
        }
        instance.savelist.List[instance.save].hpmax = 79 + instance.savelist.List[instance.save].level;
        instance.savelist.List[instance.save].hpnow = instance.savelist.List[instance.save].hpmax;
        instance.savelist.List[instance.save].attack = 9 + instance.savelist.List[instance.save].level;
        instance.savelist.List[instance.save].defense = -1 + instance.savelist.List[instance.save].level;
        instance.savelist.List[instance.save].criticalrate = -1 + instance.savelist.List[instance.save].level;
        instance.savelist.List[instance.save].dodgerate = -1 + instance.savelist.List[instance.save].level;
    }
}
