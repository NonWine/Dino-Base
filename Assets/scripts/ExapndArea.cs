using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
public class ExapndArea : MonoBehaviour
{
    
    [SerializeField] private Image _FilledImage;
    [SerializeField] private float _interval;
    [SerializeField] private float cost = 1f;
    [SerializeField] private TextMeshProUGUI _costTexts;
    [SerializeField] private bool haveFrame;
    [SerializeField] private float percentModifier;
    [SerializeField] private bool _removeWall;
    [SerializeField] private float _growtime;
    private Coroutine cor;
    private Vector3 _startPoint;
    private float percent;
    private bool canBuy;
    private GameObject _newArea;
    private float forpercent;
    private float timer;
    private int saveId;
    private bool tutor;
    private bool trig;
    private void Start()
    {
        
        Debug.Log(Mathf.RoundToInt(percentModifier));
        _costTexts.text = cost.ToString();
        _startPoint = Vector3.zero;
         forpercent = cost;
        float tcost = cost;
        while(tcost > 9)
        {
            
            tcost = tcost / 10;
            percentModifier *= 10;
        }
        
        percentModifier = cost * (percentModifier / cost);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !SpawnEnemyManager.Instance.isWave())
        {
            
          cor = StartCoroutine(FillZona());
            canBuy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !SpawnEnemyManager.Instance.isWave())
        {
            canBuy = false;
            StopCoroutine(cor);
        }
    }

    private IEnumerator FillZona()
    {
        
        float Tcost = cost;
        float bankCost = cost;
        _FilledImage.fillAmount = percent;
       // if(Bank.Instance.CoinsCount > cost)
        
        while (Tcost > 0)
        {

            if (Bank.Instance.CoinsCount > percentModifier)
            {
                percent = ((forpercent - (Tcost - percentModifier)) / forpercent);
                Tcost -= percentModifier;
                cost -= percentModifier;
                if (Tcost < 0)
                    Tcost = 0;
                _costTexts.SetText(Mathf.RoundToInt(Tcost).ToString());
                _FilledImage.fillAmount = percent;
                Debug.Log(bankCost);
                if (bankCost > 0)
                {
                    Bank.Instance.ReduceCoins(Mathf.RoundToInt(percentModifier));
                    bankCost--;
                }


            }
            else
                StopCoroutine(cor);
            yield return null;
        }
        if(_FilledImage.fillAmount == 1f && !trig)
        {
            trig = true;
            Expand();

        }
           

    }

    private void Expand()
    {
        canBuy = false;
        _newArea.SetActive(true);
        if (_removeWall)
            Basemanager.Instance.RemoveWall();
        if (haveFrame)
            SpawnEnemyManager.Instance.AddZonaSpawn();
        GetComponent<NavMeshObstacle>().enabled = false;
        StartCoroutine(GrowRoot());
    }

    IEnumerator GrowRoot()
    {
        do
        {
            _newArea.transform.localScale = Vector3.Lerp(_startPoint, new Vector3(1f, 1f, 1f), (timer / _growtime) * 2);
            timer += Time.deltaTime;
            yield return null;
        }
        while (timer < _growtime);
        timer = 0f;
        if(_newArea.transform.localScale == new Vector3(1f, 1f, 1f))
        {
            Basemanager.Instance.Save();
            gameObject.SetActive(false);
        }
   
    }

    public void SetBase(GameObject obj) => _newArea = obj;

    public void ResetArea()
    {
        gameObject.SetActive(true);

    }
}

/*For Instantiate Basa
        GameObject obj = Instantiate(_newArea);
        obj.transform.SetParent(Basemanager.Instance.transform);
        obj.transform.position = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.zero;
*/