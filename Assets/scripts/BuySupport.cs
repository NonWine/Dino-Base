using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BuySupport : MonoBehaviour
{
    [SerializeField] private GameObject _buyObject;
    [SerializeField] private TMP_Text _textCost;
    [SerializeField] private int _cost;
    [SerializeField] private Button _supportBuyButton;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private bool sol1, sol2;
    private int saveBuy, saveBuy2, saveBuy3;

    private void Awake()
    {
        saveBuy = PlayerPrefs.GetInt("saveBuy");
        saveBuy2 = PlayerPrefs.GetInt("saveBuy2");
        saveBuy3 = PlayerPrefs.GetInt("saveBuy3");
    }
    private void Start()
    {
        _textCost.text = _cost.ToString();
        _supportBuyButton.onClick.AddListener(delegate { Buy(); });
        saveBuy = PlayerPrefs.GetInt("saveBuy");
        Debug.Log(saveBuy);
        if(saveBuy == 1 && _buyObject.CompareTag("Solider1"))
        {
            GetComponent<ShowShopButton>().OffButton();
            _buyObject.SetActive(true);
            gameObject.SetActive(false);
        }
        if (saveBuy2 == 1 && _buyObject.CompareTag("Solider2"))
        {
            GetComponent<ShowShopButton>().OffButton();
            _buyObject.SetActive(true);
            gameObject.SetActive(false);
        }
        if (saveBuy3 == 1 && _buyObject.CompareTag("Solider3"))
        {
            GetComponent<ShowShopButton>().OffButton();
            _buyObject.SetActive(true);
            gameObject.SetActive(false);
        }

    }

    public void Buy()
    {
        if(Bank.Instance.CoinsCount >= _cost)
        {
            Bank.Instance.SubtractCoins(_cost);
            _buyObject.SetActive(true);
            GetComponent<ShowShopButton>().OffButton();

            if (_buyObject.CompareTag("Solider1"))
                PlayerPrefs.SetInt("saveBuy", 1);
            else if (_buyObject.CompareTag("Solider2"))
                PlayerPrefs.SetInt("saveBuy2", 1);
            else if (_buyObject.CompareTag("Solider3"))
                PlayerPrefs.SetInt("saveBuy3", 1);

            GetComponent<ShowShopButton>().OnWarButton();
                gameObject.SetActive(false);
        }

    }
    public void CheckCost()
    {
        if (Bank.Instance.CoinsCount < _cost)
            _buttonImage.color = new Color32(106, 106, 106, 255);
        else
            _buttonImage.color = new Color32(125, 197, 11, 255);
    }

}
