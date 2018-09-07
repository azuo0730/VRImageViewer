using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonUtility
{


	/// <summary>
	/// 全ての子供を削除する
	/// </summary>
	/// <param name="target">ターゲットのTransform</param>
	public static void RemoveAllChild(Transform target)
	{
		foreach ( Transform t in target )
		{
			GameObject.Destroy( t.gameObject );
		}
	}
}
