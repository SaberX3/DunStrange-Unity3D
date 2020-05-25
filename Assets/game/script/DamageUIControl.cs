using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUIControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void End()
    {
        Destroy(transform.gameObject);
    }

    public void SetText(int damage)
    {
        text.text = damage.ToString();
    }
}
