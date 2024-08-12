using NauxUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Character character;



    private void Update()
    {
        if (joystick.Direction == Vector2.zero)
        {
            PlayerIdle();
        }
        else
        {
            PlayerMove();
        }
    }


    void PlayerIdle()
    {
        character.ChangeStateAction(ActionState.Idle);
    }

    void PlayerMove()
    {
        character.ChangeStateAction(ActionState.Walk);
        character.CharacterMove(joystick.Direction);

    }

    public void SpendMoneyToPay(int amount)
    {
        DBController.Instance.MONEY -= amount;
        CurrencyBar.Instance.UpdateMoneyUI(DBController.Instance.MONEY);
    }
}
