namespace MyLibrary.Events
{
	public abstract class EventInfo<T> : IEventData where T : EventInfo<T>, new()
	{
		public string EventID { get; set; }
		public object Sender { get; set; }
		public bool Sent { get; set; }
		public bool Handled { get; set; }

		public virtual void Clear()
		{
			EventID = "";
			Sender = null;
			Sent = false;
			Handled = false;
		}

		private static ObjectPool<T> pool = new ObjectPool<T>(25, 10);

		public static T Alloate()
		{
			T eventInfo = pool.AllocateObject();
			eventInfo.Sent = false;
			eventInfo.Handled = false;

			return eventInfo;
		}


		public static void Release(T instance)
		{
			if (instance == null)
			{
				return;
			}

			instance.Clear();

			instance.Sent = true;
			instance.Handled = true;

			pool.Release(instance);
		}
	}
}
