using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
public class SpawnEnemyManager : MonoBehaviour
{
    public static SpawnEnemyManager Instance { get; private set; }
    [SerializeField] private DinoSpawn[] _spawnPoints;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _generalCountMobs;
    [SerializeField] private int _increaseGeneralMobsByWave;
    [SerializeField] private GameObject _uiLevelProgress;
    [SerializeField] private LineProgress _lineProgress;
    [SerializeField] private GameObject _boss;
    [SerializeField] private Button _warButton;
    [SerializeField] private GameObject _warObject;
    private int _waveDistance;
    private int _countsOfBigMobs;
    private int _allEnemies;
    private int id;
    private bool wave = false;
    private void Awake()
    {
        _waveDistance = PlayerPrefs.GetInt("_waveDistance",1);
        _countsOfBigMobs = PlayerPrefs.GetInt("_countsOfBigMobs");
        id = PlayerPrefs.GetInt("id");
        _generalCountMobs = PlayerPrefs.GetInt("_generalCountMobs",5);
     
        Instance = this;
    }

    private void Start()
    {

        if(LevelUIManager.Instance.VisualCurrentLevelUI > 20)
        {
            _increaseGeneralMobsByWave = 4;
        }

        Debug.Log(id);
        _warObject.gameObject.SetActive(true);
        _warButton.onClick.AddListener(delegate { StartWave(); });
       // _waveDistance = 1;
        for(int i=0;i<= id; i++)
        {
            _spawnPoints[i].SetCount(_generalCountMobs);
        }
        
    }

    //public void SetSpawn(int id)
    //{
    //    Instantiate(_spawnPoints[id]._enemy, _spawnPoints[id]._spawnPoint.position, _spawnPoints[id]._spawnPoint.rotation);
    //}

    public void LoseGame()
    {
        _allEnemies = 0;
        _warObject.gameObject.SetActive(true);
        wave = false;
     //   FrameManager.Instance.SetRepairValue();
        FrameManager.Instance.SetRepairButtonTrue();
    }

    public void AddDeathEnemy()
    {
        _allEnemies--;
        _lineProgress.UpdateLineProgress();
        if(_allEnemies == 0)
        {
            for (int i = 0; i <= id; i++)
            {
                _spawnPoints[i]._frame.CheckStateFrame();
            }
            _lineProgress.UpdateLineProgress();
            _generalCountMobs += _increaseGeneralMobsByWave;
            PlayerPrefs.SetInt("_generalCountMobs", _generalCountMobs);
            _warObject.gameObject.SetActive(true);
            wave = false;
            GameManager.Instance.GameWin();
            _allEnemies = 0;
        }
    }

    private async void Spawn(int id)
    {
        int i = 0;

        while (i < _spawnPoints[id].GetCount())
        {
            await UniTask.Delay((int)(1000 * _spawnDelay));
            GameObject dino =   Instantiate(_spawnPoints[id]._enemy, _spawnPoints[id]._spawnPoint.position, _spawnPoints[id]._spawnPoint.rotation);
            Dino enemy = dino.GetComponent<Dino>();
            DinoList.AddDinoInList(enemy.gameObject);
            Frame frame = _spawnPoints[id]._frame;
            if (!frame.isBroke())
                enemy.setTarget(frame.transform);
            else
                enemy.setTarget(PlayerController.Instance.transform);
               
            frame.GetEnemiesInfo(enemy);  
            i++;
        }
    }
    
    private async void SpawnAvarageMob(int id)
    {
        int i = 0;
        while (i < _spawnPoints[id].GetCountBig())
        {
            await UniTask.Delay((int)(1000 * _spawnDelay));
            GameObject dino = Instantiate(_spawnPoints[id]._bihEnemy, _spawnPoints[id]._spawnPoint.position, _spawnPoints[id]._spawnPoint.rotation);
            Dino enemy = dino.GetComponent<Dino>();
            DinoList.AddDinoInList(enemy.gameObject);
            Frame frame = _spawnPoints[id]._frame;
            if (!frame.isBroke())
                enemy.setTarget(frame.transform);
            else
                enemy.setTarget(PlayerController.Instance.transform);
            frame.GetEnemiesInfo(enemy);
            i++;
        }
    }

