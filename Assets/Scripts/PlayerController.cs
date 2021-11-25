using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float turnSpeed = 150f;
    [SerializeField] private Team _team;

    public Team Team => _team;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool FireWeapons { get; private set; }

    public Launcher _launcher;
    public MineDropper _dropper;

    //public GameObject thrusterPrefab;
    //private ParticleSystem _thrusterParticle;

    //private void Awake()
    //{
    //    _thrusterParticle = Instantiate(thrusterPrefab).GetComponent<ParticleSystem>();
    //    _thrusterParticle.gameObject.SetActive(false);
    //}

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireWeapon();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            DropMine();
        }

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.position += Time.deltaTime * vertical * transform.forward * speed;
        transform.Rotate(Vector3.up * horizontal * turnSpeed * Time.deltaTime);

        //if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        //{
            //thrusterPrefab.transform.position = transform.position;
            //thrusterPrefab.SetActive(vertical > 0);
            //_thrusterParticle.transform.position = transform.position;
            //_thrusterParticle.gameObject.SetActive(true);
            //_thrusterParticle.Play();
        //}
    }

    private void FireWeapon()
    {
        _launcher.FireWeapon();
    }

    private void DropMine()
    {
        _dropper.Drop();
    }
}
