using UnityEngine;

public interface IPlayerInput
{
    Vector2 HareketGirdisi { get; }
    Vector2 KameraGirdisi { get; }
    bool KosuyorMu { get; }
    bool ZipladiMi { get; }
}