using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{
    public Pipe pipeRef;
    public int poolSize;
    public float spawnRate; 

    private float spawnTimer;
    private float targetAspect;
    private Pipe[] pipes;
    private GameManager game; 

    private void Awake() 
    {
        Configure();
   
        game = GameManager.Instance;
    }

    private void OnEnable() 
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    private void OnDisable() 
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    private void OnGameOverConfirmed() 
    {
        for (int i = 0; i < pipes.Length; i++) 
        {
            pipes[i].Despawn();
        }
    }

    private void Update() 
    {
        if (GameManager.Instance && GameManager.Instance.GameOver) 
        {
            return;
        }

        spawnTimer += Time.deltaTime;
        
        if (spawnTimer > spawnRate) 
        {
            Spawn();
            spawnTimer = 0;
        }
    }

    private void Configure() 
    {
        pipes = new Pipe[poolSize];

        for (int i = 0; i < poolSize; i++) 
        {
            Pipe pipe = Instantiate(pipeRef);
            Transform t = pipe.transform;
            t.SetParent(transform);
            t.position = Vector3.one * 1000;
            pipes[i] = pipe;
        }
    }

    private void Spawn() 
    {
        for (int i = 0; i < pipes.Length; i++) 
        { 
            if (!pipes[i].gameObject.activeSelf) 
            {
                pipes[i].Spawn();
                return;
            }
        }
    }
}

