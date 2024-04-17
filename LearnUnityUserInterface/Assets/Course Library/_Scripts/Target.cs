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

    private UIGameManager _uiGameMgr;

    public int targetScore;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        _uiGameMgr = GameObject.Find("Scripts").GetComponent<UIGameManager>();
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
        //Debug.Log(this.gameObject.name);

        //string objName = this.gameObject.name;

        //int score = 0;

        //switch (objName)
        //{
        //    case "Good 01(Clone)":
        //        score = 1;
        //        break;
        //    case "Good 02(Clone)":
        //        score = 2;
        //        break;
        //    case "Good 03(Clone)":
        //        score = 3;
        //        break;
        //    case "Bad 01(Clone)":
        //        score = -5;
        //        break;
        //    default:
        //        Debug.Log("未找到该目标");
        //        break;
        //}

        //_uiGameMgr.UpdateScore(score);

        if (_uiGameMgr.isGameActive == false)
        {
            return;
        }
       
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        if (this.tag == "Bad") {
            _uiGameMgr.GameOver();
        }

        _uiGameMgr.UpdateScore(targetScore);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        _uiGameMgr.LiveCountMinusOne();
        Destroy(gameObject);
    }
}
