using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowShopButton : MonoBehaviour
{

    [SerializeField] private GameObject _showButton;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject tutorIm;
    [SerializeField] private GameObject _warbutton;
    [SerializeField] private UpgradeManager[] upgrades;
    private void Start()
    {
     

        if (PlayerPrefs.GetInt("indexTutor") == 1 && gameObject.CompareTag("TutorMarket"))
            tutorIm.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !SpawnEnemyManager.Instance.isWave())
        {
            _showButton.SetActive(true);
            if (gameObject.CompareTag("TutorMarket"))
            {
                tutorIm.SetActive(false);
                if(LevelUIManager.Instance.VisualCurrentLevelUI ==2)
                GameManager.Instance.OffTutor();
            }
            
           

            if (upgrades.Length > 0)
            {
                if (!gameObject.CompareTag("SupportUpgrade"))
                    foreach (var item in upgrades)
                    {
                        item.CheckCost();
                    }
                else
                    upgrades[0].CheckCostSolider(upgrades[0].tag);
            }

            if (gameObject.CompareTag("SupportZona"))
                GetComponent<BuySupport>().CheckCost();
            _warbutton.SetActive(false);
           
            _image.color = new Color32(26,255,0,255); 

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !SpawnEnemyManager.Instance.isWave())
        {
            if (gameObject.CompareTag("TutorMarket") && LevelUIManager.Instance.VisualCurrentLevelUI == 2 && PlayerPrefs.GetInt("indexTutor") == 0)
                GameManager.Instance.TutorOn();
            _warbutton.SetActive(true);
            _showButton.SetActive(false);
            _image.color = new Color32(255, 255, 255, 255);
        }

    }

    public void OnWarButton() => _warbutton.SetActive(true);
    public void OffButton() => _showButton.SetActive(false);

    public GameObject GetButton() { return _showButton; }

}
