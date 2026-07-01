using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IEntity, IMoveable, IDamageble, IAnimatable
{
    private PlayerContext _playerContext = new();
    private PlayerController _playerController;

    public NinjaData playerdata;
    public Animator animator { get; set; }

    public float Health { get; set; }
    public float offset;
    public LayerMask layerMask;
    public bool CheckStatus;

    #region IMoveable

    public Vector3 position { get; set; }

    public Vector3 direction { get; set; }

    public float speed => playerdata.speed;

    public bool isGrounded { get; set; }

    public IMoveable.State currentState { get; set; }

    #endregion

    public GameObject GameObject { get; set; }

    public string state;
    private CameraController cameraController;

    public JutsuManager jutsuManager;
    public List<BaseJutsuData> jutsus = new();

    private void Awake()
    {
        Health = playerdata.health;
        GameObject = gameObject;
        position = transform.position;
        jutsus = playerdata.JutsuList;

        animator = GetComponent<Animator>();
        _playerContext.moveable = this;
        _playerContext.animatable = this;
    }

    void Start()
    {
        cameraController = new CameraController(this);
        _playerController = new PlayerController((IMoveable)this, (IAnimatable)this);
        _playerController.OnJutsuOneInput += RequestJutsuOneData; // subscribe
    }

    void Update()
    {
        if (cameraController != null)
        {
            cameraController.FollowEntity();
        }
        
        _playerController.Update();
        IsSetState();
        state = currentState.ToString();
    }

    void RequestJutsuOneData()
    {
        BaseJutsuData data = playerdata.JutsuList[0];
        JutsuContext context = JutsuContext.FromCaster(gameObject, transform.right, _playerContext);
        _playerController.JutsuAction(data, context);
        
    }
    
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layerMask)
    {
        Vector2 pos = transform.position;
        Physics2D.IgnoreLayerCollision(2, 3);
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layerMask);
        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDirection * length, color);
        return hit;
    }

    void IsSetState()
    {
        var sizeOfRay = this.GetComponent<BoxCollider2D>().size.y / 2 + offset;
        RaycastHit2D hit = Raycast(new Vector2(0, 0), Vector2.down, sizeOfRay, layerMask);

        if (!hit)
        {
            if (GetComponent<Rigidbody2D>().linearVelocity.y < 0.1f)
            {
                currentState = IMoveable.State.IsFalling;
                animator.SetBool("IsFalling", true);
                return;
            }

            currentState = IMoveable.State.InAir;
            animator.SetTrigger("Jump");
            animator.SetBool("Grounded", false);
        }
        else if (hit.collider.gameObject.tag == "Ground")
        {
            currentState = IMoveable.State.IsGrounded;
            animator.SetBool("Grounded", true);
        }
    }
}

public struct PlayerContext
{
    public IMoveable moveable;
    public IAnimatable animatable;
}