using System.Collections.Generic;
using UnityEngine;

// Todo: Make this to scriptable object, so we can maintain it in editor?
// Todo: Use addressables instead of resources
public class MvvmDatabase : IMvvmDatabase
{
    private readonly Dictionary<MvvmKey, string> m_mvvmKeyValuePairs = new()
    {
        { MvvmKey.ExampleMvvm, "ExampleLifetimeScope" },
        { MvvmKey.AnotherExampleMvvm, "AnotherExampleLifetimeScope" },
    };

    public Object GetMvvmPrefabByKey(MvvmKey key)
    {
        m_mvvmKeyValuePairs.TryGetValue(key, out var value);
        return Resources.Load(value);
    }
}
