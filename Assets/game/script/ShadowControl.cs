using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowControl : MonoBehaviour
{
    public static ShadowControl instance;

    public GameObject shadowprefab;
    public int shadowNum;
    public Queue<GameObject> usableprojects = new Queue<GameObject>();
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        //初始化单例
    }
    public void FillPool()
    {
        for (int i = 0; i < shadowNum; i++)
        {
            var newshadow = Instantiate(shadowprefab);
            newshadow.transform.SetParent(transform);
            ReturnPool(newshadow);
            //返回对象池
        }
    }

    //返回对象池的方法
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        usableprojects.Enqueue(gameObject);

    }

    public GameObject GetFromPool()
    {
        if (usableprojects.Count == 0)
        {
            FillPool();

        }
        var activeShadow = usableprojects.Dequeue();
        activeShadow.SetActive(true);
        return activeShadow;
    }
}
