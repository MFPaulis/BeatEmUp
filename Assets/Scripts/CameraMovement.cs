using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement player;
    Vector3 pos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        pos.x = player.transform.position.x;
        //pos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = pos;
    }
}
