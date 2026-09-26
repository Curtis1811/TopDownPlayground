using System;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour, IMoveable, IDamageable, IAnimatable
{
    public GameObject GameObject { get; set; }
    public Vector3 position { get; set; }
    public Vector2 direction { get; set; }
    public float speed { get; set; }
    public bool isGrounded { get; set; }
    public IMoveable.State currentState { get; set; }
    public float health { get; set; }
   
    public Animator animator { get; set; }
    public Action<string> animationAction { get; set; }
    public NinjaData aiData;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = aiData.health;
        speed = aiData.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
    }
    
    public void OnDeath()
    {
        enabled = false;
    }
}