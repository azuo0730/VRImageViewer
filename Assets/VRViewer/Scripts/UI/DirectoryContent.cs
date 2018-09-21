using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectoryContent : ContentBase
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

		DirectoryInfo dir = new DirectoryInfo( path );
		// ファイル一覧
		var fileTable = dir.GetFiles();
		foreach(var fileInfo in fileTable)
		{
			// もし画像ファイルが入っているなら, サムネイルを作成する
			if( NOB.File.GetInstance.IsImageFileFromFileName(fileInfo.FullName) )
			{
				StartCoroutine(NOB.File.GetInstance.LoadTexture_IE("file://" + fileInfo.FullName, OnSuccessfulLoadThumbnail, null));
				break;						// 最初に見つかった画像ファイルでサムネイル作ったら終わり
			}
		}

	}
}
