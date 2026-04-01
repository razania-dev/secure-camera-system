using UnityEngine;

/// <summary>
/// 進行・管理 
/// </summary>

public class GameManager : MonoBehaviour
{
    public SecurityCameraManager scManager;

    //機能を追加予定 

    private void Update() 
    {
        var targetPos = GetMouse.GetWorldMousePos();

        scManager.TrackingTarget(targetPos); 
    }
}
