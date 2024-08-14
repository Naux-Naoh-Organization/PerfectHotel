using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeQuest : Quest
{
    [Header(nameof(UpgradeQuest))]
    [SerializeField] private InteractionRequest interactionRequest;

    [SerializeField] private UnlockQuestType unlockQuestType;
    [SerializeField] private int priceQuest;
    [SerializeField] private int priceSpendEachTime;
    [SerializeField] private float timeDelaySpend;
    private float timeDelayPayAnim;

    public UnityAction<int> actionUpgradeRoom;
    private float timeCheckDelay;
    public int upgradeToLevel;

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
        var _checkReady = CheckQuestState(QuestState.QuestReady);
        if (!_checkReady) return;

        if (DBController.Instance.MONEY >= priceSpendEachTime)
        {
            timeCheckDelay -= Time.deltaTime;
            if (timeCheckDelay > 0) return;
            timeCheckDelay = timeDelaySpend;
            SpawnPayMoney();
            PlayerHandle.Instance.PlayerCharacter.SpendMoneyToPay(priceSpendEachTime);
            priceQuest -= priceSpendEachTime;
            interactionRequest.SetPrice(priceQuest);
        }

        var _check = CheckCompletedProcess();
        if (!_check) return;

        SetQuestState(QuestState.QuestCompleted);
        HideQuest();
        RewardQuest();
    }
    void SpawnPayMoney()
    {
        timeDelayPayAnim -= Time.deltaTime;
        if (timeDelayPayAnim > 0) return;

        timeDelayPayAnim = 0.15f;
        var _posPlayer = PlayerHandle.Instance.PlayerCharacter.transform.position;
        _posPlayer.y += 1;
        var _gobj = SpawnHandle.Instance.SpawnObj(SpawnID.MoneyPay, _posPlayer);
        _gobj.transform.DOScale(Vector3.one * 0.7f, 0.2f);
        _gobj.transform.DOJump(transform.position, 1, 1, 0.2f).SetEase(Ease.Linear).OnComplete(() => Destroy(_gobj));
    }

    public override void RewardQuest()
    {
        actionUpgradeRoom?.Invoke(upgradeToLevel);
    }

    [ContextMenu(nameof(AddInteractionRequest))]
    public void AddInteractionRequest()
    {
        interactionRequest = GetComponentInChildren<InteractionRequest>();
    }
}
