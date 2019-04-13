using System;

using UnityEngine;

[Serializable]
public class WeaponProperties
{
	[SerializeField] private WeaponRank rank;

	[SerializeField] private int magazineSize;
	[SerializeField] private int projectileCount;

	[SerializeField] private float damage;
	[SerializeField] private float spread;
	[SerializeField] private float projectileSpeed;
	[SerializeField] private float fireRate;

	public WeaponRank Rank { get => rank; set => rank = value; }

	public int MagazineSize { get => magazineSize; set => magazineSize = value; }
	public int ProjectileCount { get => projectileCount; set => projectileCount = value; }

	public float Damage { get => damage; set => damage = value; }
	public float Spread { get => spread; set => spread = value; }
	public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
	public float FireRate { get => fireRate; set => fireRate = value; }

	public WeaponProperties(WeaponRank rank, int magazineSize, int projectileCount, float damage, float spread, float projectileSpeed, float fireRate)
	{
		this.rank = rank;
		this.magazineSize = magazineSize;
		this.projectileCount = projectileCount;
		this.damage = damage;
		this.spread = spread;
		this.projectileSpeed = projectileSpeed;
		this.fireRate = fireRate;
	}

	public WeaponProperties() { }
}