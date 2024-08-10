using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour, IDroppable, ICollectable
{
    [SerializeField] private int amount;
    [SerializeField] private BoxCollider colliderItem;

    public int Amount => amount;

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
        other.TryGetComponent<Character>(out var charact);
        if (charact != null && charact.isPlayer)
        {
            Debug.Log("get moneyt");
        }
    }


}

public interface IDroppable
{

}

public interface ICollectable
{

}
