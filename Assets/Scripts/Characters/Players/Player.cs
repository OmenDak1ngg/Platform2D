using UnityEngine;

public class Player : Character
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private InputReader _inputReader;

    private void FixedUpdate()
    {
        if(_inputReader.Direction != 0)
        {
            _playerMover.Move(_inputReader.Direction);
        }

        if(_inputReader.IsTryedToJump != false)
        {
            if (_groundChecker.IsGrounded)
            {
                _playerMover.Jump();
                _inputReader.ResetJumpState();
            }
        }
    }
}
