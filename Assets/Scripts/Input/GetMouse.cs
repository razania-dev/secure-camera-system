using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>  
/// マウスの入力の取得 
/// </summary> 

public static class GetMouse
{
    /// <summary> 
    /// マウスの座標をワールド基準にして取得 
    /// </summary> 
    /// <returns>マウスのワールド基準の座標</returns>
    
    public static Vector3 GetWorldMousePos()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 pos = new Vector3(mousePos.x, mousePos.y, 10f); return Camera.main.ScreenToWorldPoint(pos);
    }
}
