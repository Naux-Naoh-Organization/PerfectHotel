using NauxUtils;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FloorHandle : Singleton<FloorHandle>
{
    [SerializeField] private List<Room> lstRooms = new List<Room>();
    [SerializeField] private List<BotCharacter> lstBotWaiting = new List<BotCharacter>();
    [SerializeField] private List<Transform> lstPlaceWaiting = new List<Transform>();
    [SerializeField] private Transform doorLeft;
    [SerializeField] private Transform doorRight;



    IEnumerator Start()
    {
        yield return new WaitUntil(() => DBController.Instance != null);
        Init();
    }

    void Init()
    {
        var _lstRoomState = DBController.Instance.FLOOR_DATA.lstRoomState;
        var _count = _lstRoomState.Count;
        var _showedQuestUnlock = false;
        for (int i = 0; i < _count; i++)
        {
            lstRooms[i].SetIdRoom(i);
            lstRooms[i].SetStateRoom(_lstRoomState[i] ? RoomState.Unlock : RoomState.Lock);

            if (_lstRoomState[i] || _showedQuestUnlock) continue;
            _showedQuestUnlock = true;
            lstRooms[i].ActiveQuestUnlock(true);
        }
    }

    public void UpdateUnlockRoomQuest(int idRoom)
    {
        var _floorData = DBController.Instance.FLOOR_DATA;
        _floorData.lstRoomState[idRoom] = true;
        DBController.Instance.FLOOR_DATA = _floorData;

        var _count = _floorData.lstRoomState.Count;
        for (int i = 0; i < _count; i++)
        {
            if (_floorData.lstRoomState[i]) continue;
            lstRooms[i].ActiveQuestUnlock(true);
            return;
        }
    }


    public bool HasRoomValid()
    {
        var _count = lstRooms.Count;
        for (int i = 0; i < _count; i++)
        {
            var _valid = lstRooms[i].CheckRoomValid();
            if (!_valid) continue;
            return true;
        }
        return false;
    }
    public void RequiredRoom()
    {
        var _count = lstRooms.Count;
        for (int i = 0; i < _count; i++)
        {
            var _valid = lstRooms[i].CheckRoomValid();
            if (!_valid) continue;

            var _podDestination = lstRooms[i].destination.position;
            var _posBed = lstRooms[i].bedDestination.position;
            var _rand = UnityEngine.Random.Range(0, 2);
            var _posDoor = _rand == 0 ? doorLeft.position : doorRight.position;

            lstRooms[i].SetCharacterRentRoom(lstBotWaiting[0]);
            lstBotWaiting[0].RentRoomCommand(_podDestination, _posBed, _posDoor);
            lstBotWaiting.RemoveAt(0);
            StartCoroutine(nameof(UpdateWaitingPlace));
            return;
        }
    }

    IEnumerator UpdateWaitingPlace()
    {
        var _count = lstBotWaiting.Count;
        for (int i = 0; i < _count; i++)
        {
            var _podDestination = lstPlaceWaiting[i].position;
            lstBotWaiting[i].MoveToDesCommand(_podDestination);
        }
        yield return new WaitForSeconds(0.5f);

        var _podSpawn = lstPlaceWaiting[_count].position; // use [_count] cuz 3 is last element in lstPlaceWaiting too
        var _gobj = SpawnHandle.Instance.SpawnObj(SpawnID.BotAI, _podSpawn);
        var _bot = _gobj.GetComponent<BotCharacter>();
        lstBotWaiting.Add(_bot);
    }

}

[Serializable]
public class FloorData
{
    public List<bool> lstRoomState = new List<bool>();
}