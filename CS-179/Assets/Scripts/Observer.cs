using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationType
{
    PLAYER_DEAD
};

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(NotificationType type);
}

public abstract class Subject : MonoBehaviour
{
    private List<Observer> observers = new List<Observer>();

    public void AddObserver(Observer newObserver)
    {
        observers.Add(newObserver);
    }

    public void Notify(NotificationType type)
    {
        foreach(Observer watchdog in observers)
        {
            watchdog.OnNotify(type);
        }
    }
}
