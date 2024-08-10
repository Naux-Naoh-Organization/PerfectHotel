using UnityEngine;
using UnityEngine.UI;

public class InteractionWheel : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Image imgProcess;
    [SerializeField] private GameObject gobjWheel;




    private void OnValidate()
    {
        cam = Camera.main;
        GetComponent<Canvas>().worldCamera = cam;
    }

    [ContextMenu(nameof(LookAtCam))]
    public void LookAtCam()
    {
        if (cam == null) return;

        var direction = cam.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction * -1);
    }

    public void ShowInteractionWheel()
    {
        gobjWheel.SetActive(true);
    }
    public void HideInteractionWheel()
    {
        gobjWheel.SetActive(false);
    }

    public void SetProcess(float value)
    {
        imgProcess.fillAmount = value;
    }


}
