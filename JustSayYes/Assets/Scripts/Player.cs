using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float walkSpeed;
    public float recoverySpeed = 1f;
    public float runSpeed = 5f;
    public float runDuration = 5f;
    public LayerMask walkableLayer;
    public bool canMove = true;
    public bool inDialogue = false;
    public bool canBeTalkedTo = true;
    public bool isRunning = false;

    public int morale = 100;
    public int money = 50;

   

    public bool hasPhone = false;
    public bool hasRunShoes = true;
    public bool hasHeadphones = false;
    public bool hasFromage = false;

    public Button headphonesButton;
    public Button phoneButton;
    public Button runButton;
    public Sprite spriteHappy;
    public Sprite spriteNeutral;
    public Sprite spriteSad;
    public Slider moralBar;
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
        moralBar.value = morale;
        runDuration += Time.deltaTime;
        runButton.interactable = (runDuration > 20f);
        phoneButton.gameObject.SetActive(hasPhone);
        headphonesButton.gameObject.SetActive(hasHeadphones);
        runButton.gameObject.SetActive(hasRunShoes);
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
                if(Input.mousePosition.y/Screen.height < 0.9f)target = new Vector2(transform.position.x, transform.position.y);
                //Debug.Log(Input.mousePosition.y/Screen.height);
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
        if(runDuration < 2.0f){
            canBeTalkedTo = true;
            dir.y = 0;
            if(!isRunning)target = target+dir*10f;
            cc.Move(dir*runSpeed*Time.deltaTime);
            isRunning = true;
        }else if(runDuration < 5.0f){
            canBeTalkedTo = false;
            cc.Move(dir*recoverySpeed*Time.deltaTime);
            isRunning = false;
        }else {
            canBeTalkedTo = false;
            cc.Move(dir*walkSpeed*Time.deltaTime);
            isRunning = false;
        }
    }

    public void StartRunning(){
        runDuration = 0f;
    }
}
