using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPipes : MonoBehaviour
{
    [SerializeField]
    private GameObject _pipeHolde;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
        
        
        
    }
    
    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(1);
        Vector3 temp = _pipeHolde.transform.position;
        temp.y = UnityEngine.Random.Range(-2f, 2f);
        Instantiate(_pipeHolde, temp, Quaternion.identity);
        StartCoroutine(Spawner());
    }

    private void Update()
    {
        if (BirdController.instance.gameOver != null)
        {
            if (BirdController.instance.gameOver == 1f) 
            {
                Destroy(GetComponent<SpawnerPipes>());
            }
        }
    }
}
