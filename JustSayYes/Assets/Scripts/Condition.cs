using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
    Player player;
    CameraDirector cam;

    public GameObject target;

    public bool hasMetFrenchy = false;
    public bool hasNotMetFrenchy = false;
    public bool hasMetSectare = false;
    public bool hasNotMetSectare = false;
    public bool hasPhone = false;
    public bool hasNotPhone = false;
    public bool hasPlayerRun = false;
    public bool hasBestGift = false;
    public bool hasTrainTicket = false;

    public float timeArrival = 0f;
    public float timeDeparture = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(
            ((hasMetFrenchy && player.hasMetFrenchGuy) || (!hasMetFrenchy)) &&
            ((hasNotMetFrenchy && !player.hasMetFrenchGuy) || (!hasNotMetFrenchy)) &&
            ((hasMetSectare && player.hasMetSect) || (!hasMetSectare)) &&
            ((hasNotMetSectare && !player.hasMetSect) || (!hasNotMetSectare)) &&
            ((hasPhone && player.hasPhone) || (!hasPhone)) &&
            ((hasNotPhone && !player.hasPhone) || (!hasNotPhone)) &&
            ((hasPlayerRun && player.hasRun) || (!hasPlayerRun)) &&
            (cam.timeOfDay/cam.timeEndOfDay > timeArrival && cam.timeOfDay/cam.timeEndOfDay < timeDeparture)
        ){
            target.SetActive(true);
        }else{
            target.SetActive(false);
        }
    }
}
