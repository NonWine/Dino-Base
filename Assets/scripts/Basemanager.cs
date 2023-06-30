using UnityEngine;
using UnityEngine.AI;

public class Basemanager : MonoBehaviour
{
    public static Basemanager Instance { get; private set; }
    [SerializeField] private GameObject[] _bases;
    [SerializeField] private ExapndArea[] _updatePlace;
    [SerializeField] private Transform _spawnPlace;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _wallToBasies;
    private int idWall;
    private int saveId;
    private void Awake()
    {
        Instance = this;
        
        saveId = PlayerPrefs.GetInt("saveId");
        idWall = PlayerPrefs.GetInt("idWall");
        Debug.Log(saveId);
        for (int i = 0; i < saveId; i++)
        {
            _bases[i].SetActive(true);
            _updatePlace[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < idWall; i++)
        {
            _wallToBasies[i].SetActive(false);
        }
        for (int i = saveId ; i < _updatePlace.Length; i++)
        {
            _updatePlace[i].SetBase(_bases[i]);
        }
    }

    public void SetObjctPlayer(GameObject obj) => _player = obj; 

    private void Start()
    {
        Camera.main.transform.position = _player.transform.position;
        Camera.main.transform.GetComponent<CameraFollowing>().GetPlayer(_player.transform);
        Debug.Log(_spawnPlace.position);
        _player.GetComponent<NavMeshAgent>().enabled = false;
        _player.transform.SetParent(transform.GetChild(0));
        _player.transform.localPosition = _spawnPlace.localPosition;
        _player.GetComponent<NavMeshAgent>().enabled = true;
    }
    
    public void RemoveWall()
    {
        _wallToBasies[idWall].SetActive(false);
        idWall++;
        PlayerPrefs.SetInt("idWall", idWall);
    }

    public void Save()
    {
        saveId++;
        PlayerPrefs.SetInt("saveId", saveId);

    }

    public void ReserSaveId()
    {
        saveId = 0;
        idWall = 0;
        PlayerPrefs.SetInt("idWall", idWall);
        PlayerPrefs.SetInt("saveId", saveId);
    }
}
