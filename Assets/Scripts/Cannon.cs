using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private Transform _shootingDir;
    [SerializeField] private Vector3 _cannonBallPosition;

    [SerializeField] private bool _inCannon;
    private bool _isShooting;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _explosionForce;
    [SerializeField] private Vector3 _backflipSpeed;
    [SerializeField] private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip _landSound;
    [SerializeField] private AudioClip _cannonShotSound;
    private int _timesUsed;



    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInChildren<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

    }
    private void OnMouseDown()
    {
        if (!_inCannon)
        {
            _characterController.enabled = false;
            _playerBehaviour.HasControlOfMovement = false;
            _inCannon = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _characterController.gameObject.transform.position = this.transform.position + new Vector3(0, 1, 0);
            _characterController.gameObject.transform.SetParent((_rigidbody.gameObject.transform));
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (_inCannon && !_isShooting)
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (IsBetweenFloats(-50, 20, this.gameObject.transform.eulerAngles.z) || IsBetweenFloats(-50 + 360, 20 + 360, this.gameObject.transform.eulerAngles.z)/*&& this.gameObject.transform.eulerAngles.z < 20*/)
                    this.gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * _rotationSpeed));
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (IsBetweenFloats(-50, 20, this.gameObject.transform.eulerAngles.z) || IsBetweenFloats(-50 + 360, 20 + 360, this.gameObject.transform.eulerAngles.z)/* && this.gameObject.transform.eulerAngles.z > 20*/)
                {
                    //if()
                    this.gameObject.transform.Rotate(0, 0, -Time.deltaTime * _rotationSpeed);
                }
            }
            if (Input.GetKey(KeyCode.Space) && !_isShooting)
            {
                _rigidbody.constraints = RigidbodyConstraints.None;
                _rigidbody.AddForce(_explosionForce * _shootingDir.transform.forward, ForceMode.Impulse);
                _rigidbody.AddTorque(_backflipSpeed);
                _isShooting = true;
                _playerAudioSource.PlayOneShot(_cannonShotSound);
                //this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(120, 0, RandomBetween(10, 70)));;
                _timesUsed++;
            }
        }
        if (_inCannon && (_rigidbody.position.y) < 0.5f)
        {
            _rigidbody.transform.DetachChildren();
            _characterController.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            _characterController.enabled = true;
            _playerBehaviour.HasControlOfMovement = true;
            _inCannon = false;
            _isShooting = false;
            _rigidbody.Sleep();
            _rigidbody.freezeRotation = true;
            _rigidbody.transform.position = _shootingDir.position;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _playerAudioSource.Stop();
            _playerAudioSource.PlayOneShot(_landSound);
        }
    }

    public bool IsBetweenFloats(float min, float max, float test)
    {
        if (test > min && test < max)
        {
            return true;
        }
        else return false;
    }
    public float RandomBetween(float min, float max)
    {
        float r = Random.Range(min, max);
        return r;
    }
}
