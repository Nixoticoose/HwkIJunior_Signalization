using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Signalization))]
public class MotionSensor : MonoBehaviour
{
    private Signalization _signalization;

    private void Start()
    {
        _signalization = GetComponent<Signalization>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            _signalization.StartAllarm();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            _signalization.StopAllarm();
        }
    }
}
