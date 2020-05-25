using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bosshp : MonoBehaviour
{
    public Image hpred;
    public Image hpwihte;
    public Text hp;
    public BossAI boss;

    public int maxhp;
    public int nowhp;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nowhp = boss.hp;
        hpred.fillAmount = (float)nowhp / (float)maxhp;
        hpwihte.fillAmount = Mathf.Lerp(hpwihte.fillAmount, hpred.fillAmount, 0.3f);
        hp.text = (hpred.fillAmount * 100f).ToString()+'%';
    }
}
