    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
  [SerializeField] private int _health = 100;
    [SerializeField] private int _damage = 99;
    [SerializeField] private int _immortals =101;
  

    void Start()
    {
        Debug.Log("No dude,I`ll live");
        Debug.Log(_health - _damage);
        Debug.Log("Activated immortals");
        Debug.Log(1 + _health);
    }
    
}
   

