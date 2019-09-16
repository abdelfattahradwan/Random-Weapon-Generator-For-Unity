using System;

using UnityEngine;

namespace WinterboltGames.RandomWeaponGenerator
{
	[Serializable]
	public class WeaponProperties
	{
		[SerializeField] private string categoryName;

		[SerializeField] private WeaponRank rank;

		[SerializeField] private float damage;
		[SerializeField] private float accuracy;
		[SerializeField] private float recoil;
		[SerializeField] private float fireRate;

		[SerializeField] private int projectilesPerShot;
		[SerializeField] private int magazineSize;

		public string CategoryName { get => categoryName; set => categoryName = value; }

		public WeaponRank Rank { get => rank; set => rank = value; }

		public float Damage { get => damage; set => damage = value; }
		public float Accuracy { get => accuracy; set => accuracy = value; }
		public float Recoil { get => recoil; set => recoil = value; }
		public float FireRate { get => fireRate; set => fireRate = value; }

		public int ProjectilesPerShot { get => projectilesPerShot; set => projectilesPerShot = value; }
		public int MagazineSize { get => magazineSize; set => magazineSize = value; }

		public WeaponProperties(string categoryName, WeaponRank rank, float damage, float accuracy, float recoil, float fireRate, int projectilesPerShot, int magazineSize)
		{
			this.categoryName = categoryName;

			this.rank = rank;

			this.damage = damage;
			this.accuracy = accuracy;
			this.recoil = recoil;
			this.fireRate = fireRate;

			this.projectilesPerShot = projectilesPerShot;
			this.magazineSize = magazineSize;
		}

		public WeaponProperties() { }
	}
}