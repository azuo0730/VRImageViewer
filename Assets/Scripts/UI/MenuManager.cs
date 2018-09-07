using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	GameObject					fileContentPrefab = null;
	[SerializeField]
	GameObject					directoryContentPrefab = null;

	[SerializeField]
	Transform					fileContentTargetTransform = null;
	[SerializeField]
	ScrollRect					contentSctollRect = null;


	private string				m_currentSearchPath;

	// Use this for initialization
	void Start ()
	{
		m_currentSearchPath = Application.dataPath;
		ShowDirectoryAndFileList( m_currentSearchPath );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// 指定パスのディレクトリとファイルの一覧を表示する
	/// </summary>
	/// <param name="path">パス</param>
	void ShowDirectoryAndFileList(string path)
	{
		// いったん現状のコンテントを全て削除
		CommonUtility.RemoveAllChild( fileContentTargetTransform );

		// 親フォルダ
		var parentDirectory = Directory.GetParent( path );
		if( parentDirectory != null )
		{
			CreateDirectoryContent(parentDirectory.FullName, OnTapDirectoryContent, "...");
		}

		// サブフォルダ一覧
		DirectoryInfo dir = new DirectoryInfo( path );
		var subFolderTable = dir.GetDirectories();
		foreach(var fileInfo in subFolderTable)
		{
			CreateDirectoryContent(fileInfo.FullName, OnTapDirectoryContent);
		}

		// ファイル一覧
		var fileTable = dir.GetFiles();
		foreach(var fileInfo in fileTable)
		{
			CreateFileContent(fileInfo.FullName, OnTapFileContent);
		}

		// スクロール位置リセット
		contentSctollRect.verticalNormalizedPosition = 1.0f;
		contentSctollRect.horizontalNormalizedPosition = 1.0f;
	}

	/// <summary>
	/// フォルダコンテントタップ時処理
	/// </summary>
	/// <param name="content"></param>
	void OnTapDirectoryContent(ContentBase content)
	{
		var newPath = content.Path;
		if( !string.IsNullOrEmpty( newPath ) )
		{
			ShowDirectoryAndFileList( newPath );
		}
	}
	/// <summary>
	/// フォルダコンテントを作成する
	/// </summary>
	/// <param name="path">パス</param>
	/// <param name="onTap">タップ時アクション</param>
	/// <param name="displayName">表示名</param>
	void CreateDirectoryContent(string path, Action<ContentBase> onTap, string displayName=null)
	{
		var obj = Instantiate(directoryContentPrefab, fileContentTargetTransform);
		var contentComponent = obj.GetComponent<ContentBase>();
		contentComponent.Init(path, onTap, displayName);
	}

	/// <summary>
	/// ファイルコンテントタップ時処理
	/// </summary>
	/// <param name="content"></param>
	void OnTapFileContent(ContentBase content)
	{
	}
	/// <summary>
	/// ファイルコンテントを作成する
	/// </summary>
	/// <param name="path">パス</param>
	/// <param name="onTap">タップ時アクション</param>
	/// <param name="displayName">表示名</param>
	void CreateFileContent(string path, Action<ContentBase> onTap, string displayName=null)
	{
		var obj = Instantiate(fileContentPrefab, fileContentTargetTransform);
		var contentComponent = obj.GetComponent<ContentBase>();
		contentComponent.Init(path, onTap, displayName);
	}
}
