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
	GameObject					imageObjectPrefab = null;

	[SerializeField]
	Transform					fileContentTargetTransform = null;
	[SerializeField]
	ScrollRect					contentSctollRect = null;
	[SerializeField]
	Transform					imageObjectTargetTransform = null;

	[SerializeField]
	MenuToggleBtn				menuToggleBtn = null;

	private string				m_currentSearchPath;


	private float m_imageObjectHeight = 400.0f;
	public float ImageObjectHeight
	{
		set
		{
			m_imageObjectHeight = value;
		}
		get
		{
			return m_imageObjectHeight;
		}
	}

	// Use this for initialization
	void Start ()
	{
		m_currentSearchPath = Application.dataPath;
		ShowDirectoryAndFileList( m_currentSearchPath );

		menuToggleBtn.Open();
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
		CreateImageObj(content.Path);
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
		if( CommonUtility.CheckNull(contentComponent) ) return;

		contentComponent.Init(path, onTap, displayName);
	}

	void CreateImageObj(string path)
	{
		StartCoroutine(LoadTexture_IE(path, OnSuccessfulLoadTexture, null));
	}
	void OnSuccessfulLoadTexture(Texture2D tex)
	{
		var instance = Instantiate(imageObjectPrefab, imageObjectTargetTransform);
		if( CommonUtility.CheckNull(instance) ) return;

		var imageObject = instance.GetComponent<ImageObject>();
		if( CommonUtility.CheckNull(imageObject) ) return;

		imageObject.Init(tex, m_imageObjectHeight);
	}

	/// <summary>
	/// 画像の読み込み
	/// </summary>
	/// <param name="path">読み込みたい画像のパス</param>
	/// <param name="onFinish">読み込み完了時</param>
	/// <returns></returns>
	IEnumerator LoadTexture_IE(string path, Action<Texture2D> onSuccessful, Action onFailed)
	{
		var www = new WWW( "file://" + path);
		yield return www;

		if( string.IsNullOrEmpty(www.error) )
		{
			if( onSuccessful != null )
			{
				onSuccessful( www.texture );
			}
		}else{
			if( onFailed != null )
			{
				onFailed();
			}
		}
	}
}
