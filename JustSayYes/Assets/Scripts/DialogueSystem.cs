using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public NPC currentNPC;
    public bool inDialogue = false;

    public int chunk = 0;
    public GameObject dialogueUI;
    public Text nameText;
    public Text text;
    public Text[] choiceButtonsText;

    GameObject player;
    CameraDirector cam;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera").GetComponent<CameraDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(NPC talker){
        if(!inDialogue){
            currentNPC = talker;
            player.GetComponent<Player>().canMove = false;
            player.GetComponent<Player>().inDialogue = true;
            cam.inDialogue = true;
            cam.target = talker.gameObject.transform.position;
            currentNPC.inDialogue = true;
            chunk = 0;
            dialogueUI.SetActive(true);
            ShowDialogue();
        }
    }

    void ShowDialogue(){
        cam.timeOfDay += 5f;
        ApplyConsequence(currentNPC.dialogue[chunk].consequence);
        choiceButtonsText[0].gameObject.SetActive(false);
        choiceButtonsText[1].gameObject.SetActive(false);
        choiceButtonsText[2].gameObject.SetActive(false);
        choiceButtonsText[3].gameObject.SetActive(false);
        text.text = currentNPC.dialogue[chunk].text;
        nameText.text = currentNPC.name;
        int responseAmount = currentNPC.dialogue[chunk].responses.Length;
        for(int i = 0; i < responseAmount; i++){
            choiceButtonsText[i].gameObject.SetActive(true);
            choiceButtonsText[i].text = currentNPC.dialogue[chunk].responses[i].text;
        }

    }

    void EndDialogue(){
        player.GetComponent<Player>().canMove = true;
        player.GetComponent<Player>().inDialogue = false;
        cam.inDialogue = false;
        currentNPC.inDialogue = false;
        currentNPC.DoneTalking();
        dialogueUI.SetActive(false);
    }

    public void PlayerResponse(int id){
        ApplyConsequence(currentNPC.dialogue[chunk].responses[id].consequence);
        chunk = currentNPC.dialogue[chunk].responses[id].next;
        ShowDialogue();
    }

    void ApplyConsequence(int type){
        switch(type){
            case -1:
            EndDialogue();
            break;
            case 1: //Minor inconvenience
                player.GetComponent<Player>().morale -= 10;
                break;
            case 2: //Major inconvenience
                player.GetComponent<Player>().morale -= 40;
                break;
        }

    }
}
