using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIGameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float _spawnRate = 1.0f;

    private int _score;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        scoreText.text = "Score: " + _score;

        InvokeRepeating("SpawnTarget",0,_spawnRate);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnTarget()
    {
        int index = Random.Range(0, targets.Count);
        Instantiate(targets[index]);
    }

    public void UpdateScore(int scoreToAdd) {

        _score += scoreToAdd;
        scoreText.text = "Score: " + _score;

    }

}
