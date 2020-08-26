using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.Events
{
	public static class EventDispatcher
	{	
		private static Dictionary<string, EventHandler> eventHandlers = new Dictionary<string, EventHandler>();

		public static void RemoveAllListeners()
		{
			eventHandlers.Clear();
		}

		public static void AddListener(string eventID, EventHandler eventHandler)
		{
			if (string.IsNullOrEmpty(eventID) || eventHandler == null)
			{
				return;
			}

			if (!eventHandlers.ContainsKey(eventID))
			{
				eventHandlers.Add(eventID, null);
			}
			eventHandlers[eventID] += eventHandler;
		}

		public static void RemoveListener(string eventID, EventHandler eventHandler)
		{
			if (eventHandlers.ContainsKey(eventID))
			{
				eventHandlers[eventID] -= eventHandler;

				if (eventHandlers[eventID] == null)
				{
					eventHandlers.Remove(eventID);
				}
			}
		}

		public static void RaiseEvent(string eventID, object sender)
		{
			EventData eventData = EventData.Alloate();
			eventData.EventID = eventID;
			eventData.Sender = sender;

			RaiseEvent(eventData);

			EventData.Release(eventData);
		}

		public static void RaiseEvent(IEventData eventData)
		{
			if (eventData  == null)
			{
				return;
			}

			eventData.Sent = true;
			eventHandlers[eventData.EventID](eventData);
		}
	}
}
