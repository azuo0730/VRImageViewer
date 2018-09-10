using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageObject : MonoBehaviour
{
	[SerializeField]
	MeshRenderer						meshRenderer = null;
	[SerializeField]
	Transform							scaleTransform = null;


	/// <summary>
	/// 画像オブジェクトを初期化する
	/// </summary>
	/// <param name="tex"></param>
	public void Init(Texture2D tex, float height)
	{
		meshRenderer.material.mainTexture = tex;

		float aspectRatio = (float)tex.width / (float)tex.height;

		// アス比を調整
		scaleTransform.localScale = new Vector3(height*aspectRatio, height, 1.0f);
	}

}
