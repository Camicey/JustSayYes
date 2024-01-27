using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum behaviorType // your custom enumeration
{
    Follower, 
    Indifferent
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
    
    public behaviorType behavior = behaviorType.Indifferent;
    public float walkSpeed = 2.5f;
    public DialogueChunk[] dialogue;

    CharacterController cc;
    GameObject player;
    Vector2 target;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(behavior == behaviorType.Follower)FollowerBehavior();
        Movement();
    }

    void Movement(){
        Vector2 dir = (target-new Vector2(transform.position.x,transform.position.y));
        dir.Normalize();
        if(dir.magnitude < 0.1){
            dir = Vector2.zero;
            target = new Vector2(transform.position.x,transform.position.y);
        }

        cc.Move(dir*walkSpeed*Time.deltaTime);
    }

    void FollowerBehavior(){
        if(new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y).magnitude < 3){
            target = new Vector2(player.transform.position.x,player.transform.position.y);
        }
        else
        {
            target = new Vector2(transform.position.x,transform.position.y);
        }
    }

    void ApplyConsequence(int type){
        switch(type){
            case 1:
                print("money-- :(");
                break;
        }

    }

    public void PlayerAnswer(){
        return;
    }
}
