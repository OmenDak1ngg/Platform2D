using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundCheckDistance;

    public bool IsGrounded => CheckGround();

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.red);
    }

    private bool CheckGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}
