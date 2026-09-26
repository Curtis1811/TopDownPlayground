using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IMoveable, IDamageable, IAnimatable
{
    private CharacterContext _characterContext = new();
    private PlayerController _playerController;

    public NinjaData playerdata;
    public Animator animator { get; set; }
    public Action<string> animationAction { get; set; }

    public float health { get; set; }
    public float offset;
    public LayerMask layerMask;
    public bool CheckStatus;
    public BoxCollider2D hitboxCollider;

    #region IMoveable

    public Vector3 position { get; set; }

    public Vector2 direction { get; set; }

    public float speed => playerdata.speed;

    public bool isGrounded { get; set; }

    public IMoveable.State currentState { get; set; }

    #endregion


    public GameObject GameObject { get; set; }

    public string state;
    public string FSMState;
    private CameraController cameraController;

    public JutsuManager jutsuManager;
    public List<BaseJutsuData> jutsus = new();
    public List<AttackData> attacks = new();

    private void Awake()
    {
        health = playerdata.health;
        GameObject = gameObject;
        position = transform.position;
        jutsus = playerdata.JutsuList;
        attacks = playerdata.Attacks;

        animator = GetComponent<Animator>();
        _characterContext.moveable = this;
        _characterContext.animatable = this;
    }

    private void Start()
    {
        cameraController = new CameraController(this);
        _playerController = new PlayerController(_characterContext, hitboxCollider);
        _playerController.OnJutsuOneInput += RequestJutsuOneData; // subscribe
        _playerController.OnLightAttackAction += (index) => RequestAttackData(index);
        _playerController.OnMediumAttackAction += (index) => RequestAttackData(index);
        _playerController.OnHeavyAttackAction += (index) => RequestAttackData(index);
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (cameraController != null)
        {
            cameraController.FollowEntity();
        }

        _playerController.FixedUpdate();
        IsSetState();
        state = currentState.ToString();
        FSMState = _playerController.GetFSMState();
    }

    private void RequestJutsuOneData()
    {
        BaseJutsuData data = playerdata.JutsuList[0];
        JutsuContext context = JutsuContext.FromCaster(gameObject, transform.right, _characterContext);
        _playerController.JutsuAction(data, context);
    }

    private void RequestAttackData(int AttackIndex)
    {
        if (AttackIndex < 0 || AttackIndex >= attacks.Count)
        {
            Debug.LogWarning("Invalid attack index: " + AttackIndex);
            return;
        }

        _playerController.AttackAction(AttackIndex, attacks[AttackIndex], _characterContext);
    }

    private RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layerMask)
    {
        Vector2 pos = transform.position;
        Physics2D.IgnoreLayerCollision(2, 3);
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layerMask);
        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDirection * length, color);
        return hit;
    }

    private void IsSetState()
    {
        var sizeOfRay = this.GetComponent<BoxCollider2D>().size.y / 2 + offset;
        RaycastHit2D hit = Raycast(new Vector2(0, 0), Vector2.down, sizeOfRay, layerMask);
        Debug.DrawRay(transform.position, Vector2.down * sizeOfRay, Color.red);

        if (!hit)
        {
            if (GetComponent<Rigidbody2D>().linearVelocity.y < -0.1f)
            {
                currentState = IMoveable.State.IsFalling;

                return;
            }

            currentState = IMoveable.State.InAir;
        }
        else if (hit.collider.gameObject.tag == "Ground" && GetComponent<Rigidbody2D>().linearVelocity.y <= 0)
        {
            currentState = IMoveable.State.IsGrounded;
        }
    }

    private void OnDrawGizmos()
    {
        var sizeOfRay = this.GetComponent<BoxCollider2D>().size.y / 2 + offset;
        RaycastHit2D hit = Raycast(new Vector2(0, 0), Vector2.down, sizeOfRay, layerMask);
        Debug.DrawRay(transform.position, Vector2.down * sizeOfRay, Color.red);
    }
    
    public void OnDeath()
    {
        Debug.Log("Player has died.");
        enabled = false;
    }
}

public struct CharacterContext
{
    public IMoveable moveable;
    public IAnimatable animatable;
}