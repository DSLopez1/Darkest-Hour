using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private int _numToSpawn;
    [SerializeField] private int _spawnTimer;
    [SerializeField] private Transform[] _spawnPos;

    private int _spawnCount;
    private bool _isSpawning;
    private bool _startSpawning;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (_startSpawning && !_isSpawning && _spawnCount < _numToSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
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
        if (other.CompareTag("Payload") || other.CompareTag("Player"))
        {
            _startSpawning = true;
        }
    }
}
