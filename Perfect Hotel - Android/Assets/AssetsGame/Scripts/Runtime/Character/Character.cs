using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{

    [SerializeField] private CharacterMoveHandle moveHandle;
    [SerializeField] private CharacterAnimHandle animHandle;

    private ActionState actionState = ActionState.Idle;
    public bool isPlayer;



    public void ChangeStateAction(ActionState action)
    {
        actionState = action;
        animHandle.RunAnim(actionState);
    }


    public void CharacterMove(Vector2 direct)
    {
        moveHandle.MoveToDirection(direct);
    }

    public void CollectedMoneyItem(int amount)
    {
        DBController.Instance.MONEY += amount;
        CurrencyBar.Instance.UpdateMoneyUI(DBController.Instance.MONEY);
    }

}
public enum ActionState
{
    Idle = 0,
    Walk = 1,
    Run = 2,
    Sad = 3,
    Win = 4,
    SongJump = 5,
}
