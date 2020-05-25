using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatiocontrol : MonoBehaviour
{
    public Animator playeranime;
    private Movecontrol move;
    private Collision coll;
    [HideInInspector]
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        playeranime = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movecontrol>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        playeranime.SetBool("onGround", coll.onGround);
        playeranime.SetBool("onWall", coll.onWall);
        playeranime.SetBool("onRightWall", coll.onRightWall);
        playeranime.SetBool("wallGrab", move.iswallGrab);
        playeranime.SetBool("wallSlide", move.iswallSlide);
        playeranime.SetBool("isdash", move.isdash);
        playeranime.SetBool("ishurt", move.ishurt);
        playeranime.SetBool("isattack", move.isattack);
        playeranime.SetBool("isairjump", move.isairjump);
    }
    // public void SetHorizontalMovement(float x, float y, float yVel)
    // {
    //     playeranime.SetFloat("Horizontal", x);
    //     playeranime.SetFloat("Vertical", y);
    //     playeranime.SetFloat("VerticalVelocity", yVel);
    // }
}
