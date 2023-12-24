using UnityEngine;
using VContainer;
using VContainer.Unity;

public class AnotherExampleLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IModel, ExampleModel>(Lifetime.Scoped);
        builder.Register<IViewModel, ExampleViewModel>(Lifetime.Scoped);

        builder.RegisterBuildCallback(container =>
        {
            Debug.Log($"[{GetType()}] Registered Example LTS");
        });
    }

    protected override void OnDestroy()
    {
        Debug.Log($"[{GetType()}] Destroyed");
        base.OnDestroy();
    }
}
