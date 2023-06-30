using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSolider : MonoBehaviour
{
    [SerializeField] private GameObject _tower;
    [SerializeField] private MeshFilter[] _towerVersions;
    private int currentLevelSolider;
    private void Awake()
    {
        ShowShopButton but = GetComponent<ShowShopButton>();
        if(but.GetButton().CompareTag("Solider1"))
        currentLevelSolider = PlayerPrefs.GetInt("currentLevelSolider",1);
        else if(but.GetButton().CompareTag("Solider2"))
            currentLevelSolider = PlayerPrefs.GetInt("currentLevelSolider2", 1);
        else if (but.GetButton().CompareTag("Solider3"))
            currentLevelSolider = PlayerPrefs.GetInt("currentLevelSolider3", 1);

    }

    private void Start()
    {
        
        _tower.GetComponent<MeshFilter>().sharedMesh = _towerVersions[currentLevelSolider-1].sharedMesh;

    }
}
