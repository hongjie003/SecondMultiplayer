using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    private PhotonView view;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (view.IsMine) {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            // Animation
            // Left & Right
            if(horizontal > 0) anim.SetBool("IsRunningRight", true);
            if(horizontal < 0) anim.SetBool("IsRunningLeft", true);
            if(horizontal == 0) {
                anim.SetBool("IsRunningRight", false);
                anim.SetBool("IsRunningLeft", false);
            }

            // Up & Down
            if(vertical > 0) anim.SetBool("IsRunningUp", true);
            if(vertical < 0) anim.SetBool("IsRunningDown", true);
            if(vertical == 0) {
                anim.SetBool("IsRunningUp", false);
                anim.SetBool("IsRunningDown", false);
            }
        }
    }

    private void FixedUpdate() {
        if (view.IsMine) {
            if (horizontal != 0 && vertical != 0) {
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }

            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
    }
}
