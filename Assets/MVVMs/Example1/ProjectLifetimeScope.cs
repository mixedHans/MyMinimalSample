using System.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<MvvmFactory>(Lifetime.Singleton);
        builder.Register<MvvmDatabase>(Lifetime.Singleton);
        builder.Register<MvvmRegistry>(Lifetime.Singleton);

        builder.Register<ApplicationStateDatabase>(Lifetime.Singleton);
        builder.Register<ApplicationStateManager>(Lifetime.Singleton);

        builder.RegisterBuildCallback(container => 
        { 
            Debug.Log($"[{GetType()}] Registered Project LTS");
        });
    }

    private async void Start()
    {
        var appStateManager = Container.Resolve<ApplicationStateManager>();
        appStateManager.SetState(ApplicationStateKey.Example);
        await Task.Delay(2000);
        appStateManager.SetState(ApplicationStateKey.AnotherExample);
        await Task.Delay(2000);
        appStateManager.SetState(ApplicationStateKey.YetAnotherExample);
        await Task.Delay(2000);
        appStateManager.SetState(ApplicationStateKey.Example);
    }
}