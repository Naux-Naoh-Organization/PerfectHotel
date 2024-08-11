using UnityEngine;

public class ReceptionQuest : Quest
{
    [SerializeField] private DropArea dropArea;

    public override void RewardQuest()
    {
        base.RewardQuest();

        dropArea.FindPosCanSpawn(out var canSpawn, out var posSpawn, out var idFloor, out var idPlace);
        if (canSpawn)
        {
            var gobj = SpawnHandle.Instance.SpawnObj(SpawnID.Money, posSpawn);
            var money = gobj.GetComponent<DropItem>();
            money.SetMoneyPlace(dropArea, idFloor, idPlace);
            money.SetAmountItem(10);
        }


        ActiveQuest(); // Delete later, add bot and logic more
    }
}
