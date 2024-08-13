using UnityEngine;

public class CleanQuest : Quest
{

    [Header(nameof(CleanQuest))]
    [SerializeField] private InteractionWheel interactionWheel;
    [SerializeField] private float timeQuest;
    private float timeInteract;
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


    [ContextMenu(nameof(AddInteractionWheel))]
    public void AddInteractionWheel()
    {
        interactionWheel = GetComponentInChildren<InteractionWheel>();
    }
}
