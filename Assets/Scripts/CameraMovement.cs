using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterMovement player;
    Vector3 pos;

    private Vector3 bottomLeft;
    private Vector3 topRight;

    public EnemyManager enemyManager;
    public bool isEnemyInView = false;

    void Start()
    {
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
    
    }

    public Vector3 ClampPosition(Vector3 position) {

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        position.x = Mathf.Clamp(position.x, bottomLeft.x, topRight.x);
        position.y = Mathf.Clamp(position.y, bottomLeft.y, topRight.y);
        return position;

    }

    // Update is called once per frame
    void Update()
    {
        float count = 0;
        enemyManager.UpdateEnemyList();
        foreach(Enemy enemy in enemyManager.allEnemies) {
            if(Mathf.Abs(Camera.main.transform.position.x - enemy.transform.position.x) < 7.0f) {
                isEnemyInView = true;
                //Debug.Log(isEnemyInView);
                break;
            } else {
                count++;
            }
        }
        if(count == enemyManager.allEnemies.Length) {
            //Debug.Log("NO ENEMIES");
            isEnemyInView = false;
        }

        if(!isEnemyInView) {
            pos = transform.position;
            pos.x = player.transform.position.x;
            //pos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = pos;
        }

        if(Mathf.Abs(player.transform.position.y - transform.position.y) > 10.0f) {
            SceneManager.LoadScene("GameOver");
        }
    }
}
