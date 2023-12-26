using System.Collections.Generic;

public interface IMvvmRegistry
{
    List<Mvvm> ActiveMvvmList { get; }

    bool IsRegistered(MvvmKey mvvmKey);
    void Register(Mvvm mvvm);
    void Unregister(Mvvm mvvm);
}