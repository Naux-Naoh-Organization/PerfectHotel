using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionQuest : Quest
{
    [SerializeField] private DropArea dropArea;

    public override void DoneQuest()
    {
        base.DoneQuest();

        dropArea.FindPosCanSpawn(out var canSpawn, out var posSpawn);
        if (canSpawn)
            SpawnHandle.Instance.SpawnObj(SpawnID.Money, posSpawn);

        ShowQuest();
    }
}
