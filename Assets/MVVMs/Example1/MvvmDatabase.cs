using System.Collections.Generic;

public class MvvmDatabase
{
    public Dictionary<MvvmKey, string> mvvmKeyValuePairs = new()
    {
        { MvvmKey.ExampleMvvm, "ExampleLifetimeScope" },
        { MvvmKey.AnotherExampleMvvm, "AnotherExampleLifetimeScope" },
    };
}
