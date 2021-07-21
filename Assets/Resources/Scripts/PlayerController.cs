using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 3;
    [SerializeField] Transform _transUnit;
    [SerializeField] Animator _anim;

    private void Awake()
    {
        _transUnit = gameObject.transform.Find("Model");
        _anim = _transUnit.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void UpdateModel(GameObject model)
    {
        _transUnit = model.transform;
        _anim = model.GetComponent<Animator>();
    }

    private void Movement()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;

        pos.z += moveVer * _speed * Time.deltaTime;
        pos.x += moveHor * _speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _transUnit.rotation = Quaternion.LookRotation(new Vector3(moveHor, 0, moveVer));
            _anim.SetBool("Run", true);
        } else
        {
            _anim.SetBool("Run", false);
        }

        pos = transform.position.x < 0.1f
            ? new Vector3(0.1f, transform.position.y, transform.position.z)
            : pos;

        pos = transform.position.z < 0.1f
            ? new Vector3(transform.position.x, transform.position.y, 0.1f)
            : pos;

        transform.position = pos;

    }
}