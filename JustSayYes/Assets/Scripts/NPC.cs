using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum behaviorType // your custom enumeration
{
    Follower, 
    Indifferent,
    RollerKid,
    Talkable
};


[System.Serializable]
public class DialogueResponse
{
    public string text;
    public int next;
    public int consequence = 0;
}

[System.Serializable]
public class DialogueChunk
{
    public string text;
    public DialogueResponse[] responses;
    public int consequence = 0;
}

public class NPC : MonoBehaviour
{
    public string name;
    public behaviorType behavior = behaviorType.Indifferent;
    public float walkSpeed = 2.5f;
    public DialogueChunk[] dialogue;
    public bool inDialogue = false;

    DialogueSystem ds;
    CharacterController cc;
    GameObject player;
    Vector2 target;
    Vector2 startingPoint;
    CameraDirector cam;

    float lastTimeTalked = 999999f;
    
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(transform.position.x,transform.position.y);
        player = GameObject.Find("Player");
        cc = GetComponent<CharacterController>();
        ds = GameObject.Find("Dialogue").GetComponent<DialogueSystem>();
        startingPoint = new Vector2(player.transform.position.x,player.transform.position.y);
        cam = GameObject.Find("Main Camera").GetComponent<CameraDirector>();
    }

    // Update is called once per frame
    void Update()
    {

        if(behavior == behaviorType.Indifferent)target = new Vector2(transform.position.x,transform.position.y);
        if(behavior == behaviorType.Follower)FollowerBehavior();
        if(behavior == behaviorType.RollerKid)RollerKidBehavior();
        if(behavior == behaviorType.Talkable)TalkableBehavior();
        if(!inDialogue){
            lastTimeTalked += Time.deltaTime;
            Movement();
        }else{
            lastTimeTalked = 0;
        }
    }

    void Movement(){
        Vector2 dir = (target-new Vector2(transform.position.x,transform.position.y));
         if(dir.x > 0){
            transform.localScale = new Vector3(-1,1,1);
        }else{
            transform.localScale = new Vector3(1,1,1);
        }
        dir.Normalize();
        if(dir.magnitude < 0.1){
            dir = Vector2.zero;
            target = new Vector2(transform.position.x,transform.position.y);
        }

        cc.Move(dir*walkSpeed*Time.deltaTime);
    }

    void FollowerBehavior(){
        if(new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y).magnitude < 3){
            if(new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y).magnitude < 1 && !player.GetComponent<Player>().inDialogue && !player.GetComponent<Player>().canBeTalkedTo){
                ds.StartDialogue(gameObject.GetComponent<NPC>());
            }
            target = new Vector2(player.transform.position.x,player.transform.position.y);
        }
        else
        {
            target = new Vector2(transform.position.x,transform.position.y);
        }
    }

    void RollerKidBehavior(){
        if(new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y).magnitude < 1 && !player.GetComponent<Player>().inDialogue && lastTimeTalked > 10f && !player.GetComponent<Player>().canBeTalkedTo){
                ds.StartDialogue(gameObject.GetComponent<NPC>());
            }
        Vector2 dir = (target-new Vector2(transform.position.x,transform.position.y));
        if(dir.magnitude < 0.1){
            target = startingPoint + Vector2.right * Random.Range(-10f,10f) + Vector2.up* Random.Range(-1f,1f);
        }

    }

    void TalkableBehavior(){
        if(new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y).magnitude < 1 && !player.GetComponent<Player>().inDialogue && lastTimeTalked > 5f){
            ds.StartDialogue(gameObject.GetComponent<NPC>());
            }
    }

    public void DoneTalking(){
        if(behavior == behaviorType.Follower)behavior = behaviorType.Indifferent;
        lastTimeTalked = 0f;
    }

}
