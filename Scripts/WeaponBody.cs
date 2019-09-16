using System.Collections.Generic;

using UnityEngine;

namespace WinterboltGames.RandomWeaponGenerator
{
	public class WeaponBody : MonoBehaviour
	{		
		[SerializeField] private Transform stockTransform;
		[SerializeField] private Transform handleTransform;
		[SerializeField] private Transform magazineTransform;
		[SerializeField] private Transform scopeTransform;
		[SerializeField] private Transform barrelTransform;

		[SerializeField] private List<Transform> firePoints;

		public Transform StockTransform { get => stockTransform; set => stockTransform = value; }
		public Transform HandleTransform { get => handleTransform; set => handleTransform = value; }
		public Transform MagazineTransform { get => magazineTransform; set => magazineTransform = value; }
		public Transform ScopeTransform { get => scopeTransform; set => scopeTransform = value; }
		public Transform BarrelTransform { get => barrelTransform; set => barrelTransform = value; }

		public List<Transform> FirePoints { get => firePoints; set => firePoints = value; }
	}
}