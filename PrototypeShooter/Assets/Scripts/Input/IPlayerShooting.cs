using UnityEngine;

public interface IPlayerShooting
{
    
    Vector2 playerLookDir { set; get; }
    Transform body { get; }
    void Shoot();

}