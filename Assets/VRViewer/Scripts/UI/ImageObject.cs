using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageObject : MonoBehaviour
{
	[SerializeField]
	RectTransform							scaleTransform = null;
	[SerializeField]
	Image									drawImage = null;
	[SerializeField]
	bool									billboard = false;
	[SerializeField]
	Camera									billboardTargetCamera = null;				// (billboard == true && billboardTargetCamera == null) 時は, mainCameraを注視する

	/// <summary>
	/// 画像オブジェクトを初期化する
	/// </summary>
	/// <param name="tex"></param>
	public void Init(Texture2D tex, Action<ImageObject> onTapThis, float height)
	{
		var button = GetComponent<Button>();
		if( CommonUtility.CheckNull(button) ) return;
		if( onTapThis != null ) button.onClick.AddListener(()=>{ onTapThis(this); });

		// 画像の登録
		drawImage.sprite = CommonUtility.Texture2DToSprite(tex);

		// アス比を調整
		float aspectRatio = (float)tex.width / (float)tex.height;
		var sizeDelta = new Vector2(height*aspectRatio, height);
		scaleTransform.sizeDelta = sizeDelta;

		var layoutElement = GetComponent<LayoutElement>();
		if( CommonUtility.CheckNull(layoutElement) ) return;
		layoutElement.preferredWidth = sizeDelta.x;
		layoutElement.preferredHeight = sizeDelta.y;
	}

	private void Update()
	{
		if( billboard )
		{
			Camera targetCamera = billboardTargetCamera;
			if( targetCamera == null ) targetCamera = Camera.main;

			Vector3 targetPos = targetCamera.transform.position;
			targetPos.y = drawImage.transform.position.y;											// y軸は回転しない
			drawImage.transform.LookAt(targetPos);
			drawImage.transform.Rotate(0f, 180f, 0f);												// 普通にやると左右逆になっちゃうので, 反対を向かせる
		}else{
			drawImage.transform.rotation = Quaternion.identity;
		}
	}
}
