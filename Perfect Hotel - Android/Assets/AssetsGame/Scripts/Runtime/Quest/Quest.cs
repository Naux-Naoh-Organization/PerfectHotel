using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private QuestState questState;
    [SerializeField] private InteractionArea interactionArea;
    [SerializeField] protected GameObject gobjInteractionArea;

    void Start()
    {
        Init();
    }
    protected void SetQuestState(QuestState state)
    {
        questState = state;
    }
    public virtual void Init()
    {
        if (interactionArea == null)
            interactionArea = GetComponentInChildren<InteractionArea>();
        interactionArea.interact += QuestInteraction;
    }
    public virtual void ActiveQuest()
    {
        SetQuestState(QuestState.QuestReady);
    }


    public virtual void QuestInteraction()
    {
    }

    public bool CheckQuestState(QuestState state)
    {
        return questState == state;

    }
    public virtual bool CheckCompletedProcess()
    {
        return false;
    }

    public virtual void RewardQuest()
    {
    }
    [ContextMenu("AddInteractionArea")]
    public void AddInteractionArea()
    {
        gobjInteractionArea = interactionArea.gameObject;
    }
}

public enum QuestState
{
    QuestReady = 1,
    QuestCompleted = 2,
    QuestRunning = 3,
}