using UnityEngine;

public class MineImpact : MonoBehaviour
{
    public float m_MaxDamage = 100f;
    public float m_ExplosionForce = 500f;
    public float m_ExplosionRadius = 5f;

    private ParticleSystem _explosionParticles;
    public GameObject m_ExplosionPrefab;

    private void Awake()
    {
        _explosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        _explosionParticles.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tank")
        {
            collision.rigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            DroneHealth targetHealth = collision.rigidbody.GetComponent<DroneHealth>();
            targetHealth.TakeDamage(m_MaxDamage);

            _explosionParticles.transform.position = transform.position;
            _explosionParticles.gameObject.SetActive(true);
            _explosionParticles.Play();

            Destroy(gameObject);
        }
    }

    //private float CalculateDamage(Vector3 targetPosition)
    //{
    //    Vector3 explosionToTarget = targetPosition - transform.position;
    //    float explosionDistance = explosionToTarget.magnitude;
    //    float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;
    //    float damage = relativeDistance * m_MaxDamage;

    //    damage = Mathf.Max(0f, damage);

    //    return damage;
    //}
}
