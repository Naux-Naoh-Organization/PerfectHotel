using UnityEngine;

public class CleanQuest : Quest
{

    [Header(nameof(CleanQuest))]
    [SerializeField] private InteractionWheel interactionWheel;
    [SerializeField] private float timeQuest;
    [SerializeField] private Animator questAnimator;
    [SerializeField] private AnimQuestState animQuestState;

    private float timeInteract;

    static int idleAction = Animator.StringToHash("Idle");
    static int questAction = Animator.StringToHash("Quest");
    static int cleanAction = Animator.StringToHash("Clean");

    void RunAnim(AnimQuestState action)
    {
        switch (action)
        {
            case AnimQuestState.Idle:
            default:
                questAnimator.Play(idleAction);
                break;
            case AnimQuestState.Quest:
                questAnimator.Play(questAction);
                break;
            case AnimQuestState.Clean:
                questAnimator.Play(cleanAction);
                break;

        }
    }


    public override void Init()
    {
        base.Init();
        if (interactionWheel == null)
            interactionWheel = GetComponentInChildren<InteractionWheel>();
    }
    public override void ActiveQuest()
    {
        base.ActiveQuest();
        ShowQuest();
    }
    void ShowQuest()
    {

        ResetInteractionWheel();
        interactionWheel.ShowInteractionWheel();
        gobjInteractionArea.SetActive(true);
        RunAnim(AnimQuestState.Idle);
    }
    void ResetInteractionWheel()
    {
        interactionWheel.SetProcess(0);
        timeInteract = 0;
        interactionWheel.LookAtCam();
    }

    void LoadingWheel()
    {
        timeInteract += Time.deltaTime;
        var _value = timeInteract / timeQuest;
        interactionWheel.SetProcess(_value);
    }

    public override bool CheckCompletedProcess()
    {
        return timeInteract >= timeQuest;
    }
    void HideQuest()
    {
        interactionWheel.HideInteractionWheel();
        gobjInteractionArea.SetActive(false);
        RunAnim(AnimQuestState.Clean);
    }
    public override void QuestInteraction()
    {
        var _checkReady = CheckQuestState(QuestState.QuestReady);
        if (!_checkReady) return;

        LoadingWheel();
        var _check = CheckCompletedProcess();
        if (!_check) return;

        SetQuestState(QuestState.QuestCompleted);        
        HideQuest();
        RewardQuest();
    }

    public override void RewardQuest()
    {
    }

    public void PlayAnimQuest()
    {
        RunAnim(AnimQuestState.Quest);
    }

    [ContextMenu(nameof(AddInteractionWheel))]
    public void AddInteractionWheel()
    {
        interactionWheel = GetComponentInChildren<InteractionWheel>();
    }
}

public enum AnimQuestState
{
    Idle = 0,
    Quest = 1,
    Clean = 2,
}
