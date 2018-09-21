using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;




public class ContentBase : MonoBehaviour {

	[SerializeField]
	protected Text					nameTextComponent = null;
	[SerializeField]
	protected Image					thumbnailImage = null;

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

		// ひとまずサムネイルは非表示に設定しておく (OnSuccessfulLoadThumbnailで表示される)
		thumbnailImage.enabled = false;
	}

	/// <summary>
	/// 自身タップ時処理
	/// </summary>
	protected virtual void OnTapThis()
	{
		if( m_onTapThis != null )
		{
			m_onTapThis( this );
		}
	}

	/// <summary>
	/// サムネイル画像読み込み成功時処理
	/// </summary>
	/// <param name="tex"></param>
	protected void OnSuccessfulLoadThumbnail(Texture2D tex)
	{
		var imageTransform = thumbnailImage.GetComponent<Transform>();
		if( CommonUtility.CheckNull(imageTransform) ) return;

		thumbnailImage.sprite = CommonUtility.Texture2DToSprite(tex);
		// アス比を調整
		float aspectRatio = (float)tex.width / (float)tex.height;
		imageTransform.localScale = new Vector3(aspectRatio, 1.0f, 1.0f);				// 高さはそのままに, 横幅を調整する

		thumbnailImage.enabled = true;
	}
}
