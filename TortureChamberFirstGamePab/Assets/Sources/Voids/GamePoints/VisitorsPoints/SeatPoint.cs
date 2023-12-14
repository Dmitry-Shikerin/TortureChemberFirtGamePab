using System.Collections;
using System.Collections.Generic;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;
using UnityEngine;

public class SeatPoint : MonoBehaviour, IVisitorPoint
{
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
}
