using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.Diagnostics;


public static class CommonUtility
{
	public enum LogType
	{
		Info,
		Warning,
		Error,
	}
	private static readonly Action<object>[] s_logMethod = new Action<object>[]
	{
		UnityEngine.Debug.Log,							// Info
		UnityEngine.Debug.LogWarning,					// Warning
		UnityEngine.Debug.LogError,						// Red
	};

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

	/// <summary>
	/// ログを表示する
	/// </summary>
	/// <param name="type">ログタイプ</param>
	/// <param name="str">表示するログ</param>
	public static void Log(LogType type, string str)
	{
		DateTime currentDateTime = DateTime.Now;

		string typeStr = string.Format("[{0}]", type.ToString().Substring(0, 1));
		string drawStr = string.Format("{0} {1}: {2}", currentDateTime.ToString("yyyy/MM/dd HH:mm:ss"), typeStr, str);

		var logMethod = s_logMethod[(int)type];
		logMethod(drawStr);
	}

	/// <summary>
	/// Nullチェックする (Nullの場合, ログを出力する)
	/// </summary>
	/// <param name="obj">Nullチェックしたい変数</param>
	/// <param name="onNullLogType">Nullだった場合の出力ログタイプ</param>
	/// <returns>nullならばtrue, nullでなければfalseを返す</returns>
	public static bool CheckNull<T>(T obj, LogType onNullLogType=LogType.Error)
	{
		// StackFrameクラスをインスタンス化する
		StackFrame objStackFrame = new StackFrame(1);			// フレーム数:1なら直接呼び出したメソッド 
		// 呼び出し元のメソッド名を取得する
		string refMethodName = objStackFrame.GetMethod().Name;

		if( IsOfNullableType(obj) )
		{
			if( obj == null )
			{
				Log(onNullLogType, string.Format("Illegal null was detected. Method Name[{0}]", refMethodName));
				return true;
			}
		}
		return false;
	}
	private static bool IsOfNullableType<T>(T o)
	{
		var type = typeof(T);
		return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
	}

	/// <summary>
	/// Texture2D型を, Sprite型に変換する
	/// </summary>
	/// <param name="tex"></param>
	/// <returns></returns>
	public static Sprite Texture2DToSprite(Texture2D tex)
	{
		return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
	}
}
