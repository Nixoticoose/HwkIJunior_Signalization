using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PathMovement))]
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Animator))]
public class MovementForPath : MonoBehaviour
{
    private PathMovement _path;
    private Character _movingCharacter;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AnimatorControllerParameter[] _parameters;
    private Transform _currentTargetPoint;
    private int _idParameterIsRun;
    private int _currentIndexPoint;
    private bool _isRightDirection;
    private bool _isRun;

    private void Awake()
    {
        _path = GetComponent<PathMovement>();
        _movingCharacter = GetComponent<Character>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _parameters = _animator.parameters;
        _idParameterIsRun = _parameters[0].nameHash;
        _currentIndexPoint = 0;
        _isRun = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        PlayAnimationRun();
        SetDirectionSprite();
    }

    private void Move()
    {
        if (_path != null)
        {
            if (_path.Points?.Any() == true)
            {
                if (_path.TryGetPoint(_currentIndexPoint, out _currentTargetPoint))
                {
                    if (transform.position != _currentTargetPoint.position)
                    {
                        if (transform.position.x < _currentTargetPoint.position.x)
                        {
                            _isRightDirection = true;
                        }
                        else
                        {
                            _isRightDirection = false;
                        }

                        transform.position = Vector3.MoveTowards(transform.position, _currentTargetPoint.position, _movingCharacter.SpeedRun * Time.fixedDeltaTime);
                        _isRun = true;
                    }
                    else
                    {
                        ++_currentIndexPoint;
                    }
                }
                else
                {
                    _isRun = false;
                }
            }
        }
    }

    private void PlayAnimationRun()
    {
        _animator.SetBool(_idParameterIsRun, _isRun);
    }

    private void SetDirectionSprite()
    {
        if (_isRun)
        {
            _spriteRenderer.flipX = _isRightDirection;
        }
    }
}

