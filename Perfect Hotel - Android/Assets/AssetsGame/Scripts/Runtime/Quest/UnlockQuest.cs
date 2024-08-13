using UnityEngine;
using UnityEngine.Events;

public class UnlockQuest : Quest
{
    [Header(nameof(CleanQuest))]
    [SerializeField] private InteractionRequest interactionRequest;

    [SerializeField] private UnlockQuestType unlockQuestType;
    [SerializeField] private int priceQuest;
    [SerializeField] private int priceSpendEachTime;
    [SerializeField] private float timeDelaySpend;
    private float timeCheckDelay;

    public UnityAction actionUnlockRoom;

    public override void Init()
    {
        base.Init();
        if (interactionRequest == null)
            interactionRequest = GetComponentInChildren<InteractionRequest>();
    }
        
    public override void ActiveQuest()
    {
        base.ActiveQuest();
        ShowQuest();
    }
    void ShowQuest()
    {
        interactionRequest.SetTitle($"{unlockQuestType}");
        interactionRequest.SetPrice(priceQuest);
        interactionRequest.ShowInteractionRequest();
        gobjInteractionArea.SetActive(true);
    }

    void HideQuest()
    {
        interactionRequest.HideInteractionRequest();
        gobjInteractionArea.SetActive(false);
    }

    public override bool CheckCompletedProcess()
    {
        return priceQuest <= 0;
    }
    public override void QuestInteraction()
    {
        var _checkReady = CheckQuestReady();
        if (!_checkReady) return;

        if (DBController.Instance.MONEY >= priceSpendEachTime)
        {
            timeCheckDelay -= Time.deltaTime;
            if (timeCheckDelay > 0) return;
            timeCheckDelay = timeDelaySpend;

            PlayerHandle.Instance.SpendMoneyToPay(priceSpendEachTime);
            priceQuest -= priceSpendEachTime;
            interactionRequest.SetPrice(priceQuest);
        }

        var _check = CheckCompletedProcess();
        if (!_check) return;

        SetQuestState(QuestState.QuestCompleted);
        HideQuest();
        RewardQuest();
    }

    public override void RewardQuest()
    {
        actionUnlockRoom?.Invoke();
    }

    [ContextMenu(nameof(AddInteractionRequest))]
    public void AddInteractionRequest()
    {
        interactionRequest = GetComponentInChildren<InteractionRequest>();
    }
}

public enum UnlockQuestType
{
    Bed = 0,
}
