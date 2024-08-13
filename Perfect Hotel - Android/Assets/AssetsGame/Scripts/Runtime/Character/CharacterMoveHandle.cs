using UnityEngine;

public class CharacterMoveHandle : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    public float speedMovement;

    //for player
    public void MoveToDirection(Vector2 direction)
    {
        var direct3 = new Vector3(direction.x, 0, direction.y);
        direct3.y = 0;
        ///normalized? 
        characterController.Move(direct3 * Time.deltaTime * speedMovement);
        characterController.transform.forward = direct3;
    }
}
