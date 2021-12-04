using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction = Vector3.zero;
    private Rigidbody2D body;
    public float duration;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        timer = duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
            Destroy(this.gameObject);
        move();
    }

    private void move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void setDirection(Vector3 a)
    {
        direction = a;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("Player").GetComponent<PlayerBehaviour>().shakeCamera();
        Destroy(this.gameObject);
    }
}
