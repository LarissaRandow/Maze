using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    CharacterController controller;
    public GameObject win;

    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    float forwardSpeed = 20f;
    float strafeSpeed = 20f;

    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    void Start() {

        controller = GetComponent<CharacterController>();

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;

    }

    void Update() {
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        // force = input * speed * direction
        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;

        vertical += gravity * Time.deltaTime * Vector3.up;

        if(controller.isGrounded) {
            vertical = Vector3.down;
        }

        if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
            vertical = jumpSpeed * Vector3.up;
        }

        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0) {
            vertical = Vector3.zero;
        }

        Vector3 finalVelocity = forward + strafe + vertical;

        controller.Move(finalVelocity * Time.deltaTime);

    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Sphere")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }else if (coll.gameObject.tag == "Win")
        {
            win.gameObject.SetActive(true);
        }
    }

}
