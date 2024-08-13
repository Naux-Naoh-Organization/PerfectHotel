using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterAnimHandle animHandle;
    private ActionState actionState = ActionState.Idle;

    public void ChangeStateAction(ActionState action)
    {
        actionState = action;
        animHandle.RunAnim(actionState);
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
