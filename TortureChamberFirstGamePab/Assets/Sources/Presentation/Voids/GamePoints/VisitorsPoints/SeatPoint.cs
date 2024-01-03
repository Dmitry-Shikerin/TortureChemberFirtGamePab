using System;
using System.Collections;
using System.Collections.Generic;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;
using UnityEngine;

public class SeatPoint : MonoBehaviour, IVisitorPoint
{
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
    public EatPoint EatPoint { get; private set; }

    private void Awake()
    {
        EatPoint = GetComponentInChildren<EatPoint>() ?? 
                   throw new NullReferenceException(nameof(EatPoint));
    }
    //TODO этот класс должен хранить состояния
    //TODO Могу ли я через эту вьюшку спрашивать состояние из ее модели?
}
