using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DinoSpawn
{
    public Transform _spawnPoint;
    public Frame _frame;
    public GameObject _enemy;
    public GameObject _bihEnemy;
    private int _counts;
    private int _countsBigMobs;
    public void SetCount(int x) => _counts = x;

    public int GetCount() { return _counts; }

    public void SetCountBig(int x) => _countsBigMobs = x;

    public int GetCountBig() { return _countsBigMobs; }


}
