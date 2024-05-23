using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterMovement player;
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

        if(Mathf.Abs(player.transform.position.y - transform.position.y) > 10.0f) {
            SceneManager.LoadScene("GameOver");
        }
    }
}
