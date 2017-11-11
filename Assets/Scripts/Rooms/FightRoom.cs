using UnityEngine;

public class FightRoom : MonoBehaviour
{
    private DoorwayBars[] _bars;

    [SerializeField]
    private GameObject _waveOne;
    [SerializeField]
    private GameObject _waveTwo;
    [SerializeField]
    private GameObject _chestToReveal;

    private FightRoomState _state = FightRoomState.Waiting;

    private void Awake()
    {
        GetComponent<Room>().PlayerEntered += StartFight;
        _bars = GetComponentsInChildren<DoorwayBars>();
    }


    private void Update()
    {
        switch(_state)
        {
            case FightRoomState.WaveOne:
                if (_waveOne.transform.childCount == 0)
                {
                    _state = FightRoomState.WaveTwo;
                    SpawnWaveTwo();
                }
                break;
            case FightRoomState.WaveTwo:
                if (_waveTwo.transform.childCount == 0)
                {
                    _state = FightRoomState.Victory;
                    RevealChest();
                    UnlockRoom();
                }
                break;
        }
    }

    private void StartFight(Room sender, PlayerController player)
    {
        if (_state == FightRoomState.Waiting)
        {
            _state = FightRoomState.WaveOne;
            SpawnWaveOne();
            LockDownRoom();
        }
    }

    private void SpawnWaveOne()
    {
        _waveOne.SetActive(true);
    }
    private void SpawnWaveTwo()
    {
        _waveTwo.SetActive(true);
    }
    private void RevealChest()
    {
        _chestToReveal.SetActive(true);
    }

    private void LockDownRoom()
    {
        foreach(var bars in _bars)
        {
            bars.Close();
        }
    }
    private void UnlockRoom()
    {
        foreach (var bars in _bars)
        {
            bars.Open();
        }
    }

    enum FightRoomState
    {
        Waiting,
        WaveOne,
        WaveTwo,
        Victory
    }
}
