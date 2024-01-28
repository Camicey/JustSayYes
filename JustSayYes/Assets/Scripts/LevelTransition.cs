using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public GameObject nextScene;
    public GameObject currentScene;
    public Vector2 nextPlayerPosition;
    public Vector2 nextPlayerDir;
    public bool westSided = true;

    bool transitionTriggered = false;
    float transitionDelay = 1.0f;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (((player.transform.position.x > transform.position.x && westSided) || (player.transform.position.x < transform.position.x && !westSided)) && !transitionTriggered)
        {
            Debug.Log("Transit");
            player.EnterTransition();
            transitionDelay = 1.0f;
            transitionTriggered = true;
        }
        if (transitionTriggered)
        {
            transitionDelay -= Time.deltaTime;
            if (transitionDelay < 0f)
            {
                LevelTransit();
                transitionDelay = 1.0f;
                transitionTriggered = false;
            }
        }
    }

    void LevelTransit()
    {
        player.cc.enabled = false;
        player.gameObject.transform.position = new Vector3(nextPlayerPosition.x, nextPlayerPosition.y, player.transform.position.z);
        player.cc.enabled = true;
        player.target = nextPlayerPosition + nextPlayerDir;
        nextScene.SetActive(true);
        currentScene.SetActive(false);
    }
}
