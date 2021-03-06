using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    private GameControllerScript gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = gameController.currentspawnPoint.position;

        }
        else
        {
            collision.gameObject.SetActive(false);
        }

    }
}
