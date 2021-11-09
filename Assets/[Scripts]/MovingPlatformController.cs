using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public MovingPlatformDirection direction;
    [Range(0.1f, 10.0f)]
    public float speed;
    [Range(1, 20)]
    public float distance;
    public bool isLooping;
    [Range(0.05f, 0.1f)]
    public float distanceOffset;
    private bool isMovingActive;

    private Vector2 startingPos;


    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        isMovingActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();

        if (isLooping)
        {
            isMovingActive = true;
        }
    }

    private void MovePlatform()
    {
        float pingPongValue = (isMovingActive) ? Mathf.PingPong(Time.time * speed, distance) : distance ;

        if ((!isLooping) && (pingPongValue >= distance - distanceOffset))
        {
            isMovingActive = false;
        }

        switch (direction)
        {
            case MovingPlatformDirection.HOR:
                transform.position = new Vector2(startingPos.x + pingPongValue,
            transform.position.y);
                break;
            case MovingPlatformDirection.VER:
                transform.position = new Vector2(transform.position.x,
            startingPos.y + pingPongValue);
                break;
            case MovingPlatformDirection.DIA_UP:
                transform.position = new Vector2(startingPos.x + pingPongValue,
           startingPos.y + pingPongValue);
                break;
            case MovingPlatformDirection.DIA_DOWN:
                transform.position = new Vector2(startingPos.x + pingPongValue,
           startingPos.y - pingPongValue);
                break;
            default:
                break;
        }

    }
}
