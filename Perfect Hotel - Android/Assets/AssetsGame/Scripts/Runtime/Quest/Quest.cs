using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private InteractionArea interactionArea;
    [SerializeField] private InteractionWheel interactionWheel;

    [SerializeField] private float timeQuest;
    [SerializeField] private QuestState questState;

    private GameObject gobjArea;
    private float timeInteract;

    void Start()
    {
        if (interactionArea == null)
            interactionArea = GetComponentInChildren<InteractionArea>();

        if (interactionWheel == null)
            interactionWheel = GetComponentInChildren<InteractionWheel>();

        gobjArea = interactionArea.gameObject;
        interactionArea.interact += QuestInteraction;
    }

    public void ActiveQuest()
    {
        ShowQuest();
        SetQuestState(QuestState.QuestReady);
    }
    void SetQuestState(QuestState state)
    {
        questState = state;
    }
    void ShowQuest()
    {
        ResetInteractionWheel();
        interactionWheel.ShowInteractionWheel();
        gobjArea.SetActive(true);
    }
    void ResetInteractionWheel()
    {
        interactionWheel.SetProcess(0);
        timeInteract = 0;
        interactionWheel.LookAtCam();
    }


    void QuestInteraction()
    {
        if (questState != QuestState.QuestReady) return;
        LoadingWheel();

        var check = CheckQuestProcessing();
        if (!check) return;

        SetQuestState(QuestState.QuestCompleted);
        HideQuest();
        RewardQuest();
    }

    void LoadingWheel()
    {
        timeInteract += Time.deltaTime;
        var value = timeInteract / timeQuest;
        interactionWheel.SetProcess(value);
    }

    bool CheckQuestProcessing()
    {
        return timeInteract >= timeQuest;
    }
    void HideQuest()
    {
        interactionWheel.HideInteractionWheel();
        gobjArea.SetActive(false);
    }

    public virtual void RewardQuest()
    {
    }

}

public enum QuestState
{
    None = 0,
    QuestReady = 1,
    QuestCompleted = 2,
}