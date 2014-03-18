using UnityEngine;
using System.Collections;

public class DummyManager : MonoBehaviour {

	public Transform prefab;

	void Start()
	{
		Transform o = (Transform)Instantiate(prefab);
	}
}
