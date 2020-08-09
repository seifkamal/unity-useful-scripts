using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody body;
    [SerializeField]
    float speed = 15;
    [SerializeField]
    float turnSpeed = 0.05f;

    void Awake()
    {
        if (body != null) return;

        if (!TryGetComponent(out Rigidbody rb))
        {
            throw new MissingComponentException(
                "PlayerController requires a RigidBody component/property."
            );
        }

        body = rb;
    }

    void FixedUpdate()
    {
        TryMove();
    }

    void TryMove()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        if (direction == Vector3.zero) return;

        body.AddForce(Vector3.ClampMagnitude(direction * speed, speed));
        body.MoveRotation(Quaternion.Slerp(
            body.rotation,
            Quaternion.LookRotation(direction),
            turnSpeed));
    }
}
