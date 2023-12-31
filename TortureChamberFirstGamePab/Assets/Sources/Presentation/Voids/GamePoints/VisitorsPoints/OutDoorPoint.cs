using System.Collections;
using System.Collections.Generic;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;
using UnityEngine;

public class OutDoorPoint : MonoBehaviour, IVisitorPoint
{
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
}
