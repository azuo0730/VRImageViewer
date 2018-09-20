using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public enum ControllerType
	{
		GearVRController,
		OculusGoController,
	};

	[SerializeField]
	OVRTrackedRemote		leftHandController = null;
	[SerializeField]
	OVRTrackedRemote		rightHandController = null;
	[SerializeField]
	ControllerType			controllerType = ControllerType.GearVRController;

	[SerializeField]
	float					swipeSensitivity = 5.0f;


	Vector2[]				m_tatchPadAxisHistory = null;
	Vector2					m_tatchPadAxisSum;
	readonly int			TATCH_PAD_AXIS_HISTORY_NUM = 10;
	int						m_currentTatchPadAxisHistoryIndex;

	Action<Vector2> _onSwipeTatchPad = null;
	/// <summary>
	/// スワイプ時アクションのコールバックを設定する
	/// Vector2 : スワイプ方向 (左下(-1, -1) -> 右上(+1, +1))
	/// </summary>
	public Action<Vector2> OnSwipeTatchPad
	{
		set
		{
			_onSwipeTatchPad = value;
		}
	}

	// Use this for initialization
	void Start ()
	{
		m_tatchPadAxisHistory = new Vector2[TATCH_PAD_AXIS_HISTORY_NUM];
		m_currentTatchPadAxisHistoryIndex = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// コントローラーの検出
		var controller = OVRInput.GetActiveController();
		if(controller == OVRInput.Controller.LTrackedRemote)
		{
			// 左利き処理
			SetActiveLeftController(true);
			SetActiveRightController(false);
		}
		else if(controller == OVRInput.Controller.RTrackedRemote)
		{
			// 右利き処理
			SetActiveRightController(true);
			SetActiveLeftController(false);
		}
		else
		{
			return;
		}

		// スワイプ検出
		{
			m_tatchPadAxisSum -= m_tatchPadAxisHistory[m_currentTatchPadAxisHistoryIndex];									// 一番古いデータを引く
			m_tatchPadAxisHistory[m_currentTatchPadAxisHistoryIndex] = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);		// 最新データを入れる
			m_tatchPadAxisSum += m_tatchPadAxisHistory[m_currentTatchPadAxisHistoryIndex];									// その最新データを足し込む

			if( _onSwipeTatchPad != null )
			{
				float length = m_tatchPadAxisSum.sqrMagnitude;
				if( length > swipeSensitivity )
				{
					_onSwipeTatchPad( m_tatchPadAxisSum );
				}
			}

			m_currentTatchPadAxisHistoryIndex++;
			if( m_currentTatchPadAxisHistoryIndex >= TATCH_PAD_AXIS_HISTORY_NUM ) m_currentTatchPadAxisHistoryIndex = 0;
		}
	}

	/// <summary>
	/// 左手コントローラーの表示切替
	/// </summary>
	/// <param name="active">true:表示, false:非表示</param>
	void SetActiveLeftController(bool active)
	{
		SetActiveController(leftHandController, active);
	}
	/// <summary>
	/// 右手コントローラーの表示切替
	/// </summary>
	/// <param name="active">true:表示, false:非表示</param>
	void SetActiveRightController(bool active)
	{
		SetActiveController(rightHandController, active);
	}

	/// <summary>
	/// コントローラーの表示切替
	/// </summary>
	/// <param name="controller">コントローラー</param>
	/// <param name="active">true:表示, false:非表示</param>
	void SetActiveController(OVRTrackedRemote controller, bool active)
	{
		switch( controllerType )
		{
			case ControllerType.GearVRController:
				controller.m_modelGearVrController.SetActive( active );
				controller.m_modelOculusGoController.SetActive( !active );
				break;
			case ControllerType.OculusGoController:
				controller.m_modelGearVrController.SetActive( !active );
				controller.m_modelOculusGoController.SetActive( active );
				break;
		}
	}
}
