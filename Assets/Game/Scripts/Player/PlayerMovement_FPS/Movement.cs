using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
#pragma warning disable 649
  [SerializeField] CharacterController controller;
  [SerializeField] float speed = 11f;
  [SerializeField] float jumpHeight = 3.5f;
  bool jump;

  [SerializeField] float gravity = -30f; //-9.81
  Vector3 verticalVelocity = Vector3.zero;
  [SerializeField] LayerMask groundMask;
  bool isGrounded;

  Vector2 horizontalInput;
  #region  Unity Callbacks
  private void Update()
  {
    isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
    if (isGrounded)
    {
      verticalVelocity.y = 0;
    }
    Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
    controller.Move(horizontalVelocity * Time.deltaTime);
    // Jump: v = sqrt(-2* jumpHeight * gravity)
    if (jump)
    {
      // do stuff if jump is pressed.
      if (isGrounded)
      {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //allow jumping if already grounded
        verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
      }
      jump = false;

    }

    verticalVelocity.y += gravity * Time.deltaTime;
    controller.Move(verticalVelocity * Time.deltaTime);
  }
  #endregion
  public void RecieveInput(Vector2 _horizontalInput)
  {
    horizontalInput = _horizontalInput;
//    print("HorizontalInput: " + horizontalInput);
  }
  public void OnJumpPress()
  {
    jump = true;

  }
}
