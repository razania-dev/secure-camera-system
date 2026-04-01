using UnityEngine;

/// <summary> 
/// カメラの動作を行う 
/// </summary>

public class SecurityCameraManager : MonoBehaviour
{
    public float slerpSpeed;
    public float clampMin;
    public float clampMax;
    public RotationController v1_Arm;
    public RotationController h1_Arm;
    public RotationController v2_Arm;
    public RotationController h2_Arm;
    public RotationController securityCamera;


    /// <summary>
    /// ターゲットの位置に監視カメラの視点を追従する 
    /// </summary>
    /// <param name="targetPos">ターゲットの位置 </param>

    public void TrackingTarget(Vector3 targetPos)
    {
        // アームの姿勢計算・適用 
        // v1_arm: 垂直回転のみ 
        // h1_arm: 水平回転のみ 
        // v1・h1アームは角度の幅に制限あり 

        Vector3 v1_dir = RotationCaluculation.GetDirectionWorld(targetPos, v1_Arm.transform.position);
        Vector3 v1_Localdir = RotationCaluculation.ConvertToLocalDirection(v1_dir, v1_Arm.transform.parent.rotation);
        var (v1_Yaw, v1_Pitch) = RotationCaluculation.GetRotations(v1_Localdir); var v1_ClampedRot = RotationCaluculation.ClampPitch(v1_Pitch, clampMin, clampMax);

        v1_Arm.ApplyRotation(v1_ClampedRot, slerpSpeed); Vector3 h1_dir = RotationCaluculation.GetDirectionWorld(targetPos, h1_Arm.transform.position);
        Vector3 h1_Localdir = RotationCaluculation.ConvertToLocalDirection(h1_dir, h1_Arm.transform.parent.rotation); var (h1_Yaw, h1_Pitch) = RotationCaluculation.GetRotations(h1_Localdir);
        var h1_ClampedRot = RotationCaluculation.ClampYaw(h1_Yaw, clampMin, clampMax); h1_Arm.ApplyRotation(h1_ClampedRot, slerpSpeed);

        // v2_arm: 垂直回転のみ 
        // h2_arm: 水平回転のみ 
        // v2・h2アームは角度の幅に制限なし 

        Vector3 v2_dir = RotationCaluculation.GetDirectionWorld(targetPos, v2_Arm.transform.position);
        Vector3 v2_localDir = RotationCaluculation.ConvertToLocalDirection(v2_dir, v2_Arm.transform.parent.rotation);
        var (_, v2_Pitch) = RotationCaluculation.GetRotations(v2_localDir); v2_Arm.ApplyRotation(v2_Pitch, slerpSpeed);

        Vector3 h2_dir = RotationCaluculation.GetDirectionWorld(targetPos, h2_Arm.transform.position);
        Vector3 h2_localDir = RotationCaluculation.ConvertToLocalDirection(h2_dir, h2_Arm.transform.parent.rotation);
        var (h2_Yaw, _) = RotationCaluculation.GetRotations(h2_localDir); h2_Arm.ApplyRotation(h2_Yaw, slerpSpeed);

        // SecurityCamera: 縦横回転 

        Vector3 sc_dir = RotationCaluculation.GetDirectionWorld(targetPos, securityCamera.transform.position);
        Vector3 sc_localDir = RotationCaluculation.ConvertToLocalDirection(sc_dir, securityCamera.transform.parent.rotation);
        var (sc_Yaw, sc_pitch) = RotationCaluculation.GetRotations(sc_localDir);
        var sc_FullRot = sc_Yaw * sc_pitch; securityCamera.ApplyRotation(sc_FullRot, slerpSpeed);


        //デバグ 

        Debug.DrawRay(h1_Arm.transform.position, h1_Arm.transform.up * 2, Color.green);
        Debug.DrawRay(h1_Arm.transform.position, h1_Arm.transform.forward * 2, Color.blue);
        Debug.DrawRay(v1_Arm.transform.position, v1_Arm.transform.up * 2, Color.red);
        Debug.DrawRay(v1_Arm.transform.position, v1_Arm.transform.forward * 2, Color.purple);
        Debug.DrawRay(h1_Arm.transform.position, targetPos * 2, Color.yellow);
        Debug.DrawRay(v1_Arm.transform.position, targetPos * 2, Color.yellow);
    }
}
