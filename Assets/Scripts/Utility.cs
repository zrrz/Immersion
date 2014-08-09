using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static float SetPrecision(ref float f, int precision)
    {
        float exp = Mathf.Pow(10.0f, precision);
        f = Mathf.Round(f * exp) / exp;
        return f;
    }

    public static  float SetPrecision(float f, int precision)
    {
        float exp = Mathf.Pow(10.0f, precision);
        f = Mathf.Round(f * exp) / exp;
        return f;
    }

}
