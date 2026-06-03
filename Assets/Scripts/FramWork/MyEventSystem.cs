using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameEventType
{
    LevelClear,
    MonsDie,
    CoinsChange,
    HpChange,
    DefChange,
    CriChange,
    EnergyChange,
}

public interface IEventInfos { }

public class EventInfos<T, T1> : IEventInfos
{
    public UnityAction<T, T1> unityActions;

    public EventInfos() { }

    public EventInfos(UnityAction<T, T1> unityActions)
    {
        this.unityActions = unityActions;
    }
}

public class EventInfos<T> : IEventInfos
{
    public UnityAction<T> unityActions;

    public EventInfos() { }

    public EventInfos(UnityAction<T> unityActions)
    {
        this.unityActions = unityActions;
    }
}

public class EventInfos : IEventInfos
{
    public UnityAction unityActions;

    public EventInfos() { }

    public EventInfos(UnityAction unityActions)
    {
        this.unityActions = unityActions;
    }
}

/// <summary>
/// 事件中心
/// </summary>
public class MyEventSystem
{
    private static MyEventSystem instance;
    public static MyEventSystem Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }
    private readonly Dictionary<GameEventType, IEventInfos> eventDict = new();

    public void AddEventListener(GameEventType eventType, UnityAction action)
    {
        if (eventDict.TryGetValue(eventType, out IEventInfos existingAction))
        {
            (existingAction as EventInfos).unityActions += action;
        }
        else
        {
            EventInfos eventInfos = new(action);
            eventDict.Add(eventType, eventInfos);
        }
    }

    public void AddEventListener<T>(GameEventType eventType, UnityAction<T> action)
    {
        if (eventDict.TryGetValue(eventType, out IEventInfos existingAction))
        {
            (existingAction as EventInfos<T>).unityActions += action;
        }
        else
        {
            EventInfos<T> eventInfos = new(action);
            eventDict.Add(eventType, eventInfos);
        }
    }

    public void AddEventListener<T, T1>(GameEventType eventType, UnityAction<T, T1> action)
    {
        if (eventDict.TryGetValue(eventType, out IEventInfos existingAction))
        {
            (existingAction as EventInfos<T, T1>).unityActions += action;
        }
        else
        {
            EventInfos<T, T1> eventInfos = new(action);
            eventDict.Add(eventType, eventInfos);
        }
    }

    public void RemoveEventListener(GameEventType eventType, UnityAction action)
    {
        if (eventDict.TryGetValue(eventType, out IEventInfos existingAction))
        {
            (existingAction as EventInfos).unityActions -= action;
        }
        else
        {
            Debug.LogWarning("-------->   " + eventType + " 事件为空,无法被移除");
        }
    }

    public void RemoveEventListener<T>(GameEventType eventType, UnityAction<T> action)
    {
        if (eventDict.TryGetValue(eventType, out IEventInfos existingAction))
        {
            (existingAction as EventInfos<T>).unityActions -= action;
        }
        else
        {
            Debug.LogWarning("-------->   " + eventType + " 事件为空,无法被移除");
        }
    }

    public void RemoveEventListener<T, T1>(GameEventType eventType, UnityAction<T, T1> action)
    {
        if (eventDict.TryGetValue(eventType, out IEventInfos existingAction))
        {
            (existingAction as EventInfos<T, T1>).unityActions -= action;
        }
        else
        {
            Debug.LogWarning("-------->   " + eventType + " 事件为空,无法被移除");
        }
    }

    public void EventTrigger(GameEventType eventType)
    {
        if (eventDict.TryGetValue(eventType, out IEventInfos existingAction))
        {
            (existingAction as EventInfos).unityActions?.Invoke();
        }
        else
        {
            Debug.LogWarning("-------->   " + eventType + " 事件为空,不能被触发");
        }
    }

    public void EventTrigger<T>(GameEventType eventType, T eventData)
    {
        if (eventDict.TryGetValue(eventType, out IEventInfos existingAction))
        {
            (existingAction as EventInfos<T>).unityActions?.Invoke(eventData);
        }
        else
        {
            Debug.LogWarning("-------->   " + eventType + " 事件为空,不能被触发");
        }
    }

    public void EventTrigger<T, T1>(GameEventType eventType, T eventData, T1 eventData1)
    {
        if (eventDict.TryGetValue(eventType, out IEventInfos existingAction))
        {
            (existingAction as EventInfos<T, T1>).unityActions?.Invoke(eventData, eventData1);
        }
        else
        {
            Debug.LogWarning("-------->   " + eventType + " 事件为空,不能被触发");
        }
    }

    public void Clear(GameEventType eventType)
    {
        eventDict.Remove(eventType);
    }

    public void Clear()
    {
        eventDict.Clear();
    }
}
