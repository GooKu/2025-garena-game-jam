using System;
using System.Collections.Generic;

namespace GModule
{
    public class MessageEventSystem
    {
        private static Dictionary<string, List<Action<string, object[]>>> dic = new();

        public static void Register(string key, Action<string, object[]> action)
        {
            if (!dic.TryGetValue(key, out var list))
            {
                list = new List<Action<string, object[]>>();
                dic.Add(key, list);
            }

            list.Add(action);
        }

        public static void Notify(string key, params object[] paras)
        {
            if (!dic.TryGetValue(key, out var list))
            {
                return;
            }

            Queue<Action<string, object[]>> actionsQueue = new();
            HashSet<Action<string, object[]>> finishActions = new();
            foreach (var l in list)
            {
                actionsQueue.Enqueue(l);
            }

            int count = actionsQueue.Count;
            int orgCount = list.Count;

            while (count > 0)
            {
                var action = actionsQueue.Dequeue();
                action.Invoke(key, paras);
                finishActions.Add(action);

                if(orgCount != list.Count)
                {
                    actionsQueue.Clear();
                    foreach (var l in list) 
                    {
                        if (finishActions.Contains(l)) { continue; }
                        actionsQueue.Enqueue(l);
                    }
                    orgCount = list.Count;
                }
                count = actionsQueue.Count;
            }
        }

        public static void Deregister(string key, Action<string, object[]> action)
        {
            if (!dic.TryGetValue(key, out var list))
            {
                return;
            }

            list.Remove(action);
        }

        public static void DeregisterAll(string key, Action<string, object[]> action)
        {
            if (!dic.TryGetValue(key, out var list))
            {
                return;
            }

            while (list.Contains(action))
            {
                list.Remove(action);
            }
        }
    }
}