using System;
using System.Collections;
using UnityEngine;

public class DropItem : MonoBehaviour, IDroppable, ICollectable
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider colliderItem;
    [SerializeField] private MoneyPlace moneyPlace;
    [SerializeField] private int amount;
    public int Amount => amount;
    private bool isPicked;
    //private Vector3 posFoward;

    static int actionFly = Animator.StringToHash("PickupMoney");

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
        if (isPicked || charact == null) return;

        isPicked = true;
        charact.CollectedMoneyItem(amount);
        if (moneyPlace.area != null)
            moneyPlace.area.ResetPlace(moneyPlace.idFloor, moneyPlace.idPlace);

        transform.SetParent(charact.transform);
        animator.Play(actionFly);

        StartCoroutine(WaitToDestroy());
    }

    //private void Start()
    //{
    //    var _rand = UnityEngine.Random.Range(0,2);
        
    //    posFoward = _rand == 0 ? Vector3.back : Vector3.forward;
    //}
    void FixedUpdate()
    {
        if (!isPicked) return;
        //transform.forward = posFoward;
        transform.forward = Vector3.back;
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1);
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
