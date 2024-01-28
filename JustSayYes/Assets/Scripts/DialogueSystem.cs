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
        cam.timeOfDay += 2f;
        ApplyConsequence(currentNPC.dialogue[chunk].consequence);
        choiceButtonsText[0].gameObject.SetActive(false);
        choiceButtonsText[1].gameObject.SetActive(false);
        choiceButtonsText[2].gameObject.SetActive(false);
        choiceButtonsText[3].gameObject.SetActive(false);
        choiceButtonsText[0].gameObject.transform.parent.gameObject.SetActive(false);
        choiceButtonsText[1].gameObject.transform.parent.gameObject.SetActive(false);
        choiceButtonsText[2].gameObject.transform.parent.gameObject.SetActive(false);
        choiceButtonsText[3].gameObject.transform.parent.gameObject.SetActive(false);
        text.text = currentNPC.dialogue[chunk].text;
        nameText.text = currentNPC.name;
        int responseAmount = currentNPC.dialogue[chunk].responses.Length;
        for(int i = 0; i < responseAmount; i++){
            choiceButtonsText[i].gameObject.SetActive(true);
            choiceButtonsText[i].gameObject.transform.parent.gameObject.SetActive(true);
            choiceButtonsText[i].text = currentNPC.dialogue[chunk].responses[i].text;
        }

    }

    public void EndDialogue(){
        player.GetComponent<Player>().canMove = true;
        player.GetComponent<Player>().inDialogue = false;
        cam.inDialogue = false;
        currentNPC.inDialogue = false;
        currentNPC.DoneTalking();
        dialogueUI.SetActive(false);
    }

    public void PlayerResponse(int id){
        int previousChunk = chunk;
        chunk = currentNPC.dialogue[chunk].responses[id].next;
        ApplyConsequence(currentNPC.dialogue[previousChunk].responses[id].consequence);
        ShowDialogue();
    }

    void ApplyConsequence(int type){
        switch(type){
            case -1:
            EndDialogue();
            break;
            case 1: //Minor inconvenience
                player.GetComponent<Player>().morale -= 5;
                break;
            case 2: //Major inconvenience
                player.GetComponent<Player>().morale -= 40;
                break;
            case 3: //Pet Dog
                player.GetComponent<Player>().morale += 5;
                cam.timeOfDay += 4f;
                break;
            case 4: //Pet Dog Bite
                player.GetComponent<Player>().morale -= 20;
                break;
            case 5 : // get shoes
                player.GetComponent<Player>().hasRunShoes= true;
                break;
            case 6 :// activates "hasfromage" 
                if (player.GetComponent<Player>().moneyToPay+10>player.GetComponent<Player>().money)
                {
                    chunk=3;
                }
                else 
                {
                    player.GetComponent<Player>().moneyToPay+=10;
                    player.GetComponent<Player>().hasFromage= true; 
                }
                break;
            case 7 : // moral up
                player.GetComponent<Player>().morale += 40; // to modify 
                break;
             case 8 : // -10 moni trump
                player.GetComponent<Player>().money -= 10;
                break;
             case 9 : // -15 moni trump, moral down 
                player.GetComponent<Player>().money -= 15;
                player.GetComponent<Player>().morale -= 20; 
                break;
             case 10 : // check the esperantist quest, if case 11 is useless, this case is useless as well
                if(player.GetComponent<Player>().frenchIsGone == true)
                {
                    player.GetComponent<Player>().hasHeadphones= true;
                    player.GetComponent<Player>().morale += 40; 
                }
                else 
                    player.GetComponent<Player>().morale -=10;
                break;
             case 11: //useless? 
                player.GetComponent<Player>().hasMetFrenchGuy=true;
                break;
             case 12 : // activates "has met the esperantist"
                player.GetComponent<Player>().hasMetSect= true; 
                break; 
             case 13 : // french goes and see the esperantist
                player.GetComponent<Player>().frenchIsGone= true ;
                Destroy(currentNPC.gameObject);
                EndDialogue();
                // more french dialogue? 
                break;
             case 14 : // -10 moni for the ticket
                player.GetComponent<Player>().money -= -10;
                player.GetComponent<Player>().hasTicket = true ;
                break;
             case 15 : // cashier payment 
                if (player.GetComponent<Player>().hasBookToPay == true)
                {
                    if(player.GetComponent<Player>().hasFromage == false)
                    {
                        chunk  = 1;
                        player.GetComponent<Player>().money -=30;
                        player.GetComponent<Player>().moneyToPay=0;
                        player.GetComponent<Player>().hasBookToPay = false;
                    }
                    else 
                    {
                        chunk = 5;
                        player.GetComponent<Player>().money -= 40;
                        player.GetComponent<Player>().moneyToPay=0;
                        player.GetComponent<Player>().hasBookToPay = false; 
                    }
                }
                else if (player.GetComponent<Player>().hasChocolateToPay == true)
                {
                    if(player.GetComponent<Player>().hasFromage == false)
                    {
                        chunk  = 3;
                        player.GetComponent<Player>().money -=25; 
                        player.GetComponent<Player>().moneyToPay=0;
                        player.GetComponent<Player>().hasChocolateToPay =false; 
                    }
                    else 
                    {
                        chunk = 4;
                        player.GetComponent<Player>().money -= 35;
                        player.GetComponent<Player>().moneyToPay=0;
                        player.GetComponent<Player>().hasChocolateToPay =false;
                    }
                }
                else if (player.GetComponent<Player>().hasFromage == true)
                    {
                        chunk =6;
                        player.GetComponent<Player>().money -= 10;
                        player.GetComponent<Player>().moneyToPay=0;
                    }
                else 
                    chunk = 7;
                break;
            case 16 : // can we take a book? 
                if (player.GetComponent<Player>().moneyToPay+30>player.GetComponent<Player>().money)
                {
                    chunk=1;
                }
                else 
                {
                    player.GetComponent<Player>().moneyToPay+=30;
                    player.GetComponent<Player>().hasBook = true;
                    player.GetComponent<Player>().hasBookToPay = true;
                    Destroy(currentNPC.gameObject);
                    EndDialogue();
                }

                break;
            case 17 : // can we take a chocolate? 
                if (player.GetComponent<Player>().moneyToPay+25>player.GetComponent<Player>().money)
                {
                    chunk=1;
                }
                else 
                {
                    player.GetComponent<Player>().moneyToPay+=25;
                    player.GetComponent<Player>().hasChocolate = true;
                    player.GetComponent<Player>().hasChocolateToPay = true;
                    Destroy(currentNPC.gameObject);
                    EndDialogue();
                }
                break;
           
        }

    }
}
