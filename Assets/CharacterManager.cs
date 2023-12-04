using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.Speech;

public class CharacterManager : MonoBehaviour
{
    public Transform Sprites;
    private PlayerInput _input;
    private Animator _animator;
    private float _speed = 5;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    public void OnDirection(InputAction.CallbackContext context)
    {
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
        _direction = context.ReadValue<Vector2>();


    }
}