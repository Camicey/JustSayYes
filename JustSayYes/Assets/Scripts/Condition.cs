using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
    Player player;
    CameraDirector cam;

    public GameObject target;

    public bool hasMetFrenchy = false;
    public bool hasMetSectare = false;
    public bool hasPhone = false;

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
            ((hasMetSectare && player.hasMetSect) || (!hasMetSectare)) &&
            ((hasPhone && player.hasPhone) || (!hasPhone)) &&
            (cam.timeOfDay/cam.timeEndOfDay > timeArrival && cam.timeOfDay/cam.timeEndOfDay < timeDeparture)
        ){
            target.SetActive(true);
        }else{
            target.SetActive(false);
        }
    }
}
