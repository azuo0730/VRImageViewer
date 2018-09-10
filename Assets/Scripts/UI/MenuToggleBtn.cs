using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuToggleBtn : MonoBehaviour
{
	[SerializeField]
	string					onCloseBtnLabel = "";
	[SerializeField]
	string					onOpenBtnLabel = "";

	[SerializeField]
	Animation				menuRootAnimation = null;
	[SerializeField]
	Text					btnTextComponent = null;

	readonly string			OPEN_ANIMATION_NAME = "OpenMenu";
	readonly string			CLOSE_ANIMATION_NAME = "CloseMenu";

	bool					m_currentOpenFlag = false;

	private void Start()
	{
		var btn = GetComponent<Button>();
		btn.onClick.AddListener(OnTapThis);
		// 最初は開く
		Open();
	}

	/// <summary>
	/// ボタン押下時処理 (トグルでメニューをOpen/Closeする)
	/// </summary>
	void OnTapThis()
	{
		if( m_currentOpenFlag )
		{
			Close();
		}else{
			Open();
		}
	}

	/// <summary>
	/// メニューを
	/// </summary>
	public void Open()
	{
		m_currentOpenFlag = true;
		menuRootAnimation.Play( OPEN_ANIMATION_NAME );
		btnTextComponent.text = onOpenBtnLabel;
	}

	public void Close()
	{
		m_currentOpenFlag = false;
		menuRootAnimation.Play( CLOSE_ANIMATION_NAME );
		btnTextComponent.text = onCloseBtnLabel;
	}
}
