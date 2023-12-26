using UnityEngine;

public readonly struct OffsetToSpawnPose
{
    public OffsetToSpawnPose(Vector3 positionOffset, Quaternion rotationOffset)
    {
        PositionOffset = positionOffset;
        RotationOffset = rotationOffset;
    }

    public Vector3 PositionOffset { get; }
    public Quaternion RotationOffset { get; }
}
