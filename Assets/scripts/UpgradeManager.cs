using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private int[] _costs;
    [SerializeField] private float[] _values;
    [SerializeField] private TMP_Text _textLevel;
    [SerializeField] private TMP_Text _textCostValue;
    [SerializeField] private TMP_Text _maxLevel;
    [SerializeField] private WeaponStats stats;
    [SerializeField] private GameObject _tower;
    [SerializeField] private MeshFilter[] _towerVersions;
    [SerializeField] private Button button;
    [SerializeField] private Image _tutorArrow;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private UpgradeManager _speedManger, _damageMaanager;
    [SerializeField] private bool isTutor;
    private int currentLevelSpeed, maxLevelsSpeed, nextLevelSpeed;
    private int currentLevelDamage, maxLevelsDamage, nextLevelDamage;
    private int currentLevelSolider, maxLevelscurrentLevelSolider, nextLevelcurrentLevelSolider;
    private int currentLevelSolider2, maxLevelscurrentLevelSolider2, nextLevelcurrentLevelSolider2;
    private int currentLevelSolider3, maxLevelscurrentLevelSolider3, nextLevelcurrentLevelSolider3;
    [SerializeField] private bool damage, speed, solider;
    private bool isMax;
    private int currentLevel, maxLevels, nextLevel;

    private void Awake()
    {
       // DontDestroyOnLoad(this);
        currentLevelSpeed = PlayerPrefs.GetInt("currentLevelSpeed");
        nextLevelSpeed = PlayerPrefs.GetInt("nextLevelSpeed", 2);
        currentLevelDamage = PlayerPrefs.GetInt("currentLevelDamage");
        nextLevelDamage = PlayerPrefs.GetInt("nextLevelDamage", 2);
        
        currentLevelSolider = PlayerPrefs.GetInt("currentLevelSolider",1);
        nextLevelcurrentLevelSolider = PlayerPrefs.GetInt("nextLevelcurrentLevelSolider", 2);

        currentLevelSolider2 = PlayerPrefs.GetInt("currentLevelSolider2", 1);
        nextLevelcurrentLevelSolider2 = PlayerPrefs.GetInt("nextLevelcurrentLevelSolider2", 2);

        currentLevelSolider3 = PlayerPrefs.GetInt("currentLevelSolider3", 1);
        nextLevelcurrentLevelSolider3 = PlayerPrefs.GetInt("nextLevelcurrentLevelSolider3", 2);

        maxLevelsSpeed = _values.Length;
        maxLevelsDamage = _values.Length;
        maxLevelscurrentLevelSolider = _values.Length;
        maxLevelscurrentLevelSolider2 = _values.Length;
        maxLevelscurrentLevelSolider3 = _values.Length;
;
    }

    private void Start()
    {
        if (isTutor && PlayerPrefs.GetInt("indexTutor") == 1)
            _tutorArrow.gameObject.SetActive(false);
        SetValues();
        if (damage)
        {
            if (currentLevel == maxLevels)
            {
                _maxLevel.gameObject.SetActive(true);
                _textLevel.gameObject.SetActive(false);
                _textCostValue.gameObject.SetActive(false);
                button.gameObject.SetActive(false);
                isMax = true;
            }
            else
            {
                _textLevel.text = "Level " + nextLevel.ToString();
                _textCostValue.text = _costs[currentLevel].ToString();
            }
        }


        if (speed)
        {
            if (currentLevel == maxLevels)
            {
                _maxLevel.gameObject.SetActive(true);
                _textLevel.gameObject.SetActive(false);
                _textCostValue.gameObject.SetActive(false);
                button.gameObject.SetActive(false);
                isMax = true;
            }
            else
            {
                _textLevel.text = "Level " + nextLevel.ToString();
                _textCostValue.text = _costs[currentLevel].ToString();
            }
        }



        if (gameObject.CompareTag("Solider1"))
        {
            if (currentLevelSolider == maxLevelscurrentLevelSolider)
            {
                button.gameObject.SetActive(false);
                isMax = true;
                _maxLevel.gameObject.SetActive(true);
                _textLevel.gameObject.SetActive(false);
                _textCostValue.gameObject.SetActive(false);
            }

            else
            {
                _textLevel.text = "Level " + nextLevelcurrentLevelSolider.ToString();
                _textCostValue.text = _costs[currentLevelSolider].ToString();
            }
        }

        else if (gameObject.CompareTag("Solider2"))
        {
            if (currentLevelSolider2 == maxLevelscurrentLevelSolider2)
            {
                isMax = true;
                button.gameObject.SetActive(false);
                _maxLevel.gameObject.SetActive(true);
                _textLevel.gameObject.SetActive(false);
                _textCostValue.gameObject.SetActive(false);
            }

            else
            {
                _textLevel.text = "Level " + nextLevelcurrentLevelSolider2.ToString();
                _textCostValue.text = _costs[currentLevelSolider2].ToString();
            }
        }

        else if (gameObject.CompareTag("Solider3"))
        {
            if (currentLevelSolider3 == maxLevelscurrentLevelSolider3)
            {
                button.gameObject.SetActive(false);
                isMax = true;
                _maxLevel.gameObject.SetActive(true);
                _textLevel.gameObject.SetActive(false);
                _textCostValue.gameObject.SetActive(false);
            }

            else
            {
                _textLevel.text = "Level " + nextLevelcurrentLevelSolider3.ToString();
                _textCostValue.text = _costs[currentLevelSolider3].ToString();
            }
        }

        if (!isMax)
            if (Bank.Instance.CoinsCount < _costs[currentLevel])
                _buttonImage.color = new Color32(106, 106, 106, 255);
            else
                _buttonImage.color = new Color32(125, 197, 11, 255);

    }

    public void Upgrade()
    {
        Debug.Log(currentLevel);

        if (currentLevel != maxLevels)
            if (Bank.Instance.CoinsCount >= _costs[currentLevel])
            {
                if (isTutor && PlayerPrefs.GetInt("indexTutor") == 0)
                {
                    _tutorArrow.gameObject.SetActive(false);
                    PlayerPrefs.SetInt("indexTutor", 1);
                    //GameManager.Instance.TutorOn();
                }
                Bank.Instance.SubtractCoins(_costs[currentLevel]);
                if (damage)
                    stats.AddDamage(_values[currentLevel]);
                else if (speed)
                    stats.AddSpeed(_values[currentLevel]);
                currentLevel++;
                nextLevel++;
                SaveCurrentLevelValues(currentLevel);
                SaveNextLevelValues(nextLevel);
                if (currentLevel == maxLevels)
                {
                    button.gameObject.SetActive(false);
                    _maxLevel.gameObject.SetActive(true);
                    _textLevel.gameObject.SetActive(false);
                    _textCostValue.gameObject.SetActive(false);
                    if (speed)
                    {
                        isMax = true;
                        _damageMaanager.CheckCost();
                    }
                       
                    else if (damage)
                    {
                        isMax = true;
                        _speedManger.CheckCost();
                    }
                       
                }
                else
                {
                    _textLevel.text = "Level " + nextLevel.ToString();
                    _textCostValue.text = _costs[currentLevel].ToString();

                    currentLevelDamage = PlayerPrefs.GetInt("currentLevelDamage");
                    currentLevelSpeed = PlayerPrefs.GetInt("currentLevelSpeed");
                
                        _damageMaanager.CheckCost();
                    
                    
                    
                        _speedManger.CheckCost();
                    
                }


            }
    }

    public void UpgradeAll()
    {
        Debug.Log(currentLevelSpeed);
        if (currentLevelSolider != maxLevelscurrentLevelSolider)
            if (Bank.Instance.CoinsCount >= _costs[currentLevelSolider])
            {
                
                Bank.Instance.SubtractCoins(_costs[currentLevelSolider]);
                nextLevelcurrentLevelSolider++;

                _tower.GetComponent<MeshFilter>().sharedMesh = _towerVersions[currentLevelSolider].sharedMesh;
                _textLevel.text = "Level " + nextLevelcurrentLevelSolider.ToString();
                
                stats.AddDamage(_values[currentLevelSolider]);
                stats.AddSpeed(0.15f);
                currentLevelSolider++;
               
                PlayerPrefs.SetInt("nextLevelcurrentLevelSolider", nextLevelcurrentLevelSolider);
                PlayerPrefs.SetInt("currentLevelSolider", currentLevelSolider);
                Debug.Log(currentLevelSolider);
                if (currentLevelSolider == maxLevelscurrentLevelSolider)
                {
                    button.gameObject.SetActive(false);
                    _maxLevel.gameObject.SetActive(true);
                    _textLevel.gameObject.SetActive(false);
                    _textCostValue.gameObject.SetActive(false);
                    isMax = true;
                }
                else
                {
                    _textCostValue.text = _costs[currentLevelSolider].ToString();
                    CheckCostSolider(gameObject.tag);
                }
                    
            }
    }

    public void UpgradeAllSol2()
    {
        Debug.Log(currentLevelSpeed);
        if (currentLevelSolider2 != maxLevelscurrentLevelSolider2)
            if (Bank.Instance.CoinsCount >= _costs[currentLevelSolider2])
            {

                Bank.Instance.SubtractCoins(_costs[currentLevelSolider2]);
                nextLevelcurrentLevelSolider2++;
                _tower.GetComponent<MeshFilter>().sharedMesh = _towerVersions[currentLevelSolider2].sharedMesh;
                _textLevel.text = "Level " + nextLevelcurrentLevelSolider2.ToString();
             
                stats.AddDamage(_values[currentLevelSolider2]);
                stats.AddSpeed(0.15f);
                currentLevelSolider2++;
                PlayerPrefs.SetInt("nextLevelcurrentLevelSolider2", nextLevelcurrentLevelSolider2);
                PlayerPrefs.SetInt("currentLevelSolider2", currentLevelSolider2);
                Debug.Log(currentLevelSolider);
                if (currentLevelSolider2 == maxLevelscurrentLevelSolider2)
                {
                    button.gameObject.SetActive(false);
                    _maxLevel.gameObject.SetActive(true);
                    _textLevel.gameObject.SetActive(false);
                    _textCostValue.gameObject.SetActive(false);
                    isMax = true;
                }
                else
                {

                    _textCostValue.text = _costs[currentLevelSolider2].ToString();
                    CheckCostSolider(gameObject.tag);

                }

            }
    }

    public void UpgradeAllSol3()
    {
        Debug.Log(currentLevelSpeed);
        if (currentLevelSolider3 != maxLevelscurrentLevelSolider3)
            if (Bank.Instance.CoinsCount >= _costs[currentLevelSolider3])
            {

                Bank.Instance.SubtractCoins(_costs[currentLevelSolider3]);
                nextLevelcurrentLevelSolider3++;
                _tower.GetComponent<MeshFilter>().sharedMesh = _towerVersions[currentLevelSolider3].sharedMesh;
                _textLevel.text = "Level " + nextLevelcurrentLevelSolider3.ToString();
                stats.AddDamage(_values[currentLevelSolider3]);
                stats.AddSpeed(0.15f);
                currentLevelSolider3++;
                PlayerPrefs.SetInt("nextLevelcurrentLevelSolider3", nextLevelcurrentLevelSolider3);
                PlayerPrefs.SetInt("currentLevelSolider3", currentLevelSolider3);
                Debug.Log(currentLevelSolider);
                if (currentLevelSolider3 == maxLevelscurrentLevelSolider3)
                {
                    button.gameObject.SetActive(false);
                    _maxLevel.gameObject.SetActive(true);
                    _textLevel.gameObject.SetActive(false);
                    _textCostValue.gameObject.SetActive(false);
                    isMax = true;
                }
                else
                {
                    _textCostValue.text = _costs[currentLevelSolider3].ToString();
                    CheckCostSolider(gameObject.tag);
                }
                  

            }
    }

    public void SetButton(Button but) => button = but;
    private void SaveNextLevelValues(int value)
    {
        if (damage)
            PlayerPrefs.SetInt("nextLevelDamage", value);
        else if (speed)
            PlayerPrefs.SetInt("nextLevelSpeed", value);

    }

    private void SaveCurrentLevelValues(int value)
    {
        if (damage) 
            PlayerPrefs.SetInt("currentLevelDamage", value);
        else if (speed)
            PlayerPrefs.SetInt("currentLevelSpeed", value);

    }

    private void SetValues()
    {

        if (damage)
        {
            nextLevel = nextLevelDamage;
            currentLevel = currentLevelDamage;
            maxLevels = maxLevelsDamage;
        }
        else if (speed)
        {
            nextLevel = nextLevelSpeed;
            currentLevel = currentLevelSpeed;
            maxLevels = maxLevelsSpeed;
        }



    }

    public void CheckCost()
    {
        if(!isMax)
        if (Bank.Instance.CoinsCount < _costs[currentLevel])
            _buttonImage.color = new Color32(106, 106, 106, 255);
        else
            _buttonImage.color = new Color32(125, 197, 11, 255);
    }

    public void CheckCostSolider(string tag)
    {
    

        if (gameObject.CompareTag(tag) && !isMax)
        {
  
            if (Bank.Instance.CoinsCount < _costs[currentLevelSolider])
                _buttonImage.color = new Color32(106, 106, 106, 255);
            else
                _buttonImage.color = new Color32(125, 197, 11, 255);
        }
  
        else if (gameObject.CompareTag(tag) && !isMax)
        {
            if (Bank.Instance.CoinsCount < _costs[currentLevelSolider2])
                _buttonImage.color = new Color32(106, 106, 106, 255);
            else
                _buttonImage.color = new Color32(125, 197, 11, 255);
        }

        else if (gameObject.CompareTag(tag) && !isMax)
        {
            if (Bank.Instance.CoinsCount < _costs[currentLevelSolider3])
                _buttonImage.color = new Color32(106, 106, 106, 255);
            else
                _buttonImage.color = new Color32(125, 197, 11, 255);
        }
    }
}
