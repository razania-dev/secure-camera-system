using UnityEngine;

/// <summary>
/// 回転動作の適用 
/// </summary>


public class RotationController : MonoBehaviour
{
    Transform tf;

    private void Start()
    {
        tf = transform;
    }

    /// <summary>
    /// ローカルでの回転の適用 
    /// </summary>
    /// <param name="localRotation">ローカルでの回転 </param>
    /// <param name="slerpSpeed">回転スピード </param>

    public void ApplyRotation(Quaternion localRotation, float slerpSpeed)
    {
        tf.localRotation = Quaternion.Slerp(tf.localRotation,localRotation,Time.deltaTime * slerpSpeed);
    }
}