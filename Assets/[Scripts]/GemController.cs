using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField] private float rotationspeed;
    [SerializeField] private AudioSource pickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        pickUpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        spinGem();
    }

    private void spinGem()
    {
         transform.Rotate(Vector3.up, rotationspeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            pickUpSound.Play();
            StartCoroutine(destoryGem());
        }
    }

    private IEnumerator destoryGem()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
