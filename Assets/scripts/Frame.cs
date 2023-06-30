using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
public class Frame : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody[] _planks;
    [SerializeField] private int valueToRepair;
    [SerializeField] private GameObject _canvas;
    private List<Vector3> startPosPlanks = new List<Vector3>();
    private List<Dino> _enemies = new List<Dino>();
    private int countPlank;
    private float percentForPlank;
    private float sumAllDamage;
    private float startHealth;
    private bool broke;
    private void Start()
    {
        foreach (var item in _planks)
        {
            startPosPlanks.Add(item.gameObject.transform.localPosition);
        }
        percentForPlank = health / _planks.Length;
        startHealth = health;
    }

    private void OnTriggerEnter(Collider other)
    {
      //  Debug.Log(SpawnEnemyManager.Instance.isWave());
        if (other.CompareTag("Player") && !SpawnEnemyManager.Instance.isWave())
        {
            if (health < startHealth)
                Repair();
            // _repairButton.SetActive(true);
        }
        else
            return;
    }

    public  void GetDamage(float damag) 
    {
        if (broke)
        {
            SetAllPlayerTarget();
            return;
        }
           
        health -= damag;
        sumAllDamage += damag;
       
        if (sumAllDamage >= percentForPlank && countPlank < _planks.Length )
        {
            _planks[countPlank].isKinematic = false;
            _planks[countPlank].WakeUp();
            _planks[countPlank].AddForce(Vector3.down * 50,ForceMode.VelocityChange);
            countPlank++;
            sumAllDamage = 0;
        }

        if (health <= 0 && !broke)
        {
            broke = true;
            foreach (var item in _planks)
            {
                if (item.isKinematic)
                {
                    item.isKinematic = false;
                    item.WakeUp();
                    item.AddForce(Vector3.right * 50, ForceMode.VelocityChange);
                }
            }
            SetAllPlayerTarget();
            GetComponent<NavMeshObstacle>().enabled = false;
           // gameObject.SetActive(false);
        }
           
    }
    public void GetEnemiesInfo(Dino obj)
    {
        _enemies.Add(obj);
    }
    private  void SetAllPlayerTarget()
    {
    
        Debug.Log(PlayerController.Instance.GetTargetPlayer());
        foreach (var item in _enemies)
        {
            item.setTarget(PlayerController.Instance.GetTargetPlayer());
         //    item.CanDamage();
        }
    }

    public void Repair()
    {
        OffIcon();
        broke = false;
        FrameManager.Instance.ReduceCost(valueToRepair);
        GetComponent<NavMeshObstacle>().enabled = true;
        for (int i = 0; i < _planks.Length; i++)
        {
            _planks[i].transform.localPosition = startPosPlanks[i];
            _planks[i].transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            _planks[i].isKinematic = true;
           
        }
        countPlank = 0;
        health = startHealth;
    }


    public void SetPlayer(Transform player) => _player = player;

    public bool isBroke() { return broke;  }

    public void CheckStateFrame()
    {
        if (health < startHealth)
        {
            _canvas.SetActive(true);
        }
    }

    public void OffIcon() => _canvas.SetActive(false);
}
