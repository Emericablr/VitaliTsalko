using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] int Cost; 
    [SerializeField] TextMeshProUGUI costText; 
    Button button;

    public UnityEvent onBought; 

    private void Start()
    {
        button = GetComponent<Button>(); 
        costText.text = Cost.ToString(); 
        button.onClick.AddListener(TryBuy); 
    }

    public void TryBuy()
    {
        if (ScoreController.instance.score >= Cost) 
        {
            ScoreController.instance.score -= Cost;
            Cost *= 2; 
            costText.text = Cost.ToString(); 
            onBought.Invoke(); 
        }
        else 
        {
            Debug.Log("Недостаточно дерева!"); 
        }
    }
}
