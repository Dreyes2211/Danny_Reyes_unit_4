using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;
    public float speed = 10.0f; 
    public float powerUpSpeed = 10.0f;
    GameObject focalPoint; 
    Renderer rendererPlayer;

    bool hasPowerUp = false; 
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        rendererPlayer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float magnitude = forwardInput * speed * Time.deltaTime;
        rbPlayer.AddForce(focalPoint.transform.forward * magnitude, ForceMode.Impulse);

        if(forwardInput > 0)
        {
            rendererPlayer.material.color = new Color(1.0f - forwardInput, 1.0f, 1.0f - forwardInput);
        }
        else
        {
            rendererPlayer.material.color = new Color(1.0f + forwardInput, 1.0f, 1.0f + forwardInput);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
         Debug.Log("Player has collided with" + collision.gameObject + "with powerup set to: " + hasPowerUp);
        if(hasPowerUp && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with" + collision.gameObject + "with powerup set to: " + hasPowerUp);
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDir = collision.gameObject.transform.position - transform.position;

            rbEnemy.AddForce(awayDir * powerUpSpeed, ForceMode.Impulse);
        }
    }
}
