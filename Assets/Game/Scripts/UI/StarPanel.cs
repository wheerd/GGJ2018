using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPanel : MonoBehaviour
{

	[SerializeField] private GameObject StarPrefab;

	void Start()
	{
		int numberOfStars = Random.Range(1, 8);

		for (int i = 0; i < numberOfStars; i++)
		{
			var nextStar = Instantiate(StarPrefab);
			nextStar.SetActive(true);
			nextStar.gameObject.transform.SetParent(transform);
			
			nextStar.transform.localScale = new Vector3( 1, 1, 1 );
			nextStar.transform.localPosition = Vector3.zero;
		    nextStar.transform.rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);

        }
	}
}
