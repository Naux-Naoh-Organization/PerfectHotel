using UnityEngine;

public class PlayerCharacter : Character
{



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
