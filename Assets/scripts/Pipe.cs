using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Vector3 defaultSpawnPos; 
    public YSpawnRange ySpawnRange;
    public float shiftSpeed;

    public void Spawn()
    {
        Vector3 pos = Vector3.zero;
        pos.x = defaultSpawnPos.x;
        pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
        transform.position = pos;

        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    private void Update() 
    {
        transform.position += - Vector3.right * shiftSpeed * Time.deltaTime;
        
        if (transform.position.x < -defaultSpawnPos.x) 
        {
            Despawn();
        }
    }
}
