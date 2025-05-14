using Unity.VisualScripting;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _squareSideSizeX;
    [SerializeField] private float _squareSideSizeY;
    [SerializeField] private Transform _reference;

    public bool IsGrounded => CheckGround();

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.red);
    }

    private bool CheckGround()
    {
        Vector2 checkZoneSize = new Vector2(_squareSideSizeX, _squareSideSizeY);
        Collider2D hit = Physics2D.OverlapBox(_reference.position, checkZoneSize, 0f);
        return hit != null;
    }

    private void OnDrawGizmos()
    {
        Vector3 checkZoneSize = new Vector3(_squareSideSizeX, _squareSideSizeY);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(_reference.position, checkZoneSize);
    }
}
