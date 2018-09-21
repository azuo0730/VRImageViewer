using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageObject : MonoBehaviour
{
	[SerializeField]
	RectTransform							scaleTransform = null;

	/// <summary>
	/// 画像オブジェクトを初期化する
	/// </summary>
	/// <param name="tex"></param>
	public void Init(Texture2D tex, Action<ImageObject> onTapThis, float height)
	{
		var button = GetComponent<Button>();
		if( CommonUtility.CheckNull(button) ) return;
		button.image.sprite = CommonUtility.Texture2DToSprite(tex);
		if( onTapThis != null ) button.onClick.AddListener(()=>{ onTapThis(this); });

		// アス比を調整
		float aspectRatio = (float)tex.width / (float)tex.height;
		var sizeDelta = new Vector2(height*aspectRatio, height);
		scaleTransform.sizeDelta = sizeDelta;

		var layoutElement = GetComponent<LayoutElement>();
		if( CommonUtility.CheckNull(layoutElement) ) return;
		layoutElement.preferredWidth = sizeDelta.x;
		layoutElement.preferredHeight = sizeDelta.y;
	}
}
