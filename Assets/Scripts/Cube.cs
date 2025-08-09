using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Painter _painter;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private int _minLifeTime;
    [SerializeField] private int _maxLifeTime;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private bool _isCollided;

    public event Action<Cube> PlatformCollided;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void Intialize(Vector3 position, Vector3 velocity)
    {
        _isCollided = false;
        transform.position = position;
        _rigidbody.linearVelocity = velocity;

        Repaint(_defaultColor);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided == false && collision.gameObject.TryGetComponent<Platform>(out _))
        {
            _isCollided = true;

            Repaint(_painter.GenerateRandomColor());

            StartCoroutine(ReportAboutCollision());
        }
    }

    private void Repaint(Color color)
    {
        _renderer.material.color = color;
    }

    private IEnumerator ReportAboutCollision()
    {
        var wait = new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1));
        
        yield return wait;

        PlatformCollided?.Invoke(this);
    }
}
