using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{
    GameObject player;
    public float maxDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) > maxDistance){
            Vector3 dir = (player.transform.position - transform.position);
            dir.y = 0;
            dir.z = 0;
            transform.position += dir*Time.deltaTime;
        }
    }
}
