using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance = null;

	public static T FindInstance()
	{
		if( instance == null )
		{
			instance = GameObject.FindObjectOfType<T>();
		}
		
		return instance;
	}

	public static T GetOrCreateInstance()
	{
		if( instance == null )
		{
			instance = FindInstance();
			if( instance == null )
			{
				Debug.Log(string.Format("Create MonoSingleton (instance type : {0})", typeof(T).ToString()));
				instance = new GameObject("Generated " + typeof(T).ToString()).AddComponent<T>();
				
				if( CommonUtility.CheckNull(instance) ) return null;
			}
		}
		return instance;
	}

	public static T GetInstance
	{
		get
		{
			return GetOrCreateInstance();
		}
	}
}