    private void IncreaseBigMob()
    {
        if (_waveDistance >= 1)
        {
            _countsOfBigMobs++;
            PlayerPrefs.SetInt("_countsOfBigMobs", _countsOfBigMobs);
            _waveDistance = 0;
            PlayerPrefs.SetInt("_waveDistance", _waveDistance);
        }
        else
        {
            _waveDistance++;
            PlayerPrefs.SetInt("_waveDistance", _waveDistance);
        }
           
          
    }

    private void ToRandomSpawn()
    {
        int tMpbs = _generalCountMobs;
        
        if (PlayerPrefs.GetInt("VisualCurrentLevelUI") >= 4)
        {
            IncreaseBigMob();
            if (_countsOfBigMobs > 4 && LevelManager.Instance.CurrentLevel == 0)
            {
                _countsOfBigMobs = 4;
                PlayerPrefs.SetInt("_countsOfBigMobs", _countsOfBigMobs);
            }
               else if(_countsOfBigMobs > 6 && LevelManager.Instance.CurrentLevel == 1)
            {
                _countsOfBigMobs = 6;
                PlayerPrefs.SetInt("_countsOfBigMobs", _countsOfBigMobs);
            }
            int tbigMobs = _countsOfBigMobs;
            for (int i = 0; i <= id; i++)
            {
                int y = Random.Range(0, tbigMobs + 1);
                tbigMobs -= y;
                if (tbigMobs <= 0)
                    tbigMobs = 0;
                if (i == id)
                    _spawnPoints[i].SetCountBig(y + tbigMobs);

                else
                    _spawnPoints[i].SetCountBig(y);


                SpawnAvarageMob(i);
            }

        }


        _allEnemies = _countsOfBigMobs;
        Debug.Log(id);
        for (int i = 0; i <= id; i++)
        {
            int x = Random.Range(0, tMpbs + 1);
            tMpbs -= x;
            if (tMpbs <= 0)
                tMpbs = 0;

            if (i == id)
                _spawnPoints[i].SetCount(x + tMpbs);
            
            else
                _spawnPoints[i].SetCount(x);
            
            Spawn(i);
            _allEnemies += _spawnPoints[i].GetCount();
            x = 0;
        }
      
        Debug.Log(_allEnemies);
    }

    private void ActivateLineProgress()
    {
        _lineProgress.gameObject.SetActive(true);
        _lineProgress.ChangeLevelWave();
        _lineProgress.SetMobs(_allEnemies);
    }

    private void BossSpawn()
    {
        int pos = Random.Range(0, id + 1);
        GameObject boss = Instantiate(_boss, _spawnPoints[pos]._spawnPoint.position, _spawnPoints[pos]._spawnPoint.rotation);
        Dino enemy = boss.GetComponent<Dino>();
        DinoList.AddDinoInList(enemy.gameObject);
        Frame frame = _spawnPoints[pos]._frame;
        if (!frame.isBroke())
            enemy.setTarget(frame.transform);
        else
            enemy.setTarget(PlayerController.Instance.transform);
        frame.GetEnemiesInfo(enemy);
        _allEnemies++;
        if (LevelManager.Instance.CurrentLevel == 1)
        {
            LevelUIManager.Instance.FinalWaveFalse();
         
        }
           
    }

    public void StartWave()
    {

        for (int i = 0; i <= id; i++)
        {
            _spawnPoints[i]._frame.OffIcon();
        }

        wave = true;
        _warObject.gameObject.SetActive(false);
        _uiLevelProgress.SetActive(false);
        GameManager.Instance.OffTutor();
        ToRandomSpawn();
        if (LevelUIManager.Instance.isFinalWave())
        {
            BossSpawn();
        }
        ActivateLineProgress();
    }

    public bool isWave() { return wave; }

    public void AddZonaSpawn()
    {
        id++;
        PlayerPrefs.SetInt("id", id);
    //    FrameManager.Instance.AddFrame(_spawnPoints[id]._frame);
    }

    public void WarOff() => _warObject.SetActive(false);

    public void ResetSpawn()
    {
        _warObject.SetActive(false);
        id = 0;
        PlayerPrefs.SetInt("id", id);
    }

    public void SetActiveWarButton(bool flag) => _warButton.gameObject.SetActive(flag);


    public void CheckStateFrame()
    {
        for (int i = 0; i <= id; i++)
        {
            _spawnPoints[i]._frame.CheckStateFrame();
        }
    }
}
