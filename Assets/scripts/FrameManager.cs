using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FrameManager : MonoBehaviour
{
    public static FrameManager Instance { get; private set; }

    private List<Frame> _frames = new List<Frame>();
    [SerializeField] private Button _buttonRepair;
    [SerializeField] private TMP_Text value;
    [SerializeField] private GameObject _frame;
    private int cost;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _buttonRepair.onClick.AddListener(delegate { RepairAll(); });
        _buttonRepair.gameObject.SetActive(false);
    }

    public void SetRepairButtonFalse() => _buttonRepair.gameObject.SetActive(false);

    public void SetRepairButtonTrue()
    {
        if (cost > 0)
        {
            value.text = cost.ToString();
            _buttonRepair.gameObject.SetActive(true);
        }
           
    }

    public void AddFrame(Frame frame)
    {
        Debug.Log(frame.name);   
        _frames.Add(frame);
   //     value.text = cost.ToString();
    }

    public void RepairAll()
    {
        if(Bank.Instance.CoinsCount >= cost)
        {
            foreach (var item in _frames)
            {
                item.Repair();
                
            }
            Bank.Instance.SubtractCoins(cost);
            cost = 0;
            _buttonRepair.gameObject.SetActive(false);
        }
    }

    public GameObject GetFrame() { return _frame; }

    public Button GetRepairButton() { return _buttonRepair; }

    //public void SetRepairValue()
    //{
    //    Debug.Log(_frames.Count);
    //    foreach (var item in _frames)
    //    {
    //        Debug.Log(cost);
    //        cost += item.CheckStateFrame();
    //    }
    //}

    public void ReduceCost(int x)
    {
        cost -= x;
        if (cost <= 0)
            SetRepairButtonFalse();
        value.text = cost.ToString();
    } 

} 
