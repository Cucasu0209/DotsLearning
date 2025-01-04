using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;

partial struct UnitMoverSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach ((
            RefRW<LocalTransform> localTransform,
            RefRO<UnitMover> unitMover,
            RefRW<PhysicsVelocity> physicVelocity)
            in SystemAPI.Query<
                RefRW<LocalTransform>,
                RefRO<UnitMover>,
                RefRW<PhysicsVelocity>>())
        {

            float3 moveDirection = unitMover.ValueRO.TargetPosition - localTransform.ValueRO.Position;
            moveDirection = new float3(moveDirection.x, 0, moveDirection.z);
            moveDirection = math.normalize(moveDirection);



            localTransform.ValueRW.Rotation = math.slerp(localTransform.ValueRO.Rotation, quaternion.LookRotation(moveDirection, math.up()), SystemAPI.Time.DeltaTime * unitMover.ValueRO.RotationSpeed);
            //localTransform.ValueRW.Position += moveDirection * moveSpeed.ValueRO.Value * SystemAPI.Time.DeltaTime;
            physicVelocity.ValueRW.Linear = moveDirection * unitMover.ValueRO.MoveSpeed;
            physicVelocity.ValueRW.Angular = float3.zero;
        }
    }
}
