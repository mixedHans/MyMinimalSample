using System.Collections.Generic;

public class ApplicationStateDatabase : IApplicationStateDatabase
{
    private readonly List<ApplicationState> m_stateList = new()
    {
        { new(ApplicationStateKey.Empty) },
        { new(ApplicationStateKey.Example, MvvmKey.ExampleMvvm) },
        { new(ApplicationStateKey.AnotherExample, MvvmKey.AnotherExampleMvvm) },
        { new(ApplicationStateKey.YetAnotherExample, MvvmKey.ExampleMvvm, MvvmKey.AnotherExampleMvvm) },
    };

    public ApplicationState GetStateByKey(ApplicationStateKey key)
    {
        foreach (ApplicationState state in m_stateList)
        {
            if (state.StateKey == key)
                return state;
        }

        throw new KeyNotFoundException("Found no matching state! Please add ApplicationState to ApplicationStateDatabase");
    }
}
