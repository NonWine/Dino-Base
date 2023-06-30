using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _TutorialPanel;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private LineProgress _lineProgress;
    [SerializeField] private TextMeshProUGUI _leveltextWin;
    [SerializeField] private GameObject _arrowTutorUnlock;
    private bool isFinish;
    private int indexTutor;
    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        indexTutor = PlayerPrefs.GetInt("indexTutor", 0);
    }

    private void Start()
    {

    //Bank.Instance.AddCoins(15000);
        Debug.Log(_TutorialPanel);
        if (indexTutor == 0)
            _TutorialPanel.SetActive(true);
        _gamePanel.SetActive(true);
        _levelText.SetText("Level " + (LevelUIManager.Instance.VisualCurrentLevelUI).ToString());
     //   _leveltextWin.SetText("Level " + (PlayerPrefs.GetInt("VisualCurrentLevelUI")).ToString());

    }

    public void GameLose()
    {
        if (isFinish)
            return;
        _losePanel.SetActive(true);
        _gamePanel.SetActive(false);
    }

    public void GameWin()
    {
        if (isFinish)
            return;
        _leveltextWin.SetText("Level " + ((LevelUIManager.Instance.VisualCurrentLevelUI)).ToString());
        StartCoroutine(GameWinCor());
        //LevelUIManager.Instance.FinishWave();
        //_levelText.SetText("Level " + (LevelUIManager.Instance.VisualCurrentLevelUI).ToString());
    }

    public void NextLevel()
    {
       // isFinish = false;
       // _winPanel.SetActive(false);
      //  _gamePanel.SetActive(true);
      //  LevelManager.Instance.LoadLevel();
           _levelText.SetText("LEVEL " + (LevelUIManager.Instance.VisualCurrentLevelUI).ToString());

    }

    public void RestartLevel()
    {
        _losePanel.SetActive(false);
        _gamePanel.SetActive(true);
        SpawnEnemyManager.Instance.LoseGame();
        _lineProgress.RestartLineProgress();
        LevelUIManager.Instance.gameObject.SetActive(true);
        PlayerController.Instance.GetComponent<Player>().EnaableToLive();
        //isFinish = false;
        //_gamePanel.SetActive(true);
        //_losePanel.SetActive(false);

        //LevelManager.Instance.LoadLevel();
    }

    public void OffTutor() => _TutorialPanel.SetActive(false);

    public void TutorOn() => _TutorialPanel.SetActive(true);
    private IEnumerator GameWinCor()
    {
        yield return new WaitForSeconds(0.6f);
        _gamePanel.SetActive(false);
        ParticlePool.Instance.PlayWinFx();
        yield return new WaitForSeconds(0.1f);
        _winPanel.SetActive(true);
        LevelUIManager.Instance.FinishWave();
        _levelText.SetText("Level " + (LevelUIManager.Instance.VisualCurrentLevelUI).ToString());
        yield return new WaitForSeconds(2.5f);
        _winPanel.SetActive(false);
        _gamePanel.SetActive(true);
        if (LevelUIManager.Instance.VisualCurrentLevelUI == 2)
        {
            _arrowTutorUnlock.SetActive(true);
        }
          

    }

    private void StartTutorial()
    {
        _TutorialPanel.SetActive(true);
    }
}