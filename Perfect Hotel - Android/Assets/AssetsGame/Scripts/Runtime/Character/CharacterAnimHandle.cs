using UnityEngine;

public class CharacterAnimHandle : MonoBehaviour
{

    [SerializeField] private Animator animator;

    static int idleAction = Animator.StringToHash("Idle");
    static int walkAction = Animator.StringToHash("Walk");
    static int runAction = Animator.StringToHash("Run");
    static int sadAction = Animator.StringToHash("Sad");
    static int winAction = Animator.StringToHash("Win");
    static int songJumpAction = Animator.StringToHash("Song Jump");

    public void RunAnim(ActionState action)
    {
        switch (action)
        {
            case ActionState.Idle:
            default:
                animator.Play(idleAction);
                break;
            case ActionState.Walk:
                animator.Play(walkAction);
                break;
            case ActionState.Run:
                animator.Play(runAction);
                break;
            case ActionState.Sad:
                animator.Play(sadAction);
                break;
            case ActionState.Win:
                animator.Play(winAction);
                break;
            case ActionState.SongJump:
                animator.Play(songJumpAction);
                break;
        }
    }

}

