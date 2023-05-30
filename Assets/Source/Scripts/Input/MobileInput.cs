using UnityEngine;
using UnityEngine.UI;

public class MobileInput : InputManager
{
    [SerializeField] private VariableJoystick _movementJoystick;
    [SerializeField] private VariableJoystick _shootingJoystick;
    [SerializeField] private Button _buttonWeaponChange;
    [SerializeField] private Button _buttonUseAbility;
    [SerializeField] private Button _buttonInteraction;
    [SerializeField] private Transform _cameraTransform;

    private void OnEnable()
    {
        _buttonUseAbility.onClick.AddListener(OnAbilityButtonClick);
        _buttonInteraction.onClick.AddListener(OnInteractionButtonClick);
        _buttonWeaponChange.onClick.AddListener(OnWeaponChangeButtonClick);
    }

    private void OnDisable()
    {
        _buttonUseAbility.onClick.RemoveListener(OnAbilityButtonClick);
        _buttonInteraction.onClick.RemoveListener(OnInteractionButtonClick);
        _buttonWeaponChange.onClick.RemoveListener(OnWeaponChangeButtonClick);
    }

    public override Vector3 GetMoveDirection()
    {
        Vector2 direction = RotateDirection(_movementJoystick.Direction, _cameraTransform.rotation.y);

        return ToVector3(direction);
    }

    public override Vector3 GetShootDirection()
    {
        Vector2 direction = RotateDirection(_shootingJoystick.Direction, _cameraTransform.rotation.y);
        
        NeedShoot = direction != Vector2.zero;
        NeedRun = !NeedShoot;

        return ToVector3(direction);
    }

    private void OnAbilityButtonClick() => AbilityClick();
    private void OnWeaponChangeButtonClick() => WeaponClick();
    private void OnInteractionButtonClick() => Interact();
}
