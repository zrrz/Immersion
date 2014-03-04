using UnityEngine;
using System.Collections;

public class SnowAllObjects : MonoBehaviour {

	public Material snowMat;

	void Start () {
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Scenery")) {
			Material t_mat = new Material(snowMat);
			t_mat.mainTexture = obj.renderer.material.mainTexture;
			obj.renderer.material = t_mat;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
