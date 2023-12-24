using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MvvmFactory
{
    private readonly MvvmDatabase m_mvvmDatabase;

    public MvvmFactory(MvvmDatabase mvvmDatabase)
    {
        this.m_mvvmDatabase = mvvmDatabase;
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public MVVM Create(MvvmKey mvvmKey) 
    {
        Debug.Log($"[{GetType()}] Spawn MVVM: {mvvmKey}");
        m_mvvmDatabase.mvvmKeyValuePairs.TryGetValue(mvvmKey, out var mvvmResourceName);
        var lifetimeScopePrefab = Resources.Load(mvvmResourceName).GetComponent<LifetimeScope>();
        lifetimeScopePrefab.autoRun = false;
        var lifetimeScope = GameObject.Instantiate(lifetimeScopePrefab);
        lifetimeScope.Build();
        var viewModel = lifetimeScope.Container.Resolve<IViewModel>();
        var model = lifetimeScope.Container.Resolve<IModel>();
        return new MVVM(mvvmKey, lifetimeScope, viewModel, model); 
    }
}
