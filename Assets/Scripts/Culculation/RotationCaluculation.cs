using UnityEngine;

/// <summary>
/// 回転計算のユーティリティ
/// </summary>

///yaw: Y軸回転（水平回転
/// pitch: X軸回転（上下回転）

public static class RotationCaluculation
{
    /// <summary>
    /// ワールド基準の方向ベクトルを取得 
    /// </summary> 
    /// <param name="targetPos">目標のワールド座標</param> 
    /// <param name="pos">基準となるワールド座標</param> 
    /// <returns>方向ベクトル</returns>
    
    public static Vector3 GetDirectionWorld(Vector3 targetPos, Vector3 pos)
    {
        return targetPos - pos;
    }

    /// <summary>
    /// ベクトルをローカル基準に修正 
    /// </summary>
    /// <param name="dir">方向ベクトル </param>
    /// <param name="parentRot">親の回転 </param>
    /// <returns>親のローカル座標基準の方向ベクトル </returns>

    public static Vector3 ConvertToLocalDirection(Vector3 dir, Quaternion parentRot)
    {
        return Quaternion.Inverse(parentRot) * dir;
    }

    /// <summary>
    /// 方向ベクトルから回転を取り出す 
    /// </summary>
    /// <param name="dir">方向ベクトル </param>
    /// <returns>縦・横　２つの回転 </returns>

    public static (Quaternion yawRot, Quaternion pitchRot) GetRotations(Vector3 dir)
    {
        if (dir.sqrMagnitude < 0.0001f)
            return (Quaternion.identity, Quaternion.identity);

        Quaternion fullRot = Quaternion.LookRotation(dir);

        Vector3 forward = fullRot * Vector3.forward;
        forward.y = 0;

        Quaternion yaw = forward.sqrMagnitude < 0.0001f
            ? Quaternion.identity
            : Quaternion.LookRotation(forward);

        Quaternion pitch = Quaternion.Inverse(yaw) * fullRot;

        return (yaw, pitch);
    }

    /// <summary>
    /// 縦回転の制限 
    /// </summary>
    /// <param name="pitch"></param>
    /// <param name="min">角度制限の最小値（度）</param>
    /// <param name="max">角度制限の最大値（度）</param>
    /// <returns>制限後の縦回転 </returns>

    public static Quaternion ClampPitch(Quaternion pitch, float min, float max)
    {
        Vector3 e = pitch.eulerAngles;
        float x = NormalizeAngle(e.x);
        x = Mathf.Clamp(x, min, max);

        // 他の軸は0固定
        return Quaternion.Euler(x, 0, 0);
    }

    /// <summary>
    /// 横回転の制限
    /// </summary>
    /// <param name="yaw">横回転 </param>
    /// <param name="min">角度制限の最小値（度）</param>
    /// <param name="max">角度制限の最大値（度）</param>
    /// <returns>制限後の横回転 </returns>

    public static Quaternion ClampYaw(Quaternion yaw, float min, float max)
    {
        Vector3 e = yaw.eulerAngles;
        float y = NormalizeAngle(e.y);
        y = Mathf.Clamp(y, min, max);

        // 他の軸は0固定
        return Quaternion.Euler(0, y, 0);
    }

    /// <summary>
    /// 角度を -180〜180 の範囲に正規化
    /// </summary>
    /// <param name="angle">角度 </param>
    /// <returns>正規化された角度 </returns>

    public static float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
