using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<Quest> lstQuest = new List<Quest>();

    [ContextMenu(nameof(ActiveQuestRoom))]
    public void ActiveQuestRoom()
    {
        var _count = lstQuest.Count;
        for (int i = 0; i < _count; i++)
        {
            lstQuest[i].ActiveQuest();
        }
    }
}
