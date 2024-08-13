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
        ChangeStateAction(ActionState.Walk);
        navMeshAgent.SetDestination(destination);
    }


    public void MoveToDesCommand(Vector3 destination)
    {
        StartCoroutine(MoveToDesAction(destination));
    }
    IEnumerator MoveToDesAction(Vector3 destination)
    {
        AddDestination(destination);

        yield return new WaitUntil(() => navMeshAgent.pathPending == false && navMeshAgent.remainingDistance <= 0.5f);

         yield return new WaitForSeconds(0.3f); // best time
        ChangeStateAction(ActionState.Idle);
    }


    public void RentRoomCommand(Vector3 destination, Vector3 posDoor)
    {
        StartCoroutine(RentRoomAction(destination, posDoor));
    }
    IEnumerator RentRoomAction(Vector3 destination, Vector3 posDoor)
    {
        AddDestination(destination);

        yield return new WaitUntil(() => navMeshAgent.pathPending == false && navMeshAgent.remainingDistance <= 0.5f);
        yield return new WaitForSeconds(0.4f); 
        ChangeStateAction(ActionState.Idle);
        ChangeStateAction(ActionState.Idle); //anmim sleep?

        yield return new WaitForSeconds(2);
        actionCheckOut?.Invoke();
        AddDestination(posDoor);

        yield return new WaitUntil(() => navMeshAgent.pathPending == false && navMeshAgent.remainingDistance <= 0.5f);
        Destroy(gameObject);
    }
}