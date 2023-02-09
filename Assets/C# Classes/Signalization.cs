using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Signalization : MonoBehaviour
{
    private const float MaxVolume = 1f;
    private const float MinVolume = 0f;

    [SerializeField] private float _speedIncreaseVolume;
    [SerializeField] private float _speedDecreaseVolume;

    private AudioSource _sound;
    private bool _isActivating;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
        _sound.volume = 0f;
    }

    private void Update()
    {
        SetVolume(_isActivating);
        StopSound();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            _sound.Play();
            _isActivating = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            _isActivating = false;
        }
    }

    private void SetVolume(in bool isPlaying)
    {
        float targetVolume = 0f;
        float valueSpeed = 0f;

        if (_isActivating)
        {
            valueSpeed = _speedIncreaseVolume;
            targetVolume = MaxVolume;
        }
        else
        {
            valueSpeed = _speedDecreaseVolume;
            targetVolume = MinVolume;
        }

        _sound.volume = Mathf.MoveTowards(_sound.volume, targetVolume, valueSpeed * Time.fixedDeltaTime);
    }

    private void StopSound()
    {
        if (_sound.volume == 0 && _sound.isPlaying == true)
        {
            _sound.Stop();
        }
    }
}
