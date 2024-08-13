using UnityEngine;

public class ReceptionQuest : Quest
{
    [Header(nameof(ReceptionQuest))]
    [SerializeField] private InteractionWheel interactionWheel;
    [SerializeField] private float timeQuest;
    [SerializeField] private DropArea dropArea;
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
        var value = timeInteract / timeQuest;
        interactionWheel.SetProcess(value);
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

        var _checkRoom = FloorHandle.Instance.HasRoomValid();
        if (!_checkRoom) return;
        SetQuestState(QuestState.QuestRunning);

    }

    private void FixedUpdate()
    {
        var _checkRunning = CheckQuestState(QuestState.QuestRunning);
        if (!_checkRunning) return;


        LoadingWheel();
        var _check = CheckCompletedProcess();
        if (!_check) return;

        SetQuestState(QuestState.QuestCompleted);
        HideQuest();
        RewardQuest();
    }



    public override void RewardQuest()
    {
        dropArea.FindPosCanSpawn(out var canSpawn, out var posSpawn, out var idFloor, out var idPlace);
        
        if (canSpawn)
        {
            var _gobj = SpawnHandle.Instance.SpawnObj(SpawnID.Money, posSpawn);
            var _money = _gobj.GetComponent<DropItem>();
            _money.SetMoneyPlace(dropArea, idFloor, idPlace);
            _money.SetAmountItem(10);
        }

        FloorHandle.Instance.RequiredRoom();
        ActiveQuest();
    }

    [ContextMenu(nameof(AddInteractionWheel))]
    public void AddInteractionWheel()
    {
        interactionWheel = GetComponentInChildren<InteractionWheel>();
    }
}
