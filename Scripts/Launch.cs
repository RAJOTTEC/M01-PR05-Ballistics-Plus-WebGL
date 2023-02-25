using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch
{
	public Nullable<Vector3> Calculate(Vector3 start, Vector3 end, float velocity, Vector3 gravity)
	{
		Nullable<float> airTime = GetTimeToTarget(start, end, velocity, gravity);
		if (!airTime.HasValue)
		{
			return null;
		}

		Vector3 delta = end - start;

		Vector3 n1 = delta * 2;
		Vector3 n2 = gravity * (airTime.Value * airTime.Value);
		float d = 2 * velocity * airTime.Value;
		Vector3 solution = (n1 - n2) / d;

		return solution;
	}

	public Nullable<float> GetTimeToTarget(Vector3 start, Vector3 end, float velocity, Vector3 gravity)
	{
		Vector3 delta = start - end;

		float a = gravity.magnitude * gravity.magnitude;
		float b = -4 * (Vector3.Dot(gravity, delta) + velocity * velocity);
		float c = 4 * delta.magnitude * delta.magnitude;

		float quadratic = (b * b) - (4 * a * c);
		if (quadratic < 0)
		{
			return null;
		}

		float t0 = Mathf.Sqrt((-b + Mathf.Sqrt(quadratic)) / (2 * a));
		float t1 = Mathf.Sqrt((-b - Mathf.Sqrt(quadratic)) / (2 * a));

		Nullable<float> airTime;
		if (t0 < 0)
		{
			if (t1 < 0)
			{
				return null;
			}
			else
			{
				airTime = t1;
			}
		}
		else if (t1 < 0)
		{
			airTime = t0;
		}
		else
		{
			airTime = Mathf.Max(t0, t1);
		}

		return airTime;
	}
}