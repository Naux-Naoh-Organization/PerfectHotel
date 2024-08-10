using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private InteractionArea interactionArea;
    [SerializeField] private InteractionWheel interactionWheel;

    [SerializeField] private float timeQuest;

    private GameObject gobjArea;

    private float timeInteract;
    private bool questCompleted;

    void Start()
    {
        if (interactionArea == null)
            interactionArea = GetComponentInChildren<InteractionArea>();

        if (interactionWheel == null)
            interactionWheel = GetComponentInChildren<InteractionWheel>();

        gobjArea = interactionArea.gameObject;
        interactionArea.interact += LoadingProcess;
        interactionWheel.LookAtCam();
    }

    void ResetQuest()
    {
        timeInteract = 0;
        questCompleted = false;
    }
    void ResetProcessWheel()
    {
        interactionWheel.SetProcess(0);
    }

    protected void ShowQuest()
    {
        ResetProcessWheel();
        interactionWheel.ShowInteractionWheel();
        gobjArea.SetActive(true);
        ResetQuest();
    }

    void HideQuest()
    {
        interactionWheel.HideInteractionWheel();
        gobjArea.SetActive(false);
    }



    void LoadingProcess()
    {
        if (questCompleted) return;

        timeInteract += Time.deltaTime;
        var value = timeInteract / timeQuest;
        interactionWheel.SetProcess(value);

        var check = CheckQuestDone();
        if (check) DoneQuest();
    }

    bool CheckQuestDone()
    {
        return timeInteract >= timeQuest;
    }

    public virtual void DoneQuest()
    {
        questCompleted = true;
        HideQuest();
    }

}
