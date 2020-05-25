using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpuiControl : MonoBehaviour
{
    // Start is called before the first frame update
    static HpuiControl instance;
    public Text hp;
    public Text level;
    public Image hpred;
    public Image hpwhite;
    public int maxhp;
    public int nowhp;
    public int lasthp;
    public int nowlevel;
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (instance.nowhp >= 0)
            instance.hp.text = instance.nowhp.ToString() + '/' + instance.maxhp.ToString();
        else
            instance.hp.text = "0" + '/' + instance.maxhp.ToString();
        hpred.fillAmount = (float)instance.nowhp / (float)instance.maxhp;
        instance.level.text = instance.nowlevel.ToString();
        if (instance.nowhp != instance.lasthp)
        {
            if (instance.lasthp > instance.nowhp)
                instance.lasthp = (int)Mathf.Lerp(instance.lasthp, instance.nowhp, 0.01f);
            if (instance.lasthp < instance.nowhp)
                instance.lasthp = instance.nowhp;
        }
        hpwhite.fillAmount = (float)instance.lasthp / (float)instance.maxhp;
    }

    public static void changenowhp(int hpnew)
    {
        instance.nowhp = hpnew;
    }
    public static void changemaxhp(int hpnew)
    {
        instance.maxhp = hpnew;
    }
    public static void changelevel(int newlevel)
    {
        instance.nowlevel = newlevel;
    }
}
