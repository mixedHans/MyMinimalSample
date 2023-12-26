using UnityEngine;

public class TransitionService : ITransitionService
{
    public Awaitable TransitionIn(Pose from = default, Pose to = default)
    {
        var completionSource = new AwaitableCompletionSource();
        return completionSource.Awaitable;
    }

    public Awaitable Transition(Pose from = default, Pose to = default)
    {
        var completionSource = new AwaitableCompletionSource();
        return completionSource.Awaitable;
    }

    public Awaitable TransitionOut(Pose from = default, Pose to = default)
    {
        var completionSource = new AwaitableCompletionSource();
        return completionSource.Awaitable;
    }
}
