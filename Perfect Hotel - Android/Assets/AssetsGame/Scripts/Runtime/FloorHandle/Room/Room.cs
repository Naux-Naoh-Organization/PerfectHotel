using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private RoomState roomState;
    [SerializeField] private GameObject gobjBlockWall;
    [SerializeField] private GameObject gobjRoomObjects;
    [SerializeField] private UnlockQuest unlockQuest;
    [SerializeField] private List<Quest> lstQuestOfRoom = new List<Quest>();
    public GameObject destination;
    private int idRoom;
    private BotCharacter botCharacter;

    private void Start()
    {
        unlockQuest.actionUnlockRoom += ActionUnlockFromQuest;
    }
    void ActionUnlockFromQuest()
    {
        SetStateRoom(RoomState.Unlock);
        FloorHandle.Instance.UpdateUnlockRoomQuest(idRoom);
    }

    public void SetIdRoom(int id)
    {
        idRoom = id;
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
        gobjRoomObjects.SetActive(true);
    }
    public void ActiveQuestUnlock(bool status)
    {
        unlockQuest.gameObject.SetActive(status);
        unlockQuest.ActiveQuest();
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
            lstQuestOfRoom[i].ActiveQuest();
        }
    }
}
public enum RoomState
{
    Lock = 0,
    Unlock = 1,
}
