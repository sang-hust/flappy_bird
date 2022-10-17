using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHolder : MonoBehaviour
{
    public float speed;
    private float leftEdge;

    private void Start()
    {
        
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    void Update()
    {
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
        
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }

        if (BirdController.instance.gameOver != null)
        {
            if (BirdController.instance.gameOver == 1f) 
            {
                Destroy(GetComponent<PipeHolder>());
            }
        }
    }
}
