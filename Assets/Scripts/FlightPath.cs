using UnityEngine;
public class FlightPath : MonoBehaviour
{
    Vector3 start;
    public Vector3 target;
    public Vector3 flightHeight;
    [SerializeField] float speed;
    public RocketManager rm;
    private float? _t = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveAlongCurve(start, flightHeight, target, speed))
        {
            rm.RemoveRocket(gameObject);
            Destroy(gameObject);
        }
    }
    public bool MoveAlongCurve(Vector3 start, Vector3 mid, Vector3 destination, float speed = 5f, float arrivalThreshold = 0.2f)
    {
        if (_t == null) _t = 0f;

        _t += Time.deltaTime * speed / 100f;
        _t = Mathf.Clamp01(_t.Value);

        // Quadratic Bezier interpolation
        Vector3 p0 = Vector3.Lerp(start, mid, _t.Value);
        Vector3 p1 = Vector3.Lerp(mid, destination, _t.Value);
        Vector3 newPosition = Vector3.Lerp(p0, p1, _t.Value);

        // Get movement direction and rotate nose toward it
        Vector3 direction = (newPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed * 2f);
        }

        transform.position = newPosition;

        // Check arrival
        bool arrived = Vector3.Distance(transform.position, destination) <= arrivalThreshold;
        if (arrived) _t = null;

        return arrived;
    }
}
