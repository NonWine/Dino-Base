using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private float speedLerp;
    [SerializeField] private Vector3 startCamPos;
    private Transform player;
    void LateUpdate()
    {
        FollowToPlayer();
    }

    private void FollowToPlayer()
    {
        if (transform.position != player.position)
                 transform.position = Vector3.MoveTowards(transform.position,player.position + startCamPos , speedLerp);
    }

    public void GetPlayer(Transform pos) => player = pos;
}
