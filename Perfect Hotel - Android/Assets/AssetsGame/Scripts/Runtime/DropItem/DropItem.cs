using System;
using UnityEngine;

public class DropItem : MonoBehaviour, IDroppable, ICollectable
{
    [SerializeField] private int amount;
    [SerializeField] private BoxCollider colliderItem;
    [SerializeField] private MoneyPlace moneyPlace;
    public int Amount => amount;


    public void SetMoneyPlace(DropArea inArea, int idFloor, int idPlace)
    {
        moneyPlace.area = inArea;
        moneyPlace.idFloor = idFloor;
        moneyPlace.idPlace = idPlace;
    }


    public void SetAmountItem(int value)
    {
        amount = value;
    }

    public void SetPosItem(Vector3 pos)
    {
        transform.position = pos;
    }

    public void DestroyItem()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<PlayerCharacter>(out var charact);

        if (charact == null) return;

        charact.CollectedMoneyItem(amount);
        if (moneyPlace.area != null)
        {
            moneyPlace.area.ResetPlace(moneyPlace.idFloor, moneyPlace.idPlace);
        }
        Destroy(gameObject);
    }
}
[Serializable]
public class MoneyPlace
{
    public DropArea area;
    public int idFloor;
    public int idPlace;

}
public interface IDroppable
{

}

public interface ICollectable
{

}
