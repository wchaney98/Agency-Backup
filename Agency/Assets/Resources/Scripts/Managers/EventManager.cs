using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventArgsExtend : System.EventArgs { }

public enum EventChannels
{
    InGame
}
public enum InGameChannelEvents
{
    Thing
}

public class ChannelEnums
{
    public static Dictionary<EventChannels, System.Array> GetChannelEnumList()
    {
        Dictionary<EventChannels, System.Array> enumChannelEventList = new Dictionary<EventChannels, System.Array>();
        enumChannelEventList.Add(EventChannels.InGame, System.Enum.GetValues(typeof(InGameChannelEvents)));
        return enumChannelEventList;
    }
}

public class EventHandlerManager : SingletonBehavior<EventHandlerManager>
{
    public delegate void gameEventHandler(EventArgsExtend e);

    static Dictionary<EventChannels, Dictionary<Enum, gameEventHandler>> ListenerFunctions = InitializeDicts();

    public void BroadCast(EventChannels evType, Enum ev, EventArgsExtend e)
    {
        ListenerFunctions[evType][ev](e);
    }

    public void AddListener(EventChannels evType, Enum ev, gameEventHandler eventListener)
    {
        ListenerFunctions[evType][ev] += eventListener;
    }

    public void RemoveListener(EventChannels evType, Enum ev, gameEventHandler eventListener)
    {
        ListenerFunctions[evType][ev] -= eventListener;
    }

    static Dictionary<EventChannels, Dictionary<Enum, gameEventHandler>> InitializeDicts()
    {
        Dictionary<EventChannels, Array> enumChannelEventList = ChannelEnums.GetChannelEnumList();
        Dictionary<EventChannels, Dictionary<Enum, gameEventHandler>> result = new Dictionary<EventChannels, Dictionary<Enum, gameEventHandler>>();
        foreach (var val in (EventChannels[])Enum.GetValues(typeof(EventChannels)))
        {
            result.Add(val, new Dictionary<Enum, gameEventHandler>());
            foreach (var ev in enumChannelEventList[val])
            {
                result[val].Add((Enum)ev, new gameEventHandler(delegate (EventArgsExtend e) { }));
            }
        }
        return result;
    }

    private void OnDestroy()
    {
        ListenerFunctions = InitializeDicts();
    }
}
