using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private float _speed;
    private PlayerController _player;  //handle
    private Animator _anim;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        
        //null check player
        if (_player == null)
        {
            Debug.LogError("Player not found");
        }
        
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator not found");
        }
    }

    void Update()
    {
        //move down
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX,7,0);  //spawna inimigo no intervalo de x
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  //inimigo colidiu com a nave
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            
            if (player != null) //verifica se o player existe
            {
                player.Damage(); //tira um de vida
            }
            
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);  //destroi inimigo
        }

        if (other.CompareTag("Laser"))  //inimigo tomou o laser
        {
            Destroy(other.gameObject);   //destroi o inimigo
            
            if (_player != null)
            {
                _player.AddScore(5);
            }
            
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);    //destroi o laser
        }
    }
}
