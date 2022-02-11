using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private float startPos;
    private GameObject cam;
    [SerializeField] private float parallaxEffect;

    void Start ()
    {
        cam = GameObject.Find("CM vcam1");
        startPos = transform.position.x;
    }
    
    void Update ()
    {
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
