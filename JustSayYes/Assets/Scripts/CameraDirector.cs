using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDirector : MonoBehaviour
{
    GameObject player;
    Camera cam;
    public float maxDistance = 5f;
    public float regularZoomLevel = 3f;
    public float dialogueZoomLevel = 1f;
    public bool inDialogue = false;
    public Gradient skyColor;
    public Image darknessOverlay;
    public float timeOfDay = 0f;
    public float timeEndOfDay = 480f;
    public float limitDistance = 15f;

    public Vector3 target;
    float zoomLevel;
    float timeBeforeDepression = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-limitDistance,limitDistance),transform.position.y,transform.position.z);
        cam.backgroundColor = skyColor.Evaluate(timeOfDay/timeEndOfDay);
        Color darknessOverlayColor = skyColor.Evaluate(timeOfDay/timeEndOfDay);
        darknessOverlayColor.a = timeOfDay/timeEndOfDay-0.5f;
        darknessOverlay.color = darknessOverlayColor;
        if(timeOfDay-timeEndOfDay > 0.0f){
            timeBeforeDepression -= Time.deltaTime;
            if(timeBeforeDepression <= 0.0f)
            {
            timeBeforeDepression = 0.6f;
            player.GetComponent<Player>().morale -= 1;
            }

        }
        if(inDialogue){
            Vector3 dir = (target - transform.position);
            dir.z = 0;
            transform.position += dir*Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,dialogueZoomLevel,0.01f);
        }else{
            timeOfDay += Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,regularZoomLevel,0.01f);
        }
        if(Mathf.Abs(transform.position.x - player.transform.position.x) > maxDistance){
            Vector3 dir = (player.transform.position - transform.position);
            dir.y = -transform.position.y;
            dir.z = 0;
            transform.position += dir*Time.deltaTime;

        }
    }
}
