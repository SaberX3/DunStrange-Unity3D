using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraControl instance;
    public CinemachineConfiner cv;
    public bool isbackdamping = false;
    public GameObject minimapsmallCamera;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (isbackdamping)
            instance.cv.m_Damping = Mathf.Lerp(instance.cv.m_Damping, 0f, 0.1f);
        if (instance.cv.m_Damping <= 0.0001)
        {
            instance.cv.m_Damping = 0;
            instance.isbackdamping = false;
        }
    }
    public void Changeroomnow(Transform newroom)
    {
        instance.cv.m_Damping = 1.1f;
        instance.cv.m_BoundingShape2D = newroom.GetComponent<PolygonCollider2D>();
    }
    public void ChangebackDamping()
    {
        instance.isbackdamping = true;
    }
    public void SetCameraPosition(Vector3 position)
    {
        minimapsmallCamera.transform.position = new Vector3(position.x, position.y, -10);
    }
}