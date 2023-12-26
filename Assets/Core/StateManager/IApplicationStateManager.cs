public interface IApplicationStateManager
{
    void SetState(ApplicationStateKey newStateKey);
    void AddMvvm(MvvmKey mmvmKey);
    void RemoveMvvm(IViewModel mmvm);
}