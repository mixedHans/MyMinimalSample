using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

public class MvvmFactory : IMvvmFactory
{
    private readonly IMvvmDatabase m_mvvmDatabase;

    public MvvmFactory(IMvvmDatabase mvvmDatabase)
    {
        this.m_mvvmDatabase = mvvmDatabase;
        Debug.Log($"[{GetType()}] Constructor called");
    }

    public Mvvm /*async Awaitable<Mvvm>*/ Create(MvvmKey mvvmKey)
    {
        Debug.Log($"[{GetType()}] Spawn MVVM: {mvvmKey}");

        // Todo: Swap resources for addressables
        Object mvvmPrefab = m_mvvmDatabase.GetMvvmPrefabByKey(mvvmKey);

        // Setup the prefab
        var lifetimeScopeComponent = mvvmPrefab.GetComponent<LifetimeScope>();
        lifetimeScopeComponent.autoRun = false;

        // Instantiate and build the lifetimescope
        var lifetimeScope = GameObject.Instantiate(lifetimeScopeComponent);
        lifetimeScope.Build();

        // Resolve and assign mvvm
        var viewModel = lifetimeScope.Container.Resolve<IViewModel>();
        var model = lifetimeScope.Container.Resolve<IModel>();

        // Resolve and assign the UI document for data binding
        var uiDocument = lifetimeScope.Container.Resolve<UIDocument>();
        uiDocument.rootVisualElement.dataSource = viewModel;

        // Await transition in --> proposal for transition
        //await lifetimeScope.GetComponent<TransitionBehaviour>().TransitionHandle;

        return new Mvvm(mvvmKey, viewModel, model, lifetimeScope.gameObject);
    }
}