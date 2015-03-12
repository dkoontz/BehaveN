using System;

// These attributes are used to enable editor support
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class BehaviorTreeNodeCollectionAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class BehaviorTreeNodeAttribute : Attribute {
	public readonly string NodeName;

	public BehaviorTreeNodeAttribute(string nodeName) {
		NodeName = nodeName;
	}
}