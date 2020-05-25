using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    public static gameover instance;
    public Text bannertext;
    public Text levelup;
    public Text goon;
    public Image levelget;
    public Image levelhave;
    public int levelpoint;
    public int pointget;
    public bool islevelup = false;
    public bool islevelupx2 = false;
    public bool isgameover = false;
    public bool iscountine = false;
    public bool iswin = false;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        levelhave.fillAmount = ((float)levelpoint) / 10f;
        StartCoroutine(countine());
    }

    // Update is called once per frame
    void Update()
    {
        if (iscountine)
        {
            goon.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!iswin)
                    SceneManager.LoadScene(6);
                if (iswin)
                    SceneManager.LoadScene(0);
            }
        }
        if (isgameover)
        {
            if (islevelupx2)
            {
                levelget.fillAmount = Mathf.Lerp(levelget.fillAmount, 1f, 0.1f);
                if (levelget.fillAmount - 1 >= -0.01)
                {
                    islevelupx2 = false;
                    islevelup = true;
                    pointget = pointget + levelpoint - 10;
                    levelpoint = 0;
                    levelget.fillAmount = ((float)levelpoint) / 10f;
                    levelhave.fillAmount = 0;
                }
            }
            if (islevelup)
            {
                levelget.fillAmount = Mathf.Lerp(levelget.fillAmount, 1f, 0.1f);
                if (levelget.fillAmount - 1 >= -0.01)
                {
                    islevelup = false;
                    pointget = pointget + levelpoint - 10;
                    levelpoint = 0;
                    levelget.fillAmount = ((float)levelpoint) / 10f;
                    levelhave.fillAmount = 0;
                }
            }
            if (!islevelup)
            {
                levelhave.fillAmount = ((float)levelpoint) / 10f;
                levelget.fillAmount = Mathf.Lerp(levelget.fillAmount, ((float)(levelpoint + pointget)) / 10f, 0.1f);
            }
        }
    }
    public static void Badend()
    {
        instance.levelpoint = PlayerManager.instance.levelpoint;
        if (GameMaster.instance)
            instance.pointget = GameMaster.instance.clearroomnum;
        else
            instance.pointget = 15;
        instance.bannertext.text = "Mission Failed";
        PlayerManager.Gameover(instance.pointget);
        InventoryManager.cleanbag();
        instance.isgameover = true;
        instance.levelget.fillAmount = ((float)instance.levelpoint) / 10f;
        if (instance.levelpoint + instance.pointget > 10 && instance.levelpoint + instance.pointget <= 20)
        {
            instance.islevelup = true;
            instance.levelup.gameObject.SetActive(true);
        }
        if (instance.levelpoint + instance.pointget > 20)
        {
            instance.islevelup = false;
            instance.islevelupx2 = true;
            instance.levelup.gameObject.SetActive(true);
        }

    }

    public static void happyend()
    {
        instance.levelpoint = PlayerManager.instance.levelpoint;
        instance.pointget = 18;
        instance.bannertext.text = "Mission Success";
        instance.iswin = true;
        PlayerManager.Gameover(instance.pointget);
        InventoryManager.cleanbag();
        instance.isgameover = true;
        if (instance.levelpoint + instance.pointget > 10 && instance.levelpoint + instance.pointget <= 20)
        {
            instance.islevelup = true;
            instance.levelup.gameObject.SetActive(true);
        }
        if (instance.levelpoint + instance.pointget > 20)
        {
            instance.islevelup = false;
            instance.islevelupx2 = true;
            instance.levelup.gameObject.SetActive(true);
        }
    }

    IEnumerator countine()
    {
        yield return new WaitForSeconds(6f);
        instance.iscountine = true;
    }
}
