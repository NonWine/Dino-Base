using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObject/Weapon", order = 1)]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _Damage;
    [SerializeField] private float _CoolDown;
    [SerializeField] public int _Count;
    
    public void SetStats()
    {
        _Damage = PlayerPrefs.GetFloat("Damage", _Damage);
        _speed = PlayerPrefs.GetFloat("Speed", _speed);
        _CoolDown = PlayerPrefs.GetFloat("CD", _CoolDown);
    }
    
    public float GetSpeed() { return _speed; }
    public float GetDamage() { return _Damage; }
    public float GetCD() { return _CoolDown; }
    public int GetCount() { return _Count; }

    public void AddDamage(float value) {
        _Damage += value;
        PlayerPrefs.SetFloat("Damage", _Damage);
        
            }

    public void AddSpeed(float value) { _CoolDown -= value;
        PlayerPrefs.SetFloat("CD", _CoolDown);
    }

    public void ResetStats()
    {
        _Damage = 35f;
        _CoolDown = 0.4f;
    }
}
