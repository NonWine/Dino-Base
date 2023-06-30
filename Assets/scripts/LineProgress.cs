using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LineProgress : MonoBehaviour
{
    [SerializeField] private Image _filledLine;
    [SerializeField] private float _speedLerp;
    [SerializeField] private TMP_Text _from;
    [SerializeField] private TMP_Text _to;
    private int countMobs;
    private float percent;
    private float forpercent;
    private float lineFilled;
    public void SetMobs(int value)
    {
        countMobs = value;
        forpercent = countMobs;
    } 

    private IEnumerator FillLine(float count)
    {
        float speed = 0;
        percent = ((forpercent - (countMobs - count)) / forpercent);
        lineFilled += percent;
        while (_filledLine.fillAmount != lineFilled)
        {
            speed += Time.deltaTime;
            _filledLine.fillAmount = Mathf.Lerp(_filledLine.fillAmount, lineFilled, speed * _speedLerp);
            if(_filledLine.fillAmount >= 0.99999f)
            {
                gameObject.SetActive(false);
                LevelUIManager.Instance.gameObject.SetActive(true);
            }
            yield return null;
        }
    }

    public void ChangeLevelWave()
    {
        RectTransform rect;
        lineFilled = 0f;
        _filledLine.fillAmount = 0f;
         _from.text = LevelUIManager.Instance.VisualCurrentLevelUI.ToString();
        _to.text = (LevelUIManager.Instance.VisualCurrentLevelUI + 1).ToString();
        //_from.text = 21.ToString();
        //_to.text = 21.ToString();
        rect = _from.GetComponent<RectTransform>();
        if (System.Convert.ToInt32(_from.text) > 9)
            rect.sizeDelta = new Vector2(57, 35);
        else
            rect.sizeDelta = new Vector2(40, 35);

        rect = _to.GetComponent<RectTransform>();
        if (System.Convert.ToInt32(_to.text) > 9)
            rect.sizeDelta = new Vector2(56, 35);
        else
            rect.sizeDelta = new Vector2(42, 35);

        rect = _from.GetComponent<RectTransform>();
        if (System.Convert.ToInt32(_from.text) > 19)
            rect.sizeDelta = new Vector2(65, 35);

        rect = _to.GetComponent<RectTransform>();
        if (System.Convert.ToInt32(_to.text) > 19)
            rect.sizeDelta = new Vector2(60, 35);


    }

    public void UpdateLineProgress() => StartCoroutine(FillLine(1));

    public void RestartLineProgress()
    {
        StopAllCoroutines();
        lineFilled = 0f;
        _filledLine.fillAmount = 0f;
        gameObject.SetActive(false);
    }

}
