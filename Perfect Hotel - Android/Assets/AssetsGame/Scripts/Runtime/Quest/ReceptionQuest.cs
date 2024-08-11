using UnityEngine;

public class ReceptionQuest : Quest
{
    [SerializeField] private DropArea dropArea;

    public override void DoneQuest()
    {
        base.DoneQuest();

        dropArea.FindPosCanSpawn(out var canSpawn, out var posSpawn, out var idFloor, out var idPlace);
        if (canSpawn)
        {
            var gobj = SpawnHandle.Instance.SpawnObj(SpawnID.Money, posSpawn);
            var money = gobj.GetComponent<DropItem>();
            money.SetMoneyPlace(dropArea, idFloor, idPlace);
            money.SetAmountItem(10);
        }


        ShowQuest();
    }
}
