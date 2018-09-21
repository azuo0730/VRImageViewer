using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FileContent : ContentBase
{
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
		if( NOB.File.GetInstance.IsImageFileFromFileName(path) )
		{
			StartCoroutine(NOB.File.GetInstance.LoadTexture_IE("file://" + path, OnSuccessfulLoadThumbnail, null));
		}
	}
}
