using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BotCharacter : Character
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private List<Mesh> lstMeshSkin = new List<Mesh>();
    public UnityAction actionCheckOut;


    private void Awake()
    {
        var _rand = Random.Range(0, lstMeshSkin.Count);
        skinnedMeshRenderer.sharedMesh = lstMeshSkin[_rand];
    }
    void AddDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }


    public void MoveToDesCommand(Vector3 destination)
    {
        StartCoroutine(MoveToDesAction(destination));
    }
    IEnumerator MoveToDesAction(Vector3 destination)
    {
        AddDestination(destination);
        ChangeStateAction(ActionState.Walk);
        yield return new WaitUntil(() => navMeshAgent.pathPending == false && navMeshAgent.remainingDistance <= 0);

        yield return new WaitForSeconds(0.3f); // best time
        ChangeStateAction(ActionState.Idle);
    }


    public void RentRoomCommand(Vector3 destination, Vector3 posBed, Vector3 posDoor)
    {
        StartCoroutine(RentRoomAction(destination, posBed, posDoor));
    }
    IEnumerator RentRoomAction(Vector3 destination, Vector3 posBed, Vector3 posDoor)
    {
        AddDestination(destination);
        ChangeStateAction(ActionState.Walk);
        yield return new WaitUntil(() => navMeshAgent.pathPending == false && navMeshAgent.remainingDistance <= 0);

        yield return new WaitForSeconds(0.1f);
        ChangeStateAction(ActionState.Sleep);

        yield return new WaitForSeconds(0.2f);
        AddDestination(posBed);

        yield return new WaitForSeconds(4);
        actionCheckOut?.Invoke();
        AddDestination(posDoor);
        ChangeStateAction(ActionState.Walk);

        var _rand = Random.Range(0, 2);
        if(_rand == 0)
        {
            var _posBot = transform.position;
            _posBot.y += 1;
            var _gobj = SpawnHandle.Instance.SpawnObj(SpawnID.Money, _posBot);
            var _moneyDrop = _gobj.GetComponent<DropItem>();
            _moneyDrop.SetAmountItem(2);
            _moneyDrop.transform.DOJump(destination, 1, 1, 0.3f).SetEase(Ease.Linear);
        }

        yield return new WaitUntil(() => navMeshAgent.pathPending == false && navMeshAgent.remainingDistance <= 0);
        Destroy(gameObject);
    }
}