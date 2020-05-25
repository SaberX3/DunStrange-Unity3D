using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorleft, doorright, doorup, doordown;
    public bool lhavedoor, rhavedoor, uhavedoor, dhavedoor;
    public int doornum = 0;
    public GameObject alldoor;
    public float xoffset;
    public float yoffset;
    public LayerMask roomlayer;

    // Start is called before the first frame update
    void Start()
    {
        doorup.SetActive(uhavedoor);
        doordown.SetActive(dhavedoor);
        doorleft.SetActive(lhavedoor);
        doorright.SetActive(rhavedoor);
    }
    void Update()
    {
        alldoor.SetActive(GameMaster.instance.isbatter);
    }
    public void getdoornum()
    {
        if (uhavedoor)
        {
            doornum++;
        }
        if (dhavedoor)
        {
            doornum++;
        }
        if (lhavedoor)
        {
            doornum++;
        }
        if (rhavedoor)
        {
            doornum++;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraControl.instance.Changeroomnow(transform);
            CameraControl.instance.SetCameraPosition(transform.position);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            //小地图设置
            transform.GetComponentInChildren<MinimapControl>().changenew(true);
            transform.GetComponentInChildren<MinimapControl>().changenow(true);
            transform.GetComponentInChildren<MinimapControl>().changeold(true);

            if (uhavedoor)
            {
                GameObject newobj = Physics2D.OverlapCircle(transform.position + new Vector3(0, yoffset, 0), 0.2f, roomlayer).gameObject;
                newobj.GetComponentInChildren<MinimapControl>().changenow(false);
                newobj.GetComponentInChildren<MinimapControl>().changenew(true);
            }
            if (dhavedoor)
            {
                GameObject newobj = Physics2D.OverlapCircle(transform.position + new Vector3(0, -yoffset, 0), 0.2f, roomlayer).gameObject;
                newobj.GetComponentInChildren<MinimapControl>().changenow(false);
                newobj.GetComponentInChildren<MinimapControl>().changenew(true);
            }
            if (lhavedoor)
            {
                GameObject newobj = Physics2D.OverlapCircle(transform.position + new Vector3(-xoffset, 0, 0), 0.2f, roomlayer).gameObject;
                newobj.GetComponentInChildren<MinimapControl>().changenow(false);
                newobj.GetComponentInChildren<MinimapControl>().changenew(true);
            }
            if (rhavedoor)
            {
                GameObject newobj = Physics2D.OverlapCircle(transform.position + new Vector3(xoffset, 0, 0), 0.2f, roomlayer).gameObject;
                newobj.GetComponentInChildren<MinimapControl>().changenow(false);
                newobj.GetComponentInChildren<MinimapControl>().changenew(true);
            }

        }
    }
}