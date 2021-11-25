using UnityEngine;

public class Missile : MonoBehaviour, ILauncher
{
    [SerializeField] private Rigidbody missilePrefab;
    [SerializeField] private int fireForce = 2500;
    [SerializeField] private int missileSelfDestructTimer = 5;
    [SerializeField] private Transform weaponMountPoint;
    public GameObject m_ExplosionPrefab;
    private ParticleSystem m_LaunchParticles;

    private void Awake()
    {
        m_LaunchParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_LaunchParticles.gameObject.SetActive(false);
    }

    public void Launch(Launcher launcher)
    {
        var missile = Instantiate(missilePrefab, weaponMountPoint.position, weaponMountPoint.rotation);
        m_LaunchParticles.transform.position = transform.position;
        m_LaunchParticles.gameObject.SetActive(true);
        m_LaunchParticles.Play();
        missile.AddForce(missile.transform.forward * fireForce);
    }
}
