using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private CharacterController characterController;
    public float speedMovement;

    public void MoveToDirection(Vector2 direction)
    {
        var direct3 = new Vector3(direction.x, 0, direction.y);
        direct3.y = 0;
        characterController.Move(direct3 * Time.deltaTime * speedMovement);
        characterController.transform.forward = direct3;
    }

    public void CollectedMoneyItem(int amount)
    {
        DBController.Instance.MONEY += amount;
        CurrencyBar.Instance.UpdateMoneyUI(DBController.Instance.MONEY);
    }
}
