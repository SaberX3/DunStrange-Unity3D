using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;
    private Color color;

    [Header("时间")]
    public float activeTime;
    public float activeStart;

    [Header("不透明度")]
    private float alpha;
    public float alphaPro;

    public float alphaParam; //透明度因数

    void Start()
    {

    }
    void Update()
    {
        alpha *= alphaParam;
        color = new Color(1, 1, 1, alpha);
        thisSprite.color = color;
        if (Time.time >= activeStart + activeTime)
        {
            //返回对象池
            ShadowControl.instance.ReturnPool(this.gameObject);
        }
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        alpha = 0.8f;
        thisSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;
        activeStart = Time.time;
    }
}
