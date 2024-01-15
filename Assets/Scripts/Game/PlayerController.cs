using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float MovePower = 10f;
    [SerializeField]
    private float JumpPower = 500f;
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private bool isJumping;
    [SerializeField]
    private Sprite[] Images;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        isJumping = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.IsMine) {
            if(Input.GetKey(KeyCode.D)){
                rb.AddForce(new Vector2(MovePower, 0f));
                photonView.RPC("Flip", RpcTarget.All, false);
            }
            if(Input.GetKey(KeyCode.A)){
                rb.AddForce(new Vector2(-MovePower, 0f));
                photonView.RPC("Flip", RpcTarget.All, true);
            }
            if(Input.GetKey(KeyCode.Space) && !isJumping){
                rb.AddForce(new Vector2(0f, JumpPower));
                isJumping = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")){
            isJumping = false;
        }
    }

    [PunRPC]
    private void Flip(bool x) {
        renderer.flipX = x;
    }

    public void ChangeChara(int i) {
        anim = GetComponent<Animator>();
        anim.SetInteger("chid", i);
    }
}
