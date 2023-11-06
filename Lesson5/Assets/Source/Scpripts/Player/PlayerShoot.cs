using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int Counter;
    [SerializeField] private Ball _ball;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _delay;
    [SerializeField] private bool _canShoot;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _count;

    private void Start()
    {
        _delay = 0.5f;
        _count = 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canShoot && _count !=0)
        {
            StartCoroutine(ShootTick());
        }  
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }


    private void CreateBall()
    {
        Ball ballCreated = Instantiate(_ball, _spawnPoint.position, Quaternion.identity).GetComponent<Ball>();
        ballCreated.Fly(_spawnPoint.transform.forward, 50);
        StartCoroutine(Delay());
    }

    private IEnumerator ShootTick()
    {
        yield return new WaitForSeconds(_shootDelay);
        CreateBall();
        _count--;
        _canShoot = false;
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);
        _canShoot = true;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _count = 10;
    }
}
