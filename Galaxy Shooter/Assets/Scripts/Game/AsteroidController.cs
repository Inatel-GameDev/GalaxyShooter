using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 3.0f;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        //rotate object on ted zed axis!
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
    
    //check for the laser collission
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.2f);
        }
    }
    
    
}
