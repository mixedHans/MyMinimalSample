using System.Collections.Generic;
using System.Linq;

public struct ApplicationState
{
    public ApplicationStateKey StateKey;
    public List<MvvmKey> MvvmKeys;

    public ApplicationState(ApplicationStateKey stateKey, params MvvmKey[] mvvmKeys)
    {
        StateKey = stateKey;
        MvvmKeys = mvvmKeys.ToList();
    }
}