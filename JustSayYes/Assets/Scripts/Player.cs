using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float walkSpeed;
    public LayerMask walkableLayer;
    public bool canMove = true;
    public bool inDialogue = false;

    public int morale = 100;
    public int money = 50;

    public bool hasPhone = false;
    public bool hasRunShoes = false;
    public bool hasHeadPhones = false;

    public Sprite spriteHappy;
    public Sprite spriteNeutral;
    public Sprite spriteSad;
    public SpriteRenderer srHead;

    CharacterController cc;
    Vector2 target;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(transform.position.x, transform.position.y);
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(morale > 70){
            srHead.sprite = spriteHappy;
        }else if(morale > 30){
            srHead.sprite = spriteNeutral;
        }else{
            srHead.sprite = spriteSad;
        }
        if (canMove)
        {
            MouseInput();
            Movement();
        }
        else
        {
            anim.SetBool("isWalking",false);
        }
    }

    void MouseInput(){
        if(Input.GetButton("Fire1")){
            if(Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.01f,walkableLayer) != null){
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                target = new Vector2(transform.position.x, transform.position.y);
            }
        }
    }

    void Movement(){
        Vector2 dir = (target-new Vector2(transform.position.x,transform.position.y));
        if(dir.x > 0){
            transform.localScale = new Vector3(-1,1,1);
        }else{
            transform.localScale = new Vector3(1,1,1);
        }
        if(dir.magnitude < 0.1){
            dir = Vector2.zero;
            target = new Vector2(transform.position.x, transform.position.y);
            anim.SetBool("isWalking",false);
        }else{
            anim.SetBool("isWalking",true);
        }
        dir.Normalize();
        cc.Move(dir*walkSpeed*Time.deltaTime);
    }
}
