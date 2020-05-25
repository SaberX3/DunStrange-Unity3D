using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up, down, left, right };
    public Direction direction;
    [Header("房间")]
    public GameObject roomprefab;
    public int roomnum;
    public Color startcolor, endcolor;
    private GameObject endroom;
    [Header("生成控制")]
    public Transform point;
    public float xoffset, yoffset;
    public LayerMask roomlayer;
    public List<Room> roomlist = new List<Room>();
    public WallType wallType;
    public GameObject minimapcamera;
    void Start()
    {
        for (int i = 0; i < roomnum; i++)
        {
            roomlist.Add(Instantiate(roomprefab, point.position, Quaternion.identity).GetComponent<Room>());
            Changepoint();
        }


        endroom = roomlist[0].gameObject;
        foreach (var room in roomlist)
        {
            if (room.transform.position.sqrMagnitude > endroom.transform.position.sqrMagnitude)
            {
                endroom = room.gameObject;
            }
        }
        Setendroom(endroom.GetComponent<Room>(), endroom.transform.position);


        for (int i = 0; i < roomlist.Count - 1; i++)
        {
            SetRoom(roomlist[i], roomlist[i].transform.position);
        }

        CreateEndroom(roomlist[roomlist.Count - 1], roomlist[roomlist.Count - 1].transform.position);
        minimapcamera.transform.position = new Vector3((roomlist[0].gameObject.transform.position.x + roomlist[roomlist.Count - 1].gameObject.transform.position.x) / 2,
                                                       (roomlist[0].gameObject.transform.position.y + roomlist[roomlist.Count - 1].gameObject.transform.position.y) / 2,
                                                       -10
        );

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Changepoint()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);
            switch (direction)
            {
                case Direction.up:
                    point.position += new Vector3(0, yoffset, 0);
                    break;

                case Direction.down:
                    point.position += new Vector3(0, -yoffset, 0);
                    break;

                case Direction.left:
                    point.position += new Vector3(-xoffset, 0, 0);
                    break;

                case Direction.right:
                    point.position += new Vector3(xoffset, 0, 0);
                    break;

            }
        } while (Physics2D.OverlapCircle(point.position, 0.2f, roomlayer));
    }
    public void SetRoom(Room newroom, Vector3 roomposition)
    {
        newroom.uhavedoor = Physics2D.OverlapCircle(roomposition + new Vector3(0, yoffset, 0), 0.2f, roomlayer);
        newroom.dhavedoor = Physics2D.OverlapCircle(roomposition + new Vector3(0, -yoffset, 0), 0.2f, roomlayer);
        newroom.lhavedoor = Physics2D.OverlapCircle(roomposition + new Vector3(-xoffset, 0, 0), 0.2f, roomlayer);
        newroom.rhavedoor = Physics2D.OverlapCircle(roomposition + new Vector3(xoffset, 0, 0), 0.2f, roomlayer);
        newroom.getdoornum();
        switch (newroom.doornum)
        {
            case 1:
                if (newroom.uhavedoor)
                {
                    Instantiate(wallType.singleup, roomposition, Quaternion.identity);
                }
                if (newroom.dhavedoor)
                {
                    Instantiate(wallType.singdown, roomposition, Quaternion.identity);
                }
                if (newroom.lhavedoor)
                {
                    Instantiate(wallType.singleLeft, roomposition, Quaternion.identity);
                }
                if (newroom.rhavedoor)
                {
                    Instantiate(wallType.singleright, roomposition, Quaternion.identity);
                }
                break;
            case 2:
                if (newroom.lhavedoor && newroom.uhavedoor)
                    Instantiate(wallType.doubleLU, roomposition, Quaternion.identity);
                if (newroom.lhavedoor && newroom.rhavedoor)
                    Instantiate(wallType.doubleLR, roomposition, Quaternion.identity);
                if (newroom.lhavedoor && newroom.dhavedoor)
                    Instantiate(wallType.doubleLD, roomposition, Quaternion.identity);
                if (newroom.uhavedoor && newroom.rhavedoor)
                    Instantiate(wallType.doubleRU, roomposition, Quaternion.identity);
                if (newroom.uhavedoor && newroom.dhavedoor)
                    Instantiate(wallType.doubleUD, roomposition, Quaternion.identity);
                if (newroom.rhavedoor && newroom.dhavedoor)
                    Instantiate(wallType.doubleRD, roomposition, Quaternion.identity);
                break;
            case 3:
                if (!newroom.uhavedoor)
                    Instantiate(wallType.tripleLRD, roomposition, Quaternion.identity);
                if (!newroom.dhavedoor)
                    Instantiate(wallType.tripleLRU, roomposition, Quaternion.identity);
                if (!newroom.lhavedoor)
                    Instantiate(wallType.tripleUDR, roomposition, Quaternion.identity);
                if (!newroom.rhavedoor)
                    Instantiate(wallType.tripleUDL, roomposition, Quaternion.identity);
                break;
            case 4:
                Instantiate(wallType.fourDoors, roomposition, Quaternion.identity);
                break;
        }
    }
    public void Setendroom(Room lastroom, Vector3 roomposition)
    {
        Vector3 endroomposition = new Vector3(0, 0, 0);
        if (!lastroom.uhavedoor)
        {
            if (gethaveroomnum((roomposition + new Vector3(0, yoffset, 0))) < 2)
            {
                roomlist.Add(Instantiate(roomprefab, roomposition + new Vector3(0, yoffset, 0), Quaternion.identity).GetComponent<Room>());
            }
        }
        if (!lastroom.dhavedoor)
        {
            if (gethaveroomnum((roomposition + new Vector3(0, -yoffset, 0))) < 2)
            {
                roomlist.Add(Instantiate(roomprefab, roomposition + new Vector3(0, -yoffset, 0), Quaternion.identity).GetComponent<Room>());
            }
        }
        if (!lastroom.lhavedoor)
        {
            if (gethaveroomnum((roomposition + new Vector3(-xoffset, 0, 0))) < 2)
            {
                roomlist.Add(Instantiate(roomprefab, roomposition + new Vector3(-xoffset, 0, 0), Quaternion.identity).GetComponent<Room>());
            }
        }
        if (!lastroom.rhavedoor)
        {
            if (gethaveroomnum((roomposition + new Vector3(xoffset, 0, 0))) < 2)
            {
                roomlist.Add(Instantiate(roomprefab, roomposition + new Vector3(xoffset, 0, 0), Quaternion.identity).GetComponent<Room>());
            }
        }
    }

    public void CreateEndroom(Room newroom, Vector3 roomposition)
    {
        newroom.uhavedoor = Physics2D.OverlapCircle(roomposition + new Vector3(0, yoffset, 0), 0.2f, roomlayer);
        newroom.dhavedoor = Physics2D.OverlapCircle(roomposition + new Vector3(0, -yoffset, 0), 0.2f, roomlayer);
        newroom.lhavedoor = Physics2D.OverlapCircle(roomposition + new Vector3(-xoffset, 0, 0), 0.2f, roomlayer);
        newroom.rhavedoor = Physics2D.OverlapCircle(roomposition + new Vector3(xoffset, 0, 0), 0.2f, roomlayer);
        if (newroom.uhavedoor)
        {
            Instantiate(wallType.EndroomU, roomposition, Quaternion.identity);
        }
        if (newroom.dhavedoor)
        {
            Instantiate(wallType.EndroomD, roomposition, Quaternion.identity);
        }
        if (newroom.lhavedoor)
        {
            Instantiate(wallType.EndroomL, roomposition, Quaternion.identity);
        }
        if (newroom.rhavedoor)
        {
            Instantiate(wallType.EndroomR, roomposition, Quaternion.identity);
        }
    }
    public int gethaveroomnum(Vector3 roomposition)
    {
        int num = 0;
        if (Physics2D.OverlapCircle(roomposition + new Vector3(0, yoffset, 0), 0.2f, roomlayer))
        {
            num++;
        }
        if (Physics2D.OverlapCircle(roomposition + new Vector3(0, -yoffset, 0), 0.2f, roomlayer))
        {
            num++;
        }
        if (Physics2D.OverlapCircle(roomposition + new Vector3(-xoffset, 0, 0), 0.2f, roomlayer))
        {
            num++;
        }
        if (Physics2D.OverlapCircle(roomposition + new Vector3(xoffset, 0, 0), 0.2f, roomlayer))
        {
            num++;
        }
        return num;
    }
}
[System.Serializable]
public class WallType
{
    public GameObject singleLeft, singleright, singleup, singdown,
                      doubleLR, doubleLU, doubleLD, doubleRU, doubleRD, doubleUD,
                      tripleLRU, tripleLRD, tripleUDL, tripleUDR,
                      fourDoors,
                      EndroomL, EndroomR, EndroomU, EndroomD;
}
