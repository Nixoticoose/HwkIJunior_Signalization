using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MotionSensor))]
public class Signalization : MonoBehaviour
{
    private const float MaxVolume = 1f;
    private const float MinVolume = 0f;

    [SerializeField] private float _timeIncreaseVolumeInSeconds;
    [SerializeField] private float _timeDecreaseVolumeInSeconds;

    private AudioSource _sound;
    private bool _isActivating;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
        _sound.playOnAwake = false;
        _sound.loop = true;
        _sound.volume = 0f;
    }

    public void StartAllarm()
    {
        _isActivating = true;
        _sound.Play();
        StartCoroutine(SetSmoothlyVolume(_isActivating));
    }

    public void StopAllarm()
    {
        _isActivating = false;
        StartCoroutine(SetSmoothlyVolume(_isActivating));
    }

    private IEnumerator SetSmoothlyVolume(bool isActivating)
    {
        float timeChangeVolume;
        float targetValueVolume;

        if (isActivating)
        {
            timeChangeVolume = _timeIncreaseVolumeInSeconds;
            targetValueVolume = MaxVolume;
        }
        else
        {
            timeChangeVolume = _timeDecreaseVolumeInSeconds;
            targetValueVolume = MinVolume;
        }

        for (float i = timeChangeVolume; i > 0; i -= Time.deltaTime)
        {
            float valueToChangeVolume = MaxVolume / (timeChangeVolume / Time.deltaTime);
            _sound.volume = Mathf.MoveTowards(_sound.volume, targetValueVolume, valueToChangeVolume);

            yield return null;
        }

        TryStopSound();
    }

    private bool TryStopSound()
    {
        bool canStop = false;

        if (_sound.volume == 0 && _sound.isPlaying == true)
        {
            _sound.Stop();

            canStop = true;
        }

        return canStop;
    }
}