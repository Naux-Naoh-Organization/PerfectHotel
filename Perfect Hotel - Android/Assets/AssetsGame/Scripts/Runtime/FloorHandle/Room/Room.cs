using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private RoomState roomState;
    [SerializeField] private GameObject gobjBlockWall;
    [SerializeField] private GameObject gobjRoomObjects;
    [SerializeField] private UnlockQuest unlockQuest;
    [SerializeField] private UpgradeQuest upgradeQuest;
    [SerializeField] private List<CleanQuest> lstQuestOfRoom = new List<CleanQuest>();

    [SerializeField] private List<GameObject> lstModelLv01 = new List<GameObject>();
    [SerializeField] private List<GameObject> lstModelLv02 = new List<GameObject>();
    public Transform destination;
    public Transform bedDestination;
    private BotCharacter botCharacter;
    private int idRoom;
    public int levelRoom;

    private void Start()
    {
        unlockQuest.actionUnlockRoom += ActionUnlockFromQuest;
        upgradeQuest.actionUpgradeRoom += ActionUpgradeFromQuest;
    }
    void ActionUnlockFromQuest()
    {
        SetStateRoom(RoomState.Unlock);
        FloorHandle.Instance.UpdateUnlockRoomQuest(idRoom);
    }
    void ActionUpgradeFromQuest(int lv)
    {
        var _temp = DBController.Instance.FLOOR_DATA;
        for (int i = 0; i < _temp.lstRoomState.Count; i++)
        {
            if (_temp.lstRoomState[i].roomId != idRoom) continue;

            _temp.lstRoomState[i].level = lv;
        }
        DBController.Instance.FLOOR_DATA = _temp;

        SetLevelRoom(lv);
        FloorHandle.Instance.UpdateUnlockRoomQuest(idRoom);
    }
    public void SetIdRoom(int id)
    {
        idRoom = id;
    }
    public void SetLevelRoom(int lv)
    {
        levelRoom = lv;
        UpdateLevelModelRoom();
    }

    void UpdateLevelModelRoom()
    {
        var _isLevelOne = levelRoom == 1;
        for (int i = 0, _countA = lstModelLv01.Count; i < _countA; i++)
        {
            lstModelLv01[i].SetActive(_isLevelOne);
            lstModelLv02[i].SetActive(!_isLevelOne);
        }
    }
    public void SetCharacterRentRoom(BotCharacter character)
    {
        botCharacter = character;
        botCharacter.actionCheckOut += ActiveAllQuestRoom;
    }

    public void SetStateRoom(RoomState state)
    {
        roomState = state;
        BehaviorRoomState();
    }
    void BehaviorRoomState()
    {
        switch (roomState)
        {
            case RoomState.Lock:
            default:
                LockRoom();
                break;
            case RoomState.Unlock:
                UnlockRoom();
                break;
        }
    }
    void LockRoom()
    {
        gobjBlockWall.SetActive(true);
        gobjRoomObjects.SetActive(false);
    }
    void UnlockRoom()
    {
        gobjBlockWall.SetActive(false);
        gobjRoomObjects.transform.localScale = Vector3.one * 0.8f;
        gobjRoomObjects.SetActive(true);
        gobjRoomObjects.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }
    public void ActiveQuestUnlock(bool status)
    {
        unlockQuest.gameObject.SetActive(status);
        unlockQuest.ActiveQuest();

    }
    public void ActiveQuestUpgrade(bool status)
    {
        upgradeQuest.gameObject.SetActive(status);
        upgradeQuest.ActiveQuest();
    }

    public bool CheckRoomValid()
    {
        return roomState == RoomState.Unlock && botCharacter == null && CheckAllQuestCompleted();
    }

    bool CheckAllQuestCompleted()
    {
        var _count = lstQuestOfRoom.Count;
        for (int i = 0; i < _count; i++)
        {
            var _check = lstQuestOfRoom[i].CheckQuestState(QuestState.QuestCompleted);
            if (_check == false) return false;
        }
        return true;
    }
    public void ActiveAllQuestRoom()
    {
        botCharacter.actionCheckOut -= ActiveAllQuestRoom;
        botCharacter = null;

        var _count = lstQuestOfRoom.Count;
        for (int i = 0; i < _count; i++)
        {
            lstQuestOfRoom[i].PlayAnimQuest();
            lstQuestOfRoom[i].ActiveQuest();
        }
    }


}
public enum RoomState
{
    Lock = 0,
    Unlock = 1,
}
