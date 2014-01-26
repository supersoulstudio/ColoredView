using UnityEngine;
using System.Collections;

public class MovingComponent : MonoBehaviour
{
	public Transform StartPos;
	public Transform EndPos;
	public float MoveTime;
	public float WaitTime;

	private float ElapsedTime;

	private bool Waiting = false;
	private Transform Target;
	private Transform BeginTarget;
	private float Distance = 0.01f;

	private void Awake()
	{
		Transform obj;

		if (StartPos == null)
		{
			obj = this.transform.Find("Start");
			obj.parent = this.transform.parent;
			obj.renderer.enabled = false;
			StartPos = obj;
		}
		if (EndPos == null)
		{
			obj = this.transform.Find("End");
			obj.parent = this.transform.parent;
			obj.renderer.enabled = false;
			EndPos = obj;
		}

		Target = StartPos;
		BeginTarget = EndPos;
		ElapsedTime = 0f;
		//Debug.Log("Start");
	}

	private void OnEnable()
	{
		Waiting = false;
	}

	private void FixedUpdate()
	{
		ElapsedTime += Time.deltaTime;
		//Debug.Log(ElapsedTime);

		if (!Waiting)
		{
			//Debug.Log(Target.position);

			this.transform.position = Vector3.Lerp(BeginTarget.position, Target.position, ElapsedTime / MoveTime);
			this.transform.rotation = Quaternion.Lerp(BeginTarget.rotation, Target.rotation, ElapsedTime / MoveTime);

			float mag = (this.transform.position - Target.position).sqrMagnitude;
			//Debug.Log(mag);
			if (mag <= Distance)
			{
				if (Target == EndPos)
				{
					Target = StartPos;
					BeginTarget = EndPos;
				}
				else
				{
					Target = EndPos;
					BeginTarget = StartPos;
				}
				Waiting = true;
				ElapsedTime = 0f;
			}
		}
		else
		{
			if (ElapsedTime >= WaitTime)
			{
				Waiting = false;
				ElapsedTime = 0f;
			}
		}
	}

	private void OnCollisionEnter(Collision info)
	{
		if (info.gameObject.CompareTag("Player"))
		{
			info.gameObject.transform.parent = this.transform;
		}
	}
}
