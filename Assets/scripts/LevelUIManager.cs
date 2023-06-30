using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelUIManager : MonoBehaviour
{
    public static LevelUIManager Instance { get; private set; }
    [SerializeField] private Image[] _levelImages;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private Transform _winFxPos;
    [SerializeField] private WeaponStats playerstats;
    private int _index;
    private bool _finalWave;
    public int VisualCurrentLevelUI { private set; get; }

    private void Awake()
    {
      
        Instance = this;
        VisualCurrentLevelUI = PlayerPrefs.GetInt("VisualCurrentLevelUI", 1);
        if(VisualCurrentLevelUI >= 20)
        {
            for (int i = 0; i < _levelImages.Length; i++)
            {
                _levelImages[i].color = _levelImages[i].color = new Color32(34, 188, 43, 255); // green
            }
        }
    }

    private void Start()
    {
        _index = PlayerPrefs.GetInt("_index");
      
        for (int i = 0; i <= _index; i++)
        {
            _levelImages[i].color = new Color32(34, 188, 43, 255); // green
            _levelImages[_index].GetComponent<RectTransform>().sizeDelta = new Vector2(30, 50);
            if (i == _index)
            {
                _levelImages[i].color = new Color32(255, 120, 0, 255);
                _levelImages[_index].GetComponent<RectTransform>().sizeDelta = new Vector2(30, 60);

            }

        }
        
    }

    private void SetNextWave()
    {
        Debug.Log(_index);
        Debug.Log(_levelImages.Length);

        if (_index > _levelImages.Length)
            return;

            if (_index > _levelImages.Length - 1)
                FinishLevel();

            else
            {
                _levelImages[_index].color = new Color32(255, 120, 0, 255); // orangev
                _levelImages[_index].GetComponent<RectTransform>().sizeDelta = new Vector2(30, 60);
            }

            if (_index == _levelImages.Length - 1)
                _finalWave = true;
        

    }

    private void FinishLevel()
    {
        //if (LevelManager.Instance.CurrentLevel == 1)
        //{
        //    PlayerPrefs.DeleteAll();
        //    playerstats.ResetStats();
        //    SceneManager.LoadScene(0);
        //}
           
        if(LevelManager.Instance.CurrentLevel == 0)
        {
            SpawnEnemyManager.Instance.WarOff();
            _nextLevelButton.SetActive(true);
        }
            
        
            
    
    }

    public void ResetLevelUiManager()
    {
        _finalWave = false;
        //VisualCurrentLevelUI = 1;
        //PlayerPrefs.SetInt("VisualCurrentLevelUI", VisualCurrentLevelUI);
        _index = 0;
        PlayerPrefs.SetInt("_index", _index);
        for (int i = 0; i < _levelImages.Length; i++)
            _levelImages[i].color = new Color32(166, 166, 166, 255); //default
        _levelImages[_index].color = new Color32(255, 120, 0, 255);
        _levelImages[_index].GetComponent<RectTransform>().sizeDelta = new Vector2(30, 60);
        GameManager.Instance.NextLevel();
    }

    public void NextLocationLevel()
    {
        Debug.Log("ASASDAS");
      
        _nextLevelButton.SetActive(false);
        LevelManager.Instance.LoadLevel();
      //  LevelManager.Instance.FinishLevel();
    }

    public void FinishWave()
    {
        //  if(VisualCurrentLevelUI == 1)
        // Bank.Instance.AddCoins(15);
        VisualCurrentLevelUI++;
        PlayerPrefs.SetInt("VisualCurrentLevelUI", VisualCurrentLevelUI);
        if(_index < _levelImages.Length)
        {
            _levelImages[_index].color = new Color32(34, 188, 43, 255);
            _levelImages[_index].GetComponent<RectTransform>().sizeDelta = new Vector2(30, 50);
            _index++;
        }
       
        PlayerPrefs.SetInt("_index", _index);
        SetNextWave();
        Player player = PlayerController.Instance.GetComponent<Player>();
        player.ToHeal();
       
    }

    public bool isFinalWave()
    {
        return _finalWave;
    }

    public void FinalWaveFalse() => _finalWave = false;

}
