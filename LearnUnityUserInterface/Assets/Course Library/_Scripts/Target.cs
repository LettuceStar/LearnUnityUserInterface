using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _targentRb;

    private float _minSpeed = 12;
    private float _maxSpeed = 16;
    private float _maxTorque = 10;
    private float _xRange = 4;
    private float _ySpawnPos = -2;

    private UIGameManager _gameMgr;

    // Start is called before the first frame update
    void Start()
    {
        _gameMgr = GameObject.Find("Scripts").GetComponent<UIGameManager>();
        _targentRb = GetComponent<Rigidbody>();
        _targentRb.AddForce(RandomForce(), ForceMode.Impulse);
        _targentRb.AddTorque(RandomTorque(), RandomTorque()
            , RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-_maxTorque, -_maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        _gameMgr.UpdateScore(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
