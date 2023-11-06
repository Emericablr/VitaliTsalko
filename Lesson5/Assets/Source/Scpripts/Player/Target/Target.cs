using System.Collections;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private float _shootDelay;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
            _count++;
            _counter.text = _count.ToString();
        Destroy(collision.gameObject);
    }
   
}
