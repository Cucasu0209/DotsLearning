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
            RefRO<MoveSpeed> moveSpeed,
            RefRW<PhysicsVelocity> physicVelocity) 
            in SystemAPI.Query<
                RefRW<LocalTransform>, 
                RefRO<MoveSpeed>,
                RefRW<PhysicsVelocity>>())
        {
            float3 targetPosition = localTransform.ValueRO.Position + new float3(10, 0, 0);
            float3 moveDirection = targetPosition - localTransform.ValueRO.Position;
            moveDirection = math.normalize(moveDirection);
            localTransform.ValueRW.Rotation = quaternion.LookRotation(moveDirection, math.up());
            //localTransform.ValueRW.Position += moveDirection * moveSpeed.ValueRO.Value * SystemAPI.Time.DeltaTime;
            physicVelocity.ValueRW.Linear = moveDirection * moveSpeed.ValueRO.Value;
            physicVelocity.ValueRW.Angular = float3.zero;
        }
    }
}
