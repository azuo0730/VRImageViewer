using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FileContent : ContentBase
{
	[SerializeField]
	Image					thumbnailImage = null;

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="path">そのファイル/ディレクトリへのパス</param>
	/// <param name="onTap">タップ時アクション</param>
	/// <param name="displayName">表示名</param>
	public override void Init(string path, Action<ContentBase> onTap, string displayName=null)
	{
		base.Init(path, onTap, displayName);

		// もし画像ファイルなら, サムネイルを作成する
		thumbnailImage.enabled = false;
		if( NOB.File.GetInstance.IsImageFileFromFileName(path) )
		{
			StartCoroutine(NOB.File.GetInstance.LoadTexture_IE("file://" + path, OnSuccessfulLoadTexture, null));
		}
	}

	/// <summary>
	/// サムネイル画像読み込み成功時処理
	/// </summary>
	/// <param name="tex"></param>
	void OnSuccessfulLoadTexture(Texture2D tex)
	{
		thumbnailImage.sprite = CommonUtility.Texture2DToSprite(tex);
		thumbnailImage.enabled = true;
	}
}
