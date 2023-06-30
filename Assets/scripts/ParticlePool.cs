using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;

    [SerializeField] private ParticleSystem[] debrisFx;
    [SerializeField] private GameObject[] fireFx;
    [SerializeField] private ParticleSystem[] bloodFx;
    [SerializeField] private ParticleSystem[] _winFx;
    private int currentfire;
    private int currentdebris;
    private int currentBlood;
    private int currentWin;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayDebrisHit(Vector3 pos, Quaternion rot)
    {
        debrisFx[currentdebris].transform.position = pos;
        debrisFx[currentdebris].transform.rotation = rot;
        debrisFx[currentdebris].Play();
        currentdebris++;
        if (currentdebris == debrisFx.Length)
            currentdebris = 0;
    }

    public void PlayFire(Vector3 pos, Quaternion rotation, Vector3 force)
    {
        fireFx[currentfire].GetComponent<Debris>().Force(force);
        fireFx[currentfire].transform.position = pos;
        fireFx[currentfire].transform.rotation = rotation;

        currentfire++;
        if (currentfire == fireFx.Length)
            currentfire = 0;
    }

    public void StopFire()
    {
       // fireFx[currentfire].transform.position = new Vector3(1000f, 1000f, 1000f);
        currentfire++;
        if (currentfire == fireFx.Length)
            currentfire = 0;
    }

    public void PlayBloodEnemy(Vector3 pos)
    {
        bloodFx[currentBlood].transform.position = pos;
        bloodFx[currentBlood].Play();
        currentBlood++;
        if (currentBlood == bloodFx.Length)
            currentBlood = 0;
    }

    public void PlayWinFx()
    {
        _winFx[0].Play();
    }
}
