using TMPro;
using UnityEngine;

public class InteractionRequest : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject gobjRequest;
    [SerializeField] private TextMeshProUGUI tmpTitle;
    [SerializeField] private TextMeshProUGUI tmpPrice;

    private void OnValidate()
    {
        cam = Camera.main;
        GetComponent<Canvas>().worldCamera = cam;
    }
    public void SetTitle(string title)
    {
        tmpTitle.text = title;
    }
    public void SetPrice(int value)
    {
        tmpPrice.text = $"{value}";
    }
    public void ShowInteractionRequest()
    {
        gobjRequest.SetActive(true);
    }
    public void HideInteractionRequest()
    {
        gobjRequest.SetActive(false);
    }
}
