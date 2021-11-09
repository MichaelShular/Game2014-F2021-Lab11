using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    public float runForce;
    public Transform lookAheadPoint;
    public Transform lookInFrontPoint;

    public LayerMask goundLayerMask;
    public LayerMask wallLayerMask;
    public bool isGroundAhead;

    private Rigidbody2D enemyRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    
    // Update is called once per frame
    
    void FixedUpdate()
    {
        LookInFront();
        lookAhead();
        MoveEnemy();
    }

    private void lookAhead()
    {
        var Hit = Physics2D.Linecast(transform.position, lookAheadPoint.position, goundLayerMask);
        if (Hit)
        {
            isGroundAhead = true;
        }
        else
        {
            isGroundAhead = false;
        }
    }

    private void LookInFront()
    {
        var Hit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, wallLayerMask);
        if (Hit)
        {
            flip();
        }
               
    }
    private void MoveEnemy()
    {
        if (isGroundAhead)
        {
            enemyRigidbody.AddForce(Vector2.left * runForce * transform.localScale.x);
            enemyRigidbody.velocity *= 0.90f;
        }
        else
        {
            flip();
        }
    }

    private void flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(lookAheadPoint.position, this.transform.position);
        Gizmos.DrawLine(lookInFrontPoint.position, this.transform.position);
    }
}
