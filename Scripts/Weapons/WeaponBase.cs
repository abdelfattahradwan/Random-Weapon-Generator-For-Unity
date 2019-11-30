using UnityEngine;

namespace WinterboltGames.RandomWeaponGenerator
{
	public class WeaponBase : MonoBehaviour
	{
		[SerializeField] protected string category;

		[SerializeField] protected WeaponProperties properties;

		[SerializeField] protected Transform stockTransform;
		[SerializeField] protected Transform gripTransform;
		[SerializeField] protected Transform magazineTransform;
		[SerializeField] protected Transform scopeTransform;
		[SerializeField] protected Transform barrelTransform;

		public string Category { get => category; set => category = value; }

		public WeaponProperties Properties { get => properties; set => properties = value; }

		public Transform StockTransform { get => stockTransform; set => stockTransform = value; }
		public Transform GripTransform { get => gripTransform; set => gripTransform = value; }
		public Transform MagazineTransform { get => magazineTransform; set => magazineTransform = value; }
		public Transform ScopeTransform { get => scopeTransform; set => scopeTransform = value; }
		public Transform BarrelTransform { get => barrelTransform; set => barrelTransform = value; }
	}
}