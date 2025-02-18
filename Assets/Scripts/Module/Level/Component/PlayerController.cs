using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 选关角色控制
/// </summary>

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;

    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        GameApp.CameraManager.SetPos(transform.position);
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal == 0)
        {
            animator.Play("idle");
        }
        else
        {
            if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0))
            {
                Flip();
            }
            Vector3 pos = transform.position + Vector3.right * horizontal*moveSpeed*Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -32f, 24f);
            transform.position = pos;
            GameApp.CameraManager.SetPos(transform.position);
            animator.Play("move");
        }
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
