using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public Transform cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cameraPos.position.x*scrollSpeed,cameraPos.position.y*scrollSpeed,0f);
    }
}
