using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private WeaponStats stats;
    [SerializeField] private Transform _psPosDebris;
    [SerializeField] private Detector _detector;
    [SerializeField] private Debris _debris;
    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private bool _playFirePS;
    private float timer = 0;

    private void Start()
    {
        stats.SetStats();
    }

    void Update()
    {
       
        if (_detector.isDetected())
        {
            timer += Time.deltaTime;

            if (timer >= stats.GetCD())
            {
                if (_playFirePS)
                    //ParticlePool.Instance.PlayFire(transform.position, Quaternion.Euler(new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f))),_psPosDebris.forward);
                     _fire.Play();
                Debris debris = Instantiate(_debris, _psPosDebris.position, _psPosDebris.rotation);
                GameObject prefabBullet = Instantiate(_bullet, transform.position, transform.rotation);
                debris.Force(_psPosDebris.forward);
                
                prefabBullet.GetComponent<Bullet>().SetDamage(stats.GetDamage());
                Rigidbody bulletBody = prefabBullet.GetComponent<Rigidbody>();
                Vector3 target = _detector.GetTargetInfo();
                if (target != null)
                    bulletBody.velocity = (target - prefabBullet.transform.position).normalized * stats.GetSpeed();
                timer = 0;
            }
        }
        else
        {
            {
               // if (_playFirePS)
                    //_fire.Stop();
                      //  ParticlePool.Instance.StopFire();
            }

        }


    }


}

