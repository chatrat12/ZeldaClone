using UnityEngine;

[System.Serializable]
public class PlayerMovement : CharacterMovement
{
    public delegate void PlayerEvent(PlayerController player);
    public event PlayerEvent FinishedAutoWalking;

    private static float _autoWalkTargetDistance = 0.2f;

    public bool IsAutoWalking { get { return _autoWalking; } }
    
    private bool _autoWalking = false;
    private Vector3 _autoWalkTarget;

    public void AutoWalkTo(Vector3 position)
    {
        _autoWalking = true;
        _autoWalkTarget = position;

        var direction = _autoWalkTarget - _rigidbody.position;
        direction.y = 0;
        direction.Normalize();

        _character.transform.rotation = Quaternion.LookRotation(direction);
    }

    public override void Update()
    {
        if (_autoWalking)
            AutoWalk();
        else
            base.Update();
    }

    private void AutoWalk()
    {
        if (Vector3.Distance(_rigidbody.position, _autoWalkTarget) > _autoWalkTargetDistance)
        {
            var direction = _autoWalkTarget - _rigidbody.position;
            direction.y = 0;
            direction.Normalize();
            
            _rigidbody.velocity = direction * _runSpeed;
        }
        else
        {
            _autoWalking = false;
            if (FinishedAutoWalking != null)
                FinishedAutoWalking(_character as PlayerController);
        }
    }

}
