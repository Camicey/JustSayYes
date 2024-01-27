using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float walkSpeed;
    public LayerMask walkableLayer;
    public bool canMove = true;
    public bool inDialogue = false;

    CharacterController cc;
    Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(transform.position.x, transform.position.y);
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MouseInput();
            Movement();
        }
<<<<<<< HEAD
    }

    void MouseInput(){
        if(Input.GetButton("Fire1")){
            if(Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.01f,walkableLayer) != null){
=======



    }

    void MouseInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.01f, walkableLayer) != null)
            {
>>>>>>> 005e312080727930eee9abc521fe765ebfd12561
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                target = new Vector2(transform.position.x, transform.position.y);
            }
        }
    }

<<<<<<< HEAD
    void Movement(){
        Vector2 dir = (target-new Vector2(transform.position.x,transform.position.y));
        if(dir.magnitude < 0.1){
=======
    void Movement()
    {
        Vector2 dir = (target - new Vector2(transform.position.x, transform.position.y));

        if (dir.magnitude < 0.1)
        {
>>>>>>> 005e312080727930eee9abc521fe765ebfd12561
            dir = Vector2.zero;
            target = new Vector2(transform.position.x, transform.position.y);
        }
        dir.Normalize();
<<<<<<< HEAD
        cc.Move(dir*walkSpeed*Time.deltaTime);
=======
        cc.Move(dir * walkSpeed * Time.deltaTime);
>>>>>>> 005e312080727930eee9abc521fe765ebfd12561
    }
}
