using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 3;
    Transform transUnit;
    Animator anim;

    private void Start()
    {
        transUnit = gameObject.transform.Find("Object");
        anim = transUnit.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;

        pos.z += moveVer * speed * Time.deltaTime;
        pos.x += moveHor * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            transUnit.rotation = Quaternion.LookRotation(new Vector3(moveHor, 0, moveVer));
            anim.SetBool("Run", true);
        } else
        {
            anim.SetBool("Run", false);
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