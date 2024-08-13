using NauxUtils;
using UnityEngine;

public class PlayerHandle : Singleton<PlayerHandle>
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private PlayerCharacter character;

    private void Update()
    {
        if (joystick.Direction == Vector2.zero)
            Idle();
        else
            Move();
    }

    void Idle()
    {
        character.ChangeStateAction(ActionState.Idle);
    }

    void Move()
    {
        character.ChangeStateAction(ActionState.Walk);
        character.MoveToDirection(joystick.Direction);
    }

    public void SpendMoneyToPay(int amount)
    {
        DBController.Instance.MONEY -= amount;
        CurrencyBar.Instance.UpdateMoneyUI(DBController.Instance.MONEY);
    }
}
