using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;
    private Transform _target;
    private Zombie zombie;

    void Start()
    {
        zombie = GetComponent<Zombie>();
        _target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (zombie.isAlive())
        {
       
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            transform.LookAt(_target);
        }      
    }
}
