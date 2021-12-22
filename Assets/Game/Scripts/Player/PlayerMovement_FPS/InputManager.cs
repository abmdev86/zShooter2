
using UnityEngine;

public class InputManager : MonoBehaviour
{
  [SerializeField] Movement movement;
  [SerializeField] MouseLook mouseLook;
  WeaponManager weaponManager;

  PlayerControls controls;
  PlayerControls.GroundMovementActions groundMovement;
  Vector2 horizontalInput;
  Vector2 mouseInput;
  private void Awake()
  {
    weaponManager = GetComponent<WeaponManager>();
    controls = new PlayerControls();
    groundMovement = controls.GroundMovement;
    // groundMovement.[action].performed += context => do something.
    groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
    groundMovement.Jump.performed += _ => movement.OnJumpPress();
    groundMovement.WeaponSlot1.performed += _ => weaponManager.UsePrimary();
    groundMovement.WeaponSlot3.performed += _ => weaponManager.UseSecondary();

    groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();

    groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    groundMovement.Shoot.performed += _ => weaponManager.Attack();

  }
  private void Update()
  {
    movement.RecieveInput(horizontalInput);
    mouseLook.RecieveInput(mouseInput);
  }

  private void OnEnable()
  {
    controls.Enable();
  }
  private void OnDisable()
  {
    controls.Disable();
  }
}
