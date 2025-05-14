using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal = "Horizontal";

    public bool TryedToJump { get; private set; }
    public float Direction { get; private set; }

    private void Awake()
    {
        TryedToJump = false;
    }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryedToJump = true;
        }
    }

    public void ResetJumpState()
    {
        TryedToJump = false;
    }
}
