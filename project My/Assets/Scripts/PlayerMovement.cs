using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;

    [SerializeField] private float _jumpPower = 5.0f;
    [SerializeField] private float _runSpeed = 3.0f;

    [SerializeField] private bool _isGrounded;
    private bool _jumpCommand;
    private bool _leftCommand;
    private bool _rightCommand;

    [SerializeField] private GameObject _groundTestLineStart;
    [SerializeField] private GameObject _groundTestLineEnd;
    [SerializeField] private GameObject _playerBullet;
    Vector3 scale;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpCommand = true;
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
        if (_jumpCommand && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
            _jumpCommand = false;
        }

        if (_leftCommand)
        {
            _rb.velocity = new Vector2(-_runSpeed, _rb.velocity.y);
            _leftCommand = false;

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
        }

        _isGrounded = Physics2D.Linecast(_groundTestLineEnd.transform.position, _groundTestLineStart.transform.position);
    }

    private void Shoot()
    {
        var inst = Instantiate(_playerBullet, transform.position, Quaternion.identity);
        if (scale.x > 0)
        {
            inst.GetComponent<BulletMovement>().speed = (-1) * inst.GetComponent<BulletMovement>().speed;
        }
    }
}
