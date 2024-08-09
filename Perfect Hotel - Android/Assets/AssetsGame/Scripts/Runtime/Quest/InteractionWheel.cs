using System.Collections;
using System.Collections.Generic;
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
        transform.LookAt(cam.transform.position);
    }

    public void ShowInteractionWheel()
    {
        gobjWheel.SetActive(true);
    }
    public void HideInteractionWheel()
    {
        gobjWheel.SetActive(false);
    }

    public void SetProcess(int value)
    {
        imgProcess.fillAmount = value;
    }


}
