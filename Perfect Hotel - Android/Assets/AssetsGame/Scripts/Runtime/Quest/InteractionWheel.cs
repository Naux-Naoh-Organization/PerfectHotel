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

    private void Start()
    {
        LookAtCam();
    }

    [ContextMenu(nameof(LookAtCam))]
    public void LookAtCam()
    {
        if (cam == null) return;

        transform.LookAt(cam.transform.position);
        var _newEuler = transform.eulerAngles;
        _newEuler.y = 180;
        transform.eulerAngles = _newEuler;
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
