using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireballshooter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fireballprefab;
    public GameObject target;
    public float lastime = 0f;
    public float cdtime = 3f;
    public float endtime = 0f;
    public bool canfire = true;
    public Vector3 direction;
    public int count = 0;
    public float radius;
    public float speed;
    void Start()
    {
        StartCoroutine(danmu(cdtime));
    }
    void Update()
    {


    }


    IEnumerator ModLinepro()
    {

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.4f);
            GameObject newone = Instantiate(fireballprefab);
            newone.transform.position = this.transform.position;
            newone.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        yield return new WaitForSeconds(0);

    }

    IEnumerator ModTriLinepro()
    {
        Vector3 dir = direction;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.4f);
            Quaternion leftRota = Quaternion.AngleAxis(-30, Vector3.forward);
            Quaternion RightRota = Quaternion.AngleAxis(30, Vector3.forward);
            GameObject newone = Instantiate(fireballprefab);
            newone.transform.position = this.transform.position;
            newone.GetComponent<Rigidbody2D>().velocity = dir * speed;
            newone = Instantiate(fireballprefab);
            newone.transform.position = this.transform.position;
            newone.GetComponent<Rigidbody2D>().velocity = leftRota * dir * speed;
            newone = Instantiate(fireballprefab);
            newone.transform.position = this.transform.position;
            newone.GetComponent<Rigidbody2D>().velocity = RightRota * dir * speed;
        }
        yield return new WaitForSeconds(0);
    }


    IEnumerator createcircle(float time)
    {
        float angle = 0;
        List<GameObject> fireball = new List<GameObject>();
        while (true)
        {
            float x = this.transform.position.x + radius * Mathf.Cos(angle * 3.14f / 180f);
            float y = this.transform.position.y + radius * Mathf.Sin(angle * 3.14f / 180f);
            GameObject newone = Instantiate(fireballprefab);
            fireball.Add(newone);
            newone.transform.position = new Vector3(x, y, this.transform.position.z);
            angle += 30;
            yield return new WaitForSeconds(time);
            if (angle >= 360)
            {
                yield return new WaitForSeconds(0.5f);
                foreach (GameObject item in fireball)
                {
                    if (item != null)
                        item.GetComponent<Rigidbody2D>().velocity = direction * speed;
                }
                break;
            }
        }
    }


    IEnumerator danmu(float cdtime)
    {
        while (true)
        {
            direction = Vector3.Normalize(new Vector3(GameObject.FindWithTag("Player").transform.position.x - transform.position.x, GameObject.FindWithTag("Player").transform.position.y - transform.position.y, 0f));

            int num = Random.Range(0, 3);
            if (num == 0)
            {
                StartCoroutine(ModLinepro());
            }
            if (num == 1)
            {
                StartCoroutine(ModTriLinepro());
            }
            if (num == 2)
            {
                StartCoroutine(createcircle(0.1f));
            }
            yield return new WaitForSeconds(cdtime);


        }
    }
}
