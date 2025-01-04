using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
partial struct UnitMoverSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach ((RefRW<LocalTransform> localTransform, RefRO<MoveSpeed> moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveSpeed>>())
        {
            localTransform.ValueRW.Position = localTransform.ValueRO.Position + new float3(moveSpeed.ValueRO.Value, 0, 0) * SystemAPI.Time.DeltaTime;
        }
    }
}