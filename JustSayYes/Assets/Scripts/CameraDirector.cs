using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{
    GameObject player;
    Camera cam;
    public float maxDistance = 5f;
    public float regularZoomLevel = 3f;
    public float dialogueZoomLevel = 1f;
    public bool inDialogue = false;

    public Vector3 target;
    float zoomLevel;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inDialogue){
            Vector3 dir = (target - transform.position);
            dir.z = 0;
            transform.position += dir*Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,dialogueZoomLevel,0.1f);
        }else{
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,regularZoomLevel,0.1f);
        }
        if(Mathf.Abs(transform.position.x - player.transform.position.x) > maxDistance){
            Vector3 dir = (player.transform.position - transform.position);
            dir.y = -transform.position.y;
            dir.z = 0;
            transform.position += dir*Time.deltaTime;
        }
    }
}
