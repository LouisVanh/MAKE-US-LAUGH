using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _orientation;
    //[SerializeField] private HealthBarUI _healthBar;
    [Header("Settings")]
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _jumpSpeed = 0.3f;
    [SerializeField] private float _jumpGraceperiod;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    //[SerializeField] private Gate _gate;
    public bool _Grounded => _controller.isGrounded;

    private CharacterController _controller;
    private float _speed = 10f;
    private Vector3 _moveDirection;
    private float _horizontalInput;
    private float _verticalInput;
    private float? _lastGroundedTime;
    private float? _jumpButtonPressedTime;
    private float _cameraVerticalRotation = 0f;
    private float _gravity = -1f;
    private float _velocity;
    private float _horizontalSpeedMultiplier = 0.8f;
    //private CameraHeadBob _headBob;
    private bool _hasJumped;
    [NonSerialized] public bool IsMoving = false;
    [NonSerialized] public bool IsSprinting = false;
    [NonSerialized] public int SkullAmount;
    [NonSerialized] public bool HasDied = false;
    [NonSerialized] public bool WonGame = false;
    private float _timeSinceHit;


    void Start()
    {
        //_healthBar = GameObject.Find("HealthBar").GetComponent<HealthBarUI>();
        //_healthBar.SetMaxHealth(_maxHealth);
        //_headBob = GetComponentInChildren<CameraHeadBob>();
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (SceneManager.GetActiveScene() == (SceneManager.GetSceneByBuildIndex(2)))
        {
            //reset everything to good start values
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            ScreenCapture.CaptureScreenshot("NEW.png", 1);
        }
        _timeSinceHit += Time.deltaTime;
        if (!HasDied && !WonGame)
        {
            Camera();
            _horizontalInput = (Input.GetAxis("Horizontal") * _horizontalSpeedMultiplier);
            _verticalInput = Input.GetAxis("Vertical");
            Sprinting();

            if (_controller.isGrounded)
            {
                _lastGroundedTime = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpButtonPressedTime = Time.time;
                _hasJumped = true;
            }
        }
    }

    //public void ShotgunPickup()
    //{
    //    _scrollIndex = 0;
    //    HasShotgun = true;
    //}
    //private void WeaponCycle() // make sure that the playerbehaviour is correctly assigned in the pickup script, and DONT FUCKING CHANGE ANY OF THIS OR I WILL DO STUFF TO YOUR ENTIRE FAMILY. INCLUDING THE DOG.
    //{
    //    if ((_skull != null && HasSkull) || (_shotgun != null && HasSkull) /*&& HasShotgun*/)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Alpha1) && HasShotgun)
    //        {
    //            _scrollIndex = 0;
    //        }
    //        if (Input.GetKeyDown(KeyCode.Alpha2))
    //        {
    //            _scrollIndex = 1;
    //        }
    //        if (_scrollIndex == 0)
    //        {
    //            _skull.gameObject.SetActive(false);
    //            _shotgun.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            _skull.gameObject.SetActive(true);
    //            _shotgun.gameObject.SetActive(false);
    //        }
    //        if (Input.mouseScrollDelta.y > 0 && HasShotgun)
    //        {
    //            _scrollIndex++;
    //        }
    //        if (Input.mouseScrollDelta.y < 0 && HasShotgun)
    //        {
    //            _scrollIndex--;
    //        }
    //        if (_scrollIndex > 1)
    //        {
    //            _scrollIndex = 0;
    //        }
    //        if (_scrollIndex < 0)
    //        {
    //            _scrollIndex = 1;

    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        _jumpButtonPressedTime = Time.time;
    //        _hasJumped = true;
    //    }

    //}

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            HasDied = true;
        }
    }

    private void Sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift) && IsMoving && Input.GetAxis("Vertical") > 0)
        {
            //_headBob.bobSpeed = 8;
            //_headBob.bobAmount = 0.3f;

            _speed = 20f;
            IsSprinting = true;
            if (_camera.fieldOfView <= 90)
            {
                _camera.fieldOfView += 40 * Time.deltaTime;
            }
        }
        else
        {
            //_headBob.bobSpeed = 6;
            //_headBob.bobAmount = 0.15f;

            if (_camera.fieldOfView >= 80)
            {
                _camera.fieldOfView -= 40 * Time.deltaTime;
            }
            IsSprinting = false;
            _speed = 10f;
        }
    }
    private void FixedUpdate()
    {
        Movement();
        ApplyGravity();

        if (_hasJumped == true && _controller.isGrounded)
        {
            Jumping();
            _hasJumped = false;
        }

        if (_moveDirection.sqrMagnitude > 0.2f/* && _controller.isGrounded*/) IsMoving = true;
        else { IsMoving = false; }
    }
    private void Camera()
    {
        float xInput = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float yInput = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _cameraVerticalRotation -= yInput;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -90f, 90f);
        _camera.transform.localEulerAngles = Vector3.right * _cameraVerticalRotation;
        transform.Rotate(Vector3.up * xInput);
    }

    private void ApplyGravity()
    {
        if (!_controller.isGrounded)
        {
            _velocity += _gravity / 2 * Time.fixedDeltaTime;
        }
        else
        {
            _velocity = -0.1f;
        }
    }
    private void Movement()
    {
        _moveDirection = transform.forward * _verticalInput + transform.right * _horizontalInput;
        Vector3 generalMovement = _moveDirection * _speed * Time.fixedDeltaTime;
        generalMovement.y = _velocity;
        _controller.Move(generalMovement);
    }
    private void Jumping()
    {

        if (Time.time - _lastGroundedTime <= _jumpGraceperiod)
        {
            if (Time.time - _jumpButtonPressedTime <= _jumpGraceperiod)
            {
                _velocity = _jumpSpeed;
                _jumpButtonPressedTime = null;
                _lastGroundedTime = null;
            }
        }
    }
    //private void PassiveHealing()
    //{
    //    if (_timeSinceHit > 3)
    //    {
    //        AddHealth(Time.deltaTime * 5);
    //    }
    //}
}