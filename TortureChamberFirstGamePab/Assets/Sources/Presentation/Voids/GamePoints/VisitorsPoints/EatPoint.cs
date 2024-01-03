using UnityEngine;

public class EatPoint : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
    
    //TODO это должно быть в модели
    public bool IsClean { get; private set; }

    public void SetIsClean(bool isClean)
    {
        IsClean = isClean;
    }
}
