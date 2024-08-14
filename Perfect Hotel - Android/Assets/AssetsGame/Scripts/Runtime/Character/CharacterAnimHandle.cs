using UnityEngine;

public class CharacterAnimHandle : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Transform model;
    private ActionState actionAnim = ActionState.Idle;


    static int idleAction = Animator.StringToHash("Idle");
    static int walkAction = Animator.StringToHash("Walk");
    static int sleepAction = Animator.StringToHash("Sleep");
    //static int runAction = Animator.StringToHash("Run");
    //static int sadAction = Animator.StringToHash("Sad");
    //static int winAction = Animator.StringToHash("Win");
    //static int songJumpAction = Animator.StringToHash("Song Jump");


    private void FixedUpdate()
    {
        if (actionAnim == ActionState.Sleep)
            model.forward = Vector3.back;
        else
            model.localRotation = Quaternion.identity;
    }
    public void RunAnim(ActionState action)
    {
        actionAnim = action;

        switch (actionAnim)
        {
            case ActionState.Idle:
            default:
                animator.Play(idleAction);
                break;
            case ActionState.Walk:
                animator.Play(walkAction);
                break;
            case ActionState.Sleep:
                animator.Play(sleepAction);
                break;
                //case ActionState.Run:
                //    animator.Play(runAction);
                //    break;
                //case ActionState.Sad:
                //    animator.Play(sadAction);
                //    break;
                //case ActionState.Win:
                //    animator.Play(winAction);
                //    break;
                //case ActionState.SongJump:
                //    animator.Play(songJumpAction);
                //    break;
        }
    }

}

