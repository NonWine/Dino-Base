using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supporter : MonoBehaviour
{
    [SerializeField] private Animator _suppoerterAnim;
    [SerializeField] private Detector _detector;
    [SerializeField] private Transform _ShootPos;
    
    private void Start()
    {
        
    }
    void Update()
    {
        if (_detector.isDetected())
        {
            _ShootPos.localPosition = new Vector3(0.252999991f, -0.264999986f, -0.323000014f);
            _ShootPos.localRotation = Quaternion.Euler(new Vector3(316.557526f, 329.165619f, 304.653198f));
            _suppoerterAnim.SetBool("Shoot", true);
        }
        else
        {
            _ShootPos.localPosition = new Vector3(-0.187099993f, 0.0842999965f, -0.124399997f);
            _ShootPos.localRotation = Quaternion.Euler(new Vector3(6.77990055f, 5.86818361f, 41.4879303f));
            _suppoerterAnim.SetBool("Shoot", false);

        }
           

    }
}
