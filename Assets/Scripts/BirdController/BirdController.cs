using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float jump;
    public float leanRate;
    private int score = 0;
    private bool isLive;

    public float gameOver = 0; 
    private Rigidbody2D _rigidbody2D;

    private Animator _animator;
    public static BirdController instance;

    void Awake()
    {
        isLive = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _MakeInstance();
    }
    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }   
    }
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _flyClip, _pingClip, _diedClip;
    
    void Update()
    {
        if (isLive)
        {
            _MoveMethod();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Scoring")
        {
            _audioSource.PlayOneShot(_pingClip);
            score++;
            if (Gameplay.instance != null)
            {
                Gameplay.instance._SetScore(score);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Pipe")
        {
            gameOver = 1;
            isLive = false;
            _audioSource.PlayOneShot(_diedClip);
            _animator.SetTrigger("died");
            
            if (Gameplay.instance != null)
            {
                Gameplay.instance._ShowPanel();
                Gameplay.instance._EndScore(score);
            }
            
        }
    }

    void _MoveMethod()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("up"))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jump);
            _audioSource.PlayOneShot(_flyClip);
        }

        if (Input.GetKeyDown("down"))
        {
            float stronger = -jump * 1.5f;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, stronger);
            _audioSource.PlayOneShot(_flyClip);
            
            
        }
        
        
        if (_rigidbody2D.velocity.y > 0)
        {
            float lean = 0;
            lean = Mathf.Lerp(0, 90, _rigidbody2D.velocity.y / leanRate);
            transform.rotation = Quaternion.Euler(0, 0 , lean);
            
        } else if (_rigidbody2D.velocity.y < 0)
        {
            float lean = 0;
            lean = Mathf.Lerp(0, -90, -_rigidbody2D.velocity.y / leanRate);
            transform.rotation = Quaternion.Euler(0, 0 , lean);
        } else if (_rigidbody2D.velocity.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
