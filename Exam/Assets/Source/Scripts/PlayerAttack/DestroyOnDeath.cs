using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    Health health; 

    private void Start()
    {
        health = GetComponent<Health>();

        health.onDeath.AddListener(() => Destroy(gameObject));
    }
}
