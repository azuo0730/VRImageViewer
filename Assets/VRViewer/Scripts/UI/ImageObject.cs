using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		var size = new Vector3(height*aspectRatio, height, 1.0f);
		scaleTransform.localScale = size;

		var layoutElement = GetComponent<LayoutElement>();
		if( CommonUtility.CheckNull(layoutElement) ) return;
		layoutElement.preferredWidth = size.x;
		layoutElement.preferredHeight = size.y;
	}

}
