using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private float _radius;
    [SerializeField] private Transform player;
    [SerializeField] private float _speedRotation =8f;
    [SerializeField] private bool _rotateOnlyYAxis;
    private PlayerController playerController;
    private  bool inTrigger;
    private Vector3 closestTarget;
    private float point;

    private void Start()
    {
        point = Mathf.Infinity;
        closestTarget = Vector3.zero;
        if(transform.parent.CompareTag("Player"))
        playerController = GetComponentInParent<PlayerController>();
    }
    private void Update()
    {

       
        if (inTrigger)
        {
            if (transform.parent.CompareTag("Player"))
                playerController.ChangeAbilityToRotate(false);
            point = Mathf.Infinity;
            Vector3 dir = closestTarget - player.transform.position;
            Quaternion rot = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(dir), _speedRotation * Time.deltaTime);
            if (_rotateOnlyYAxis)
            {
                rot.x = 0f;
                rot.z = 0f;
            }
            player.rotation = rot;
        }
        else
             if (transform.parent.CompareTag("Player"))
                playerController.ChangeAbilityToRotate(true);
        TryFindTheNearlestEnemy();
    }
    private void TryFindTheNearlestEnemy()
    {
        Collider[] enemys = Physics.OverlapSphere(transform.position, _radius);
        foreach (var item in enemys)
        {
            if (item.CompareTag(_tag))
            {
                if (!item.GetComponent<Dino>().isDead())
                {
                    inTrigger = true;
                    break;
                }
        
            }
            else
                inTrigger = false;

        }

        if (inTrigger)
            for (int i = 0; i < enemys.Length; i++)
            {
                if (enemys[i].CompareTag(_tag))
                {
                    Vector3 close = enemys[i].ClosestPoint(transform.position);
                    Vector3 dir = player.transform.position - close;
                    if (point > dir.magnitude && !enemys[i].GetComponent<Dino>().isDead())
                    {
                        point = dir.magnitude;
                        closestTarget = enemys[i].transform.position;
                    }
                }

            }

    }
    public  bool isDetected() { return inTrigger; }
    public void SetDetected(bool flag) => inTrigger = flag;
    public  Vector3 GetTargetInfo() { return closestTarget; }
}
