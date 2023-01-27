using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _modelTransform;

    private void Update()
    {
        float angle = _modelTransform.localRotation.eulerAngles.y;
        angle = Mathf.Deg2Rad * angle;

        _animator.SetFloat(AnimationConstants.DirectionX, Mathf.Sin(angle));
        _animator.SetFloat(AnimationConstants.DirectionY, Mathf.Cos(angle));
    }
}
