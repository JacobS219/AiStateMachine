using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float fireRefreshRate = .75f;
    public AudioClip AudioFile;

    private ILauncher _launcher;
    private float _nextFireTime;

    private void Awake()
    {
        _launcher = GetComponent<ILauncher>();
    }

    private bool CanFire()
    {
        return Time.time > _nextFireTime;
    }

    public void FireWeapon()
    {
        if (CanFire())
        {
            _launcher.Launch(this);
            _nextFireTime = Time.time + fireRefreshRate;
            GetComponent<AudioSource>().clip = AudioFile;
            GetComponent<AudioSource>().Play();
        }
    }
}