using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IMvvmFactory, MvvmFactory>(Lifetime.Singleton);
        builder.Register<IMvvmDatabase, MvvmDatabase>(Lifetime.Singleton);
        builder.Register<IMvvmRegistry, MvvmRegistry>(Lifetime.Singleton);

        builder.Register<IApplicationStateDatabase, ApplicationStateDatabase>(Lifetime.Singleton);
        builder.Register<IApplicationStateManager, ApplicationStateManager>(Lifetime.Singleton);

        builder.Register<ITransitionService, TransitionService>(Lifetime.Singleton);

        builder.RegisterBuildCallback(container => 
        { 
            Debug.Log($"[{GetType()}] Registered Project LTS");
        });
    }

    private async void Start()
    {
        var appStateManager = Container.Resolve<IApplicationStateManager>();
        appStateManager.SetState(ApplicationStateKey.Example);
        await Awaitable.WaitForSecondsAsync(2);
        appStateManager.SetState(ApplicationStateKey.AnotherExample);
        appStateManager.AddMvvm(MvvmKey.ExampleMvvm);
        appStateManager.AddMvvm(MvvmKey.AnotherExampleMvvm);
        await Awaitable.WaitForSecondsAsync(2);
        appStateManager.SetState(ApplicationStateKey.Empty);
        await Awaitable.WaitForSecondsAsync(2);
        appStateManager.SetState(ApplicationStateKey.YetAnotherExample);
        await Awaitable.WaitForSecondsAsync(2);
        appStateManager.SetState(ApplicationStateKey.Example);
        await Awaitable.WaitForSecondsAsync(2);
        appStateManager.SetState(ApplicationStateKey.AnotherExample);
    }
}