using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UnitMoverAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public float RotationSpeed;
    public Vector3 TargetPosition;


    public class Baker : Baker<UnitMoverAuthoring>
    {
        public override void Bake(UnitMoverAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new UnitMover()
            {
                MoveSpeed = authoring.MoveSpeed,
                RotationSpeed = authoring.RotationSpeed,
                TargetPosition = authoring.TargetPosition,
            });
        }
    }
}
public struct UnitMover : IComponentData
{

    public float RotationSpeed;
    public float MoveSpeed;
    public float3 TargetPosition;
}
