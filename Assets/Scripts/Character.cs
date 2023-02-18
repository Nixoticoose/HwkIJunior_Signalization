using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private float _speedRun;

    public float SpeedRun { get => _speedRun; }
}
