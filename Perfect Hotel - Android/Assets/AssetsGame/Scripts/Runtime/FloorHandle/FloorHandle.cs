using NauxUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHandle : Singleton<FloorHandle>
{
    [SerializeField] private List<Room> lstRooms = new List<Room>();

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

            if (_lstRoomState[i] ||_showedQuestUnlock) continue;
            _showedQuestUnlock = true;
            lstRooms[i].ActiveQuestUnlock(true);
        }
    }

    public void UpdateUnlockRoomQuest(int idRoom)
    {
        var _floorData = DBController.Instance.FLOOR_DATA;
        _floorData.lstRoomState[idRoom] = true;
        DBController.Instance.FLOOR_DATA = _floorData;

        Init();

    }
}

[Serializable]
public class FloorData
{
    public List<bool> lstRoomState = new List<bool>();
}