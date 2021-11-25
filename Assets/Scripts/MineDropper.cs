using UnityEngine;

public class MineDropper : MonoBehaviour
{
    [SerializeField] private float fireRefreshRate = .75f;

    private IMineDropper _dropper;
    private float _nextFireTime;

    private void Awake()
    {
        _dropper = GetComponent<IMineDropper>();
    }

    private bool CanFire()
    {
        return Time.time > _nextFireTime;
    }

    public void Drop()
    {
        if (CanFire())
        {
            _dropper.Drop(this);
            _nextFireTime = Time.time + fireRefreshRate;
        }
    }
}