using System;
using System.Collections.Generic;

namespace BehaveN
{
	public class Blackboard {
		Dictionary<string, object> values;

		public Blackboard() {
			values = new Dictionary<string, object>();
		}

		public Blackboard(Dictionary<string, object> initialValues) {
			values = new Dictionary<string, object>(initialValues);
		}

		public T Get<T>(Enum name) {
			return Get<T>(EnumValueToString(name));
		}

		public T Get<T>(string name) {
			object result;
			if (values.TryGetValue(name, out result)) {
				return (T)result;
			}
			else {
				throw new InvalidOperationException("Blackboard does not contain a value for " + name);
			}
		}

		public T GetOrCreate<T>(Enum name) {
			return GetOrCreate<T>(EnumValueToString(name));
		}

		public T GetOrCreate<T>(string name) {
			object result;
			if (!values.TryGetValue(name, out result)) {
				result = default(T);
				values[name] = result;
			}
			return (T)result;
		}

		public void Set(Enum name, object value) {
			Set(EnumValueToString(name), value);
		}

		public void Set(string name, object value) {
			values[name] = value;
		}

		string EnumValueToString(Enum value) {
			return string.Format("{0}.{1}", value.GetType(), value);
		}
	}
}