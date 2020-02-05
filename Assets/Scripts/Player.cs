using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 Velocity;
    public float JumpPower;

    public Transform verRot;
    public Transform horRot;

    public float MoveSpeed;
    private Animator animator;

    [Range(1, 100)]
    public float CameraRotRatio = 4f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");

        horRot.transform.Rotate(new Vector3(0, X_Rotation * CameraRotRatio, 0));
        verRot.transform.Rotate(-Y_Rotation * CameraRotRatio, 0, 0);

        bool moving = false;
                bool shoot = Input.GetMouseButtonDown(0);

        if (Input.GetKey(KeyCode.W))
        {
            characterController.Move(this.gameObject.transform.forward * MoveSpeed * Time.deltaTime);
            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(this.gameObject.transform.forward * -1f * MoveSpeed * Time.deltaTime);
            moving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            characterController.Move(this.gameObject.transform.right * -1 * MoveSpeed * Time.deltaTime);
            moving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            characterController.Move(this.gameObject.transform.right * MoveSpeed * Time.deltaTime);
            moving = true;
        }

        animator.SetBool(moving ? "WalkFrontShoot" : "BurstShot", Input.GetMouseButtonDown(0));

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            animator.SetBool("Jump", false);
        }
        else
        {
            if (Input.GetKey(KeyCode.Space) && !animator.IsInTransition(0))
            {
                animator.SetBool("Jump", true);
                Velocity.y += 1.0f;
            }
        }

        animator.SetBool("Running", moving);

        characterController.Move(Velocity);//①キャラクターコントローラーをVelocityだけ動かし続ける
        Velocity.y += Physics.gravity.y * Time.deltaTime;//①Velocityのy軸を重力*Time.deltaTime分だけ動かす
    }
}
