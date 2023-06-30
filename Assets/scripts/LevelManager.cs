using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private List<GameObject> _levels;
    [SerializeField] private int testLevelIndex;
    public bool on;
    public int CurrentLevel { private set; get; }
    public int VisualCurrentLevel { private set; get; }

    private GameObject level;


    private void Awake()
    {
      //  DontDestroyOnLoad(this);
        Instance = this;
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (CurrentLevel == 0)
            VisualCurrentLevel = 0;
        VisualCurrentLevel = PlayerPrefs.GetInt("VisualCurrentLevel",1);

    }

    private void Start()
    {
      
        if (CurrentLevel >= _levels.Count)
            CurrentLevel = 0;
        //level = Instantiate(_levels[CurrentLevel]);
      //  level.GetComponentInChildren<Basemanager>().SetObjctPlayer(_player);
        _levels[CurrentLevel].SetActive(true);
    }

    public void FinishLevel()
    {
        CurrentLevel++;
        VisualCurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.SetInt("VisualCurrentLevel", VisualCurrentLevel);
    }

    public void LoadLevel()
    {
        SpawnEnemyManager.Instance.SetActiveWarButton(false);
        _levels[CurrentLevel].SetActive(false);
        ResetValues();
        FinishLevel();
        
        // Destroy(level);
        if (CurrentLevel >= _levels.Count)
        {
            SceneManager.LoadScene(0);
            CurrentLevel = 0;
            VisualCurrentLevel = 1;
        }
        _levels[CurrentLevel].SetActive(true);
        LevelUIManager.Instance.ResetLevelUiManager();
       
        // Application.Quit();
      //  LevelUIManager.Instance.ResetLevelUiManager();

    }

    private void ResetValues()
    {
       // PlayerPrefs.SetInt("currentLevelSpeed",0);
      //  PlayerPrefs.SetInt("nextLevelSpeed", 2);
      //  PlayerPrefs.SetInt("currentLevelDamage",0);
     //   PlayerPrefs.SetInt("nextLevelDamage", 2);
        PlayerPrefs.SetInt("currentLevelSolider", 1);
        PlayerPrefs.SetInt("nextLevelcurrentLevelSolider", 2);
        PlayerPrefs.SetInt("currentLevelSolider2", 1);
        PlayerPrefs.SetInt("nextLevelcurrentLevelSolider2", 2);
        PlayerPrefs.SetInt("currentLevelSolider3", 1);
        PlayerPrefs.SetInt("nextLevelcurrentLevelSolider3", 2);
        PlayerPrefs.SetInt("saveBuy",0);
        PlayerPrefs.SetInt("saveBuy2", 0);
        PlayerPrefs.SetInt("saveBuy3", 0);
        PlayerPrefs.SetInt("_waveDistance", 1);
        PlayerPrefs.SetInt("_countsOfBigMobs",0);
        PlayerPrefs.SetInt("_generalCountMobs", 9);
        
        Basemanager.Instance.ReserSaveId();
      SpawnEnemyManager.Instance.ResetSpawn();
    }

    [ContextMenu("SetTestLevel")]
    public void SetTestLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", testLevelIndex);
    }
}