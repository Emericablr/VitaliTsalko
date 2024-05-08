using UnityEngine;

public class AddScore : MonoBehaviour
{
    Health health; 
    public int MinAmount = 1; 
    public int MaxAmount = 3; 

    private void Start()
    {
        health = GetComponent<Health>();

        health.onDeath.AddListener(Add);
    }

    void Add()
    {
        ScoreController.instance.AddScore(Random.Range(MinAmount, MaxAmount));
    }
}
