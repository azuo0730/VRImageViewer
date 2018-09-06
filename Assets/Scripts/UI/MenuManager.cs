using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	GameObject					FileContentPrefab = null;
	[SerializeField]
	GameObject					DirectoryContentPrefab = null;

	[SerializeField]
	Transform					FileContentTargetTransform = null;

	// Use this for initialization
	void Start () {
		
		var obj = Instantiate(FileContentPrefab, FileContentTargetTransform);
		var obj2 = Instantiate(DirectoryContentPrefab, FileContentTargetTransform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
