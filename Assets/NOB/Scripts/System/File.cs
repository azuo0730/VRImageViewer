using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOB {


	public class File : ISingleton<File>
	{
		/// <summary>
		/// 画像の読み込み
		/// </summary>
		/// <param name="path">読み込みたい画像のパス</param>
		/// <param name="onFinish">読み込み完了時に呼ばれるアクション</param>
		/// <returns></returns>
		public IEnumerator LoadTexture_IE(string path, Action<Texture2D> onSuccessful, Action onFailed)
		{
			var www = new WWW( path );
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

		/// <summary>
		/// ファイル名(拡張子)から, そのファイルが画像ファイルかどうかを調べる (※厳密にいうと, Unityにアセットとして突っ込んだ時に, Texture2Dとして読み込まれる拡張子かどうかを判定する)
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <returns>true:画像ファイル, false:画像ファイルではない</returns>
		public bool IsImageFileFromFileName(string fileName)
		{
			return System.Text.RegularExpressions.Regex.IsMatch(fileName, ".+\\.(psd|tif|tiff|jpg|tga|png|gif|bmp|iff|pict|pdf)$");
		}
	}





}
