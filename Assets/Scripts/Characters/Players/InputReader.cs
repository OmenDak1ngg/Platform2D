using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal = "Horizontal";

    public bool IsTryedToJump { get; private set; }
    public float Direction { get; private set; }

    private void Awake()
    {
        IsTryedToJump = false;
    }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsTryedToJump = true;
        }
    }

    public void ResetJumpState()
    {
        IsTryedToJump = false;
    }
}
