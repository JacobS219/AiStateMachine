using UnityEngine;

public class DroneHealth : MonoBehaviour
{
    public float startingHealth = 100f;
    //public Color m_ZeroHealthColor = Color.red;
    //public Color m_FullHealthColor = Color.green;
    //public Slider m_Slider;
    //public Image m_FillImage;

    [SerializeField] private Rigidbody healthPrefab;
    [SerializeField] private Transform healthDropPoint;
    [SerializeField] private int fireForce = 1;

    private float _currentHealth;
    private bool _dead;

    public GameObject m_ExplosionPrefab;
    private ParticleSystem _explosionParticles;

    public GameObject damagePrefab;
    private ParticleSystem _damageParticle;

    private void Awake()
    {
        _damageParticle = Instantiate(damagePrefab).GetComponent<ParticleSystem>();
        _damageParticle.gameObject.SetActive(false);

        _explosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        _explosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _currentHealth = startingHealth;
        _dead = false;

        // Update the health slider's value and color.
        //SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        // Change the UI elements appropriately.
        //SetHealthUI();

        if (_currentHealth <= 50f && !_dead)
        {
            _damageParticle.transform.position = transform.position;
            _damageParticle.gameObject.SetActive(true);
            _damageParticle.Play();
        }

        if (_currentHealth <= 0f && !_dead)
        {
            OnDeath();
        }
    }

    public void GiveHealth(float amount)
    {
        _currentHealth += amount;
    }


    //private void SetHealthUI()
    //{
    //    // Set the slider's value appropriately.
    //    m_Slider.value = m_CurrentHealth;

    //    // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
    //    m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    //}


    private void OnDeath()
    {
        _dead = true;

        _explosionParticles.transform.position = transform.position;
        _explosionParticles.gameObject.SetActive(true);
        _explosionParticles.Play();

        // Play the tank explosion sound effect.
        //m_ExplosionAudio.Play();

        DropHealth();
        
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void DropHealth()
    {
        var health = Instantiate(healthPrefab, healthDropPoint.position, healthDropPoint.rotation);
        health.AddForce(health.transform.forward * fireForce);
    }
}
