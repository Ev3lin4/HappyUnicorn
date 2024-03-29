using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
public class Unicorn : MonoBehaviour
{

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float tapForce = 10; 
    public float tiltSmooth = 5; 
    public Vector3 startPos;

    private Rigidbody2D rigidbody;
    private Quaternion downRotation;
    private Quaternion forwardRotation; 

    private GameManager game;

    private void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;
    }

    private void OnEnable() 
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    private void OnDisable() 
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    private void OnGameStarted() 
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;
    }

    private void OnGameOverConfirmed() 
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }

    private void Update() 
    {
        if (game.GameOver) 
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            transform.rotation = forwardRotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.tag == "ScoreZone") 
        {
            // register a score event
            OnPlayerScored(); //event sent to GameManager
           // play a sound
        }

        if (col.gameObject.tag == "DeadZone") 
        {
            rigidbody.simulated = false;
            // register a dead event
            OnPlayerDied(); //event sent to GameManager
            // play a sound
        }
    }
}
