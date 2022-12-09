using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    Dictionary<string, object> objectValues = new Dictionary<string, object>();

    public void AddValue(string key, object value)
    {
        if (!objectValues.ContainsKey(key))
        {
            objectValues.Add(key, value);
        }
    }

    public void SetValue(string key, object value)
    {
        if (objectValues.ContainsKey(key))
        {
            objectValues[key] = value;
        }
        else
        {
            AddValue(key, value);
        }
    }

    public object GetValue(string key)
    {
        if (objectValues.ContainsKey(key))
        {
            return objectValues[key];
        }
        else
        {
            return default(object);
        }
    }

    public void RemoveValue(string key)
    {
        if (objectValues.ContainsKey(key))
        {
            objectValues.Remove(key);
        }
    }
}