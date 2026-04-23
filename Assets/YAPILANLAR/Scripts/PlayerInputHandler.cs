using UnityEngine;

public class PlayerInputHandler : MonoBehaviour, IPlayerInput
{
    public Vector2 HareketGirdisi => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    public Vector2 KameraGirdisi => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    public bool KosuyorMu => Input.GetKey(KeyCode.LeftShift);
    public bool ZipladiMi => Input.GetButton("Jump");
}