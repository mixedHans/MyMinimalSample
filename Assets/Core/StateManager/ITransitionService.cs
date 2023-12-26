using UnityEngine;

public interface ITransitionService
{
    Awaitable Transition(Pose from = default, Pose to = default);
    Awaitable TransitionIn(Pose from = default, Pose to = default);
    Awaitable TransitionOut(Pose from = default, Pose to = default);
}