using System.Linq.Expressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigitbody;
    [SerializeField] private float _speed;
    [SerializeField] private Mass _mass;
    private void Start()
    {
        _rigitbody = GetComponent<Rigidbody>();
        Debug.Log("My mass" + "=" + _mass.Amount);
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigitbody.velocity = new Vector3(0, 0, _speed);

        }
        if (Input.GetKey(KeyCode.S))
        {
            _rigitbody.velocity = new Vector3(0, 0, -_speed);

        }
        if (Input.GetKey(KeyCode.D))
        {
            _rigitbody.velocity = new Vector3(_speed, 0, 0);

        }
        if (Input.GetKey(KeyCode.A))
        {
            _rigitbody.velocity = new Vector3(-_speed, 0, 0);

        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Mass>())
        {
            if(_mass.Amount > collision.gameObject.GetComponent<Mass>().Amount)
            {
              _mass.Amount += collision.gameObject.GetComponent<Mass>().Amount;
                transform.localScale = new Vector3(_mass.Amount, _mass.Amount, _mass.Amount);
                Destroy(collision.gameObject);
                Debug.Log($"I destroyed it" + " " + "and took his life" + "has now become more powerful " + _mass.Amount);
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("A curse" + " " + "I'll take revenge next time");
            }
        }
        
    }
}