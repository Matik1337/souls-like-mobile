using UnityEngine;
using System.Collections;
using System.Linq;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody _rigidbody;

    public bool IsActive {get; private set;}
    public float RemainingDistance {get; private set;}

    public void Run(Vector3 direction)
    {
        IsActive = true;
        transform.LookAt(direction);
        StartCoroutine(Move(direction));
    }

    public void ResetState()
    {
        IsActive = false;
        _rigidbody.isKinematic = true;
        gameObject.SetActive(false);
        RemainingDistance = 0;
    }

    private IEnumerator Move(Vector3 direction)
    {
        Vector3 startPosition = transform.position;
        float comleteDistance = 0;

        _rigidbody.isKinematic = false;
        direction = (direction - transform.position).normalized;

        while (comleteDistance < _maxDistance && IsActive)
        {
            comleteDistance = (transform.position - startPosition).magnitude;
            RemainingDistance = _maxDistance - comleteDistance;
            _rigidbody.velocity = (direction * _speed);
            yield return null;
        }

        ResetState();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Health health))
        {
            //health.TakeDamage(damage);
        }
        //Debug.Log("Collision");
        ResetState();
    }
}
