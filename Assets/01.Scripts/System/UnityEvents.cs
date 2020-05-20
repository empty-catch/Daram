using System;
using UnityEngine.Events;

[Serializable]
public class IntEvent : UnityEvent<int> { }

[Serializable]
public class IntIntEvent : UnityEvent<int, int> { }
