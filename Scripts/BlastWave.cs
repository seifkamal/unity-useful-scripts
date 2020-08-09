using System.Collections;
using UnityEngine;

public class BlastWave : MonoBehaviour
{
    [SerializeField]
    KeyCode key;

    [Header("Force")]
    [SerializeField]
    float minForce = 5;
    [SerializeField]
    float maxForce = 10;
    [SerializeField]
    float forceChargeSpeed = 3;

    [Header("Radius")]
    [SerializeField]
    float minRadius = 3;
    [SerializeField]
    float maxRadius = 7;
    [SerializeField]
    float radiusChargeSpeed = 3;

    bool charging;

    void Update()
    {
        if (!charging && Input.GetKeyDown(key))
        {
            StartCoroutine(ChargeAndBlast());
        }
    }

    IEnumerator ChargeAndBlast()
    {
        charging = true;

        float force = minForce;
        float radius = minRadius;

        do
        {
            force += forceChargeSpeed * Time.deltaTime;
            radius += radiusChargeSpeed * Time.deltaTime;

            yield return null;
        } while (Input.GetKey(key));

        Blast(
            force > maxForce ? maxForce : force,
            radius > maxRadius ? maxRadius : radius
        );

        charging = false;
    }

    void Blast(float force, float radius)
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, radius))
        {
            if (!collider.TryGetComponent(out Rigidbody colliderBody)) continue;
            if (collider.gameObject == gameObject) continue;

            Vector3 direction = colliderBody.position - transform.position;
            colliderBody.AddForce(direction.normalized * force, ForceMode.VelocityChange);
        }
    }
}
