using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;




public class ContentBase : MonoBehaviour {

	[SerializeField]
	Text					nameTextComponent = null;

	Action<ContentBase>		m_onTapThis = null;


	/// <summary>
	/// コンテントのフルパス
	/// </summary>
	public string Path
	{
		private set; get;
	}
	public string DisplayName
	{
		private set; get;
	}

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="path">そのファイル/ディレクトリへのパス</param>
	/// <param name="onTap">タップ時アクション</param>
	/// <param name="displayName">表示名</param>
	public virtual void Init(string path, Action<ContentBase> onTap, string displayName=null)
	{
		// 名前
		if( string.IsNullOrEmpty(displayName) )
		{
			displayName = System.IO.Path.GetFileName( path );
		}
		nameTextComponent.text = displayName;

		// タップ時のアクション
		var buttonComponent = GetComponent<Button>();
		if( CommonUtility.CheckNull(buttonComponent) == false )
		{
			buttonComponent.onClick.AddListener(OnTapThis);
		}

		Path = path;
		DisplayName = displayName;
		m_onTapThis = onTap;
	}

	protected virtual void OnTapThis()
	{
		if( m_onTapThis != null )
		{
			m_onTapThis( this );
		}
	}
}
