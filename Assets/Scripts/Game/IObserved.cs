using System.Collections.Generic;

public interface IObserved
{


    public abstract void AddObserver(IObserver observer);
    public abstract void RemoveObserver(IObserver observer);
    public abstract void NotifyObservers();

}