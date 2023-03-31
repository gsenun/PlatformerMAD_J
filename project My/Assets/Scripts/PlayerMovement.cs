using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;

    private float _groundedTime;
    [SerializeField] private float _jumpBufferTime;
    private float _jumpCommandTime;
    private bool _jumpEnded;
    private bool _canAirJump;
    [SerializeField] private bool _doubleJumpEnabled;
    [SerializeField] private float _jumpPower = 5.0f;
    [SerializeField] private float _runSpeed = 3.0f;
    private bool _canJump;
    [SerializeField] private float _coyoteTime;
    [SerializeField] private bool _isGrounded;
    private bool _jumpCommand;
    private bool _leftCommand;
    private bool _rightCommand;


    [SerializeField] private GameObject _groundTestLineStart;
    [SerializeField] private GameObject _groundTestLineEnd;
    [SerializeField] private GameObject _playerBullet;
    Vector3 scale;


    private Animator _animator;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpCommand = true;
            _jumpCommandTime = Time.unscaledTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _jumpEnded = true;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            _leftCommand = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rightCommand = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {

        string animation = "IdleAnimation";
        if (!_isGrounded)
        {
            if (_rb.velocity.y > 0)
            {
                animation = "JumpAnimation";
            }
            else
            {
                animation = "FallAnimation";
            }
        }



        if (_jumpCommand && _canJump && Time.unscaledTime - _jumpCommandTime < _jumpBufferTime)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
            _jumpCommand = false;
            _canAirJump = true;

        }

        if ( _doubleJumpEnabled && _jumpCommand && !_isGrounded && _canAirJump && _rb.velocity.y < 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
            _jumpCommand = false;
            _canAirJump = false;
        }

        if (_jumpEnded)
        {
            Vector2 v = new Vector2(_rb.velocity.x, _rb.velocity.y / 2.0f);
            _rb.velocity = v;
            _jumpEnded = false;
        }

        if (_leftCommand)
        {
            _rb.velocity = new Vector2(-_runSpeed, _rb.velocity.y);
            _leftCommand = false;

            animation = "RunAnimation";

            scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
        else if (_rightCommand)
        {
            _rb.velocity = new Vector2(_runSpeed, _rb.velocity.y);
            _rightCommand = false;
            scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
            animation = "RunAnimation";
        }
        _animator.Play(animation);

        _isGrounded = Physics2D.Linecast(_groundTestLineEnd.transform.position, _groundTestLineStart.transform.position);

        if (_isGrounded)
        {
            _groundedTime = Time.unscaledTime;
            _canJump = true;
        }
        else
        {
            if (Time.unscaledTime - _groundedTime > _coyoteTime)
            {
                _canJump = false;
            }
        }

    }

    private void Shoot()
    {
        var inst = Instantiate(_playerBullet, transform.position, Quaternion.identity);
        if (scale.x > 0)
        {
            inst.GetComponent<BulletMovement>().SetSpeed((-1) * inst.GetComponent<BulletMovement>().GetSpeed());
        }
    }
}
