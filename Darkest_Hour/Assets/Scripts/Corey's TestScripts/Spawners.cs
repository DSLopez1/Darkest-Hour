using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField] GameObject _objectToSpawn;
    [SerializeField] int _numToSpawn;
    [SerializeField] int _spawnTimer;
    [SerializeField] Transform[] _spawnPos;

    int _spawnCount;
    bool _isSpawning;
    bool _startSpawning;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_startSpawning && !_isSpawning && _spawnCount < _numToSpawn)
        {
            StartCoroutine(spawn());
        }
    }

    IEnumerator spawn()
    {
        _isSpawning = true;

        // Picks random spawn location
        int arrayPos = Random.Range(0, _spawnPos.Length);
        // Creates object
        Instantiate(_objectToSpawn, _spawnPos[arrayPos].position, _spawnPos[arrayPos].rotation);
        // Increase count
        _spawnCount++;
        yield return new WaitForSeconds(_spawnTimer);
        _isSpawning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _startSpawning = true;
        }
    }
}
