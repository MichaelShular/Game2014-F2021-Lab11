using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTriggerController : MonoBehaviour
{
    public Transform spawnpoint;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.SetCurrentSpaenPoint(spawnpoint);
        }
    }
}
