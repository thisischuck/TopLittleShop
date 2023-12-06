using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    private Rigidbody2D _body;
    public Transform Sprites;
    private PlayerInput _input;
    private Animator _animator;
    private float _speed = 5;
    private Vector3 _direction;
    private bool interacted = false;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _body.velocity = _direction * _speed;
    }

    public void OnDirection(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
        if (context.performed)
        {
            _animator.Play("Rogue_run_01");
            if (_direction.x != 0)
                Sprites.localScale = _direction.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
        else if (context.canceled)
        {
            _animator.Play("Rogue_idle_01");
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
            interacted = context.ReadValueAsButton();
    }
}
