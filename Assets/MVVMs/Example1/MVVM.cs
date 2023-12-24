using UnityEngine;
using VContainer.Unity;

public class MVVM
{
    public MVVM(MvvmKey key, LifetimeScope lifetimeScope, IViewModel viewModel, IModel model)
    {
        Key = key;
        LifetimeScope = lifetimeScope;
        ViewModel = viewModel;
        Model = model;
        Debug.Log($"[{key}] Constructor called");
    }

    public MvvmKey Key { get; }
    public LifetimeScope LifetimeScope { get; }
    public IViewModel ViewModel { get; }
    public IModel Model { get; }
}
