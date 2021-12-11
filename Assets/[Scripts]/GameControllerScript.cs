using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public Transform player;
    public Transform currentspawnPoint;

    // Start is called before the first frame update
    void Start()
    {
         player.position = currentspawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentSpaenPoint(Transform newSpawn)
    {
        currentspawnPoint = newSpawn;
    }
}
