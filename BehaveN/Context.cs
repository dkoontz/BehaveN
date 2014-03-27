using System;
using System.Collections.Generic;

namespace BehaveN
{
	public class Context {
		Dictionary<string, object> values;

		public Context() {
			values = new Dictionary<string, object>();
		}

		public Context(Dictionary<string, object> initialValues) {
			values = new Dictionary<string, object>(initialValues);
		}

		public T Get<T>(string name) {
			object result;
			if (values.TryGetValue(name, out result)) {
				return (T)result;
			}
			else {
				throw new InvalidOperationException("Context does not contain a value for " + name);
			}
		}

		public T GetOrCreate<T>(string name) {
			object result;
			if (!values.TryGetValue(name, out result)) {
				result = default(T);
				values[name] = result;
			}
			return (T)result;
		}

		public void Set(string name, object value) {
			values[name] = value;
		}
	}
}