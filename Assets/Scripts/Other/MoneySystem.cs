using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private Text countMoneyText;
    private int currentMoney = 0;

    void Start()
    {
        countMoneyText.text = currentMoney.ToString();
    }
    public void AddMoney(int count)
    {
        if (count > 0)
        {
            currentMoney += count;
        }
        MoneyTextUpdate();
    }
    private void MoneyTextUpdate()
    {
        countMoneyText.text = currentMoney.ToString();
    }
    
}
