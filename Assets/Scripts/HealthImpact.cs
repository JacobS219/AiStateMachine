using UnityEngine;

public class HealthImpact : MonoBehaviour
{
    public float healthAmount = 40f;
    public GameObject healthParticlePrefab;
    private ParticleSystem _healthParticles;
    public AudioClip AudioFile;

    private void Awake()
    {
        _healthParticles = Instantiate(healthParticlePrefab).GetComponent<ParticleSystem>();
        _healthParticles.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tank")
        {
            DroneHealth targetHealth = collision.rigidbody.GetComponent<DroneHealth>();
            targetHealth.GiveHealth(healthAmount);

            _healthParticles.transform.position = transform.position;
            _healthParticles.gameObject.SetActive(true);
            _healthParticles.Play();

            GetComponent<AudioSource>().clip = AudioFile;
            GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }
    }
}
