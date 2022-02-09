using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private bool touch;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (touch)
                rb.AddForce(transform.up * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Jurang"))
        {
            Debug.Log("Kena");
            Game.lives -= 1;
            Game.sekor = 0;
            SceneManager.LoadScene("Test");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tanah"))
        {
            touch = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tanah"))
        {
            touch = false;
        }
    }
}
