using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyHealthUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image hpfill;
    [SerializeField] private Image WhiteFill;
    [SerializeField] private float _speedLerp;
    private float health;
    private float percent;
    private float forpercent;
    private Coroutine _canvasCd;

    private void Start()
    {
     if(gameObject.CompareTag("Creature"))
        canvas.SetActive(false);
    }


    public void SetHealth(float value)
    {
        health = value;
        forpercent = health;
    }

    public void GetDamageUI(float count)
    {

        if (gameObject.CompareTag("Creature"))
        {
            if (_canvasCd != null)
                StopCoroutine(_canvasCd);
            _canvasCd = StartCoroutine(CanvasCD());
        }

        StartCoroutine(ReducHp(count));
    }

    public void TurnOffUiHP()
    {
        canvas.SetActive(false);
        GetComponent<enemyHealthUI>().enabled = false;
    } 

    private IEnumerator ReducHp(float count)
    {
        float speed = 0;
        percent = ((forpercent - (health - count)) / forpercent);
        hpfill.fillAmount -= percent;
        while (WhiteFill.fillAmount != hpfill.fillAmount)
        {
            speed += Time.deltaTime;
            WhiteFill.fillAmount = Mathf.Lerp(WhiteFill.fillAmount, hpfill.fillAmount, speed * _speedLerp);
            if (WhiteFill.fillAmount <= 0)
            {
                if(gameObject.CompareTag("Creature"))
                    TurnOffUiHP();
                break;
            }
            yield return null;
        }
       
    }

    public float GetfillAmout() { return hpfill.fillAmount; }

    public void ResetUIHealth()
    {
        hpfill.fillAmount = 1f;
        WhiteFill.fillAmount = 1f;
        gameObject.SetActive(true);
    }

    private IEnumerator CanvasCD()
    {
        
        canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        canvas.SetActive(false);
    }

}
