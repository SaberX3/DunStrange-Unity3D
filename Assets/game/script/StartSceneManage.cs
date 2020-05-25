using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManage : MonoBehaviour
{
    public Toggle save1;
    public Toggle save2;
    public Toggle save3;

    public Savelist savelist;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void starting()
    {
        savelist.List[0].isselected = save1.isOn;
        savelist.List[0].isselected = save1.isOn;
        savelist.List[0].isselected = save1.isOn;
        SceneManager.LoadScene(1);
    }
}
