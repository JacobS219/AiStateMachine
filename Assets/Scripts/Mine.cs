using UnityEngine;

public class Mine : MonoBehaviour, IMineDropper
{
    [SerializeField] private Rigidbody minePrefab;
    [SerializeField] private Transform weaponMountPoint;
    [SerializeField] private int fireForce = 1;
    public GameObject m_ExplosionPrefab;

    public void Drop(MineDropper dropper)
    {
        var mine = Instantiate(minePrefab, weaponMountPoint.position, weaponMountPoint.rotation);
        mine.AddForce(mine.transform.forward * fireForce);
    }
}
