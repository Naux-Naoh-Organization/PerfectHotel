using NauxUtils;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyBar : Singleton<CurrencyBar>
{
    [SerializeField] private TextMeshProUGUI tmpMoney;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => DBController.Instance != null);
        UpdateMoneyUI(DBController.Instance.MONEY);
    }
    public void UpdateMoneyUI(int value)
    {
        tmpMoney.text = $"{value}";
    }
}
