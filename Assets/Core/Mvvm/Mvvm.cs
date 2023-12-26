using UnityEngine;

// Todo: Interface for mvvm?
public class Mvvm
{
    public Mvvm(MvvmKey key, IViewModel viewModel, IModel model, GameObject gameObject)
    {
        Key = key;
        ViewModel = viewModel;
        Model = model;
        GameObject = gameObject;
        Debug.Log($"[{key}] Constructor called");
    }

    public MvvmKey Key { get; }
    public IViewModel ViewModel { get; }
    public IModel Model { get; }
    public GameObject GameObject { get; }
}
