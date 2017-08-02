using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BallMover : MonoBehaviour
{
	Vector3 speed;
	Vector3[] path_phys;
	float speedScalar;
	int targetIdx;
	private bool IsOnPath;
	string pathToJson;
	string jsonString;

	// Use this for initialization
	void Start ()
	{
		IsOnPath = false;
		targetIdx = 0;
		LoadPath ();
		transform.position = path_phys [0];
		speedScalar = 1;
	}

	void LoadPath ()
	{
		pathToJson = Application.streamingAssetsPath + "/ball_path.json";
		jsonString = File.ReadAllText (pathToJson);
		Path path = JsonUtility.FromJson<Path> (jsonString);
		path_phys = new Vector3[path.x.Length];
		for (int i = 0; i < path.x.Length; i++) 
		{
			path_phys[i] = new Vector3 (path.x [i], path.y [i], path.z [i]);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (IsOnPath) 
		{
			if ((path_phys[targetIdx] - transform.position).magnitude < 0.01) 
			{
				

				if(targetIdx == path_phys.Length-1)
				{
					transform.position = path_phys[targetIdx];
					IsOnPath = false;
					return;
				}
				speed = (path_phys [targetIdx+1] - path_phys [targetIdx]).normalized;
				targetIdx++;
				transform.position = path_phys[targetIdx-1];
				return;
			}
			transform.position += speed * speedScalar * Time.deltaTime;
		}
	}

	void OnMouseDown()
	{
		if (!IsOnPath) 
		{
			IsOnPath = true;
			transform.position = path_phys [0];
			targetIdx = 0;
		}
	}


}

     

[System.Serializable]
public class Path
{
	public float[] x;
	public float[] y;
	public float[] z;
}