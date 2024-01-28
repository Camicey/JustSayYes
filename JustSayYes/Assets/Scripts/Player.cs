using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float walkSpeed;
    public float recoverySpeed = 1f;
    public float runSpeed = 5f;
    public float runDuration = 20f;
    public float musicDuration = 15f;
    public LayerMask walkableLayer;
    public bool canMove = true;
    public bool inDialogue = false;
    public bool canBeTalkedTo = true;
    public bool isRunning = false;
    public bool inTransition = true;

    public int morale = 100;
    public int money = 50;

    public int moneyToPay =0;
    public int score = 0;

   

    public bool hasPhone = false;
    public bool hasRunShoes = false;
    public bool hasHeadphones = false;

    public bool hasMetFrenchGuy = false;

    public bool frenchIsGone = false;

    public bool hasMetSect = false;
    
    public bool hasBook = false;
    public bool hasBookToPay = false;


    public bool hasChocolate = false;
    public bool hasChocolateToPay = false;


    public bool hasFromage = false;


    public bool hasRun = false;

    public bool isGuilty = false;

    public int phoneBattery = 4;
    public int headphonesBattery = 3;

    public Button headphonesButton;
    public Button phoneButton;
    public Button runButton;
    public Sprite spriteHeadPhones;
    public Sprite spriteHappy;
    public Sprite spriteNeutral;
    public Sprite spriteSad;

    public Sprite[] batteryLevels;
    public Image batteryHeadPhonesLevel;
    public Image batteryPhoneLevel;

    public Image[] objectDisplay;
    public bool[] hasObject;
    public int[] objectValue;

    public Slider moralBar;
    public SpriteRenderer srHead;

    public CharacterController cc;
    public Vector2 target;
    Animator anim;
    DialogueSystem ds;
    AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(transform.position.x, transform.position.y);
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        ds = GameObject.Find("Dialogue").GetComponent<DialogueSystem>();
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(morale <= 0)SceneManager.LoadScene("PsyFin");
        morale = Mathf.Clamp(morale,0,100);
        moralBar.value = morale;
        runDuration += Time.deltaTime;
        musicDuration += Time.deltaTime;
        runButton.interactable = (runDuration > 20f);
        phoneButton.gameObject.SetActive(hasPhone);
        phoneButton.interactable = (inDialogue && phoneBattery > 0);
        headphonesButton.gameObject.SetActive(hasHeadphones);
        phoneButton.interactable = (headphonesBattery > 0 && musicDuration > 12f);
        runButton.gameObject.SetActive(hasRunShoes);

        if(phoneButton.interactable && Input.GetButtonDown("Phone"))PhoneCall();
        if(runButton.interactable && Input.GetButtonDown("Run"))StartRunning();
        if(headphonesButton.interactable && Input.GetButtonDown("Headphones"))PutOnHeadphones();

        batteryPhoneLevel.sprite = batteryLevels[phoneBattery];
        batteryHeadPhonesLevel.sprite = batteryLevels[headphonesBattery];

        for(int i = 0; i < hasObject.Length; i++){
            objectDisplay[i].gameObject.SetActive(hasObject[i]);
        }

        if(morale > 70){
            srHead.sprite = spriteHappy;
        }else if(morale > 30){
            srHead.sprite = spriteNeutral;
        }else{
            srHead.sprite = spriteSad;
        }
        if(musicDuration < 12f){
            canBeTalkedTo = true;
            music.volume = Mathf.Lerp(music.volume,0.5f,0.02f);

        }else{
            canBeTalkedTo = false;
            music.volume = Mathf.Lerp(music.volume,0f,0.02f);
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
            inTransition = false;
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
        }else {
            canBeTalkedTo = false;
            cc.Move(dir*walkSpeed*Time.deltaTime);
            isRunning = false;
        }
    }

    public void StartRunning(){
        runDuration = 0f;
        hasRun = true;
    }

    public void PhoneCall(){
        ds.EndDialogue();
        phoneBattery -= 1;
    }

    public void PutOnHeadphones(){
        musicDuration = 0f;
        headphonesBattery -= 1;
        morale += 24;
    }

    public void EnterTransition(){
        Vector2 dir = (target-new Vector2(transform.position.x,transform.position.y));
        dir.y = 0;
        target = target+dir*10f;
        inTransition = true;
        hasRun = false;
    }

    public void GiveObject(int id){
        score += objectValue[id];
        hasObject[id] = true;
    }
}
