using UnityEngine;

public class MissileImpact : MonoBehaviour
{
    public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
    private ParticleSystem _explosionParticles;         // Reference to the particles that will play on explosion.
    //public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
    public float m_MaxDamage = 20f;                    // The amount of damage done if the explosion is centred on a tank.
    public float m_ExplosionForce = 500f;              // The amount of force added to a tank at the centre of the explosion.
    public float m_MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.
    public float m_ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.

    public GameObject m_ExplosionPrefab;
    public AudioClip AudioClip;

    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

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

            //m_ExplosionAudio.Play();
            //m_ExplosionAudio.PlayOneShot(AudioClip);
            Destroy(gameObject);
        }

        // Unparent the particles from the shell.
        //m_ExplosionParticles.transform.parent = null;

        // Play the explosion sound effect.
        //m_ExplosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        //ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
        //Destroy(m_ExplosionParticles.gameObject, mainModule.duration);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
