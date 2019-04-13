using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public static class RuntimeWeaponGenerator
{
	public static readonly string[] WEAPON_MANUFACTURERS = new string[9]
	{
		"Lockheed Martin",
		"Boeing",
		"BAE Systems",
		"Raytheon",
		"Northrop Grumman",
		"General Dynamics",
		"Airbus Group",
		"United Technologies",
		"L-3 Technologies",
	};

	public static readonly string[] LETTERS = new string[]
	{
		"A",
		"AA",
		"F",
		"FN",
		"M",
		"MG",
		"MP",
	};

	public static string GenerateRandomWeaponName()
	{
		return $"{WEAPON_MANUFACTURERS[Random.Range(0, WEAPON_MANUFACTURERS.Length)]} {LETTERS[Random.Range(0, LETTERS.Length)]}-{Random.Range(1, 1024)}";
	}

	public static WeaponProperties GenerateRandomWeaponProperties(WeaponPropertiesTemplate template, WeaponType type)
	{
		var chance = Random.value;

		var multiplier = 1f;

		var rank = WeaponRank.Common;

		if (chance <= template.CommonRankChance)
		{
			multiplier = template.CommonRankMultiplier;
			rank = WeaponRank.Common;
		}
		else if (chance > template.CommonRankChance && chance <= template.UncommonRankChance)
		{
			multiplier = template.UncommonRankChance;
			rank = WeaponRank.Uncommon;
		}
		else if (chance > template.UncommonRankChance && chance <= template.RareRankChance)
		{
			multiplier = template.RareRankMultiplier;
			rank = WeaponRank.Rare;
		}
		else if (chance > template.RareRankChance && chance <= template.EpicRankChance)
		{
			multiplier = template.EpicRankMultiplier;
			rank = WeaponRank.Epic;
		}
		else if (chance > template.EpicRankChance && chance <= template.LegendaryRankChance)
		{
			multiplier = template.LegendaryRankMultiplier;
			rank = WeaponRank.Legendary;
		}

		WeaponProperties GeneratePropertiesFromValues(int magazineSize, int projectileCount, float damage, float spread, float projectileSpeed, float fireRate) => new WeaponProperties()
		{
			Rank = rank,

			MagazineSize = Mathf.RoundToInt(Random.Range(magazineSize, magazineSize * multiplier)),
			ProjectileCount = Mathf.RoundToInt(Random.Range(projectileCount, projectileCount * multiplier)),

			Damage = Random.Range(damage, damage * multiplier),
			Spread = Random.Range(spread, Mathf.Max(0f, spread - ((spread * multiplier) - spread))),
			ProjectileSpeed = Random.Range(projectileSpeed, projectileSpeed * multiplier),
			FireRate = Random.Range(fireRate, Mathf.Max(0f, fireRate - ((fireRate * multiplier) - fireRate))),
		};

		var properties = new WeaponProperties();

		switch (type)
		{
			case WeaponType.Pistol:
				properties = GeneratePropertiesFromValues(template.BasePistolMagazineSize, template.BasePistolProjectileCount, template.BasePistolDamage, template.BasePistolSpread, template.BasePistolProjectileSpeed, template.BasePistolFireRate);
				break;

			case WeaponType.SMG:
				properties = GeneratePropertiesFromValues(template.BaseSMGMagazineSize, template.BaseSMGProjectileCount, template.BaseSMGDamage, template.BaseSMGSpread, template.BaseSMGProjectileSpeed, template.BaseSMGFireRate);
				break;

			case WeaponType.Rifle:
				properties = GeneratePropertiesFromValues(template.BaseRifleMagazineSize, template.BaseRifleProjectileCount, template.BaseRifleDamage, template.BaseRifleSpread, template.BaseRifleProjectileSpeed, template.BaseRifleFireRate);
				break;

			case WeaponType.Shotgun:
				properties = GeneratePropertiesFromValues(template.BaseShotgunMagazineSize, template.BaseShotgunProjectileCount, template.BaseShotgunDamage, template.BaseShotgunSpread, template.BaseShotgunProjectileSpeed, template.BaseShotgunFireRate);
				break;

			default:
				properties = GeneratePropertiesFromValues(0, 0, 0f, 0f, 0f, 0f);
				break;
		}

		return properties;
	}

	public static Transform GenerateRandomWeapon(
		WeaponPropertiesTemplate weaponPropertiesTemplate,
		List<WeaponBody> weaponBodies,
		List<GameObject> stocks,
		List<GameObject> handles,
		List<GameObject> magazines,
		List<GameObject> scopes,
		List<GameObject> barrels)
	{
		if (weaponPropertiesTemplate != null && weaponBodies != null && weaponBodies.Count != 0)
		{
			var weaponBody = weaponBodies.ElementAtOrDefault(Random.Range(0, weaponBodies.Count));

			if (weaponBody != null)
			{
				var selectedStock = stocks.ElementAtOrDefault(Random.Range(0, stocks.Count));
				var selectedHandle = handles.ElementAtOrDefault(Random.Range(0, handles.Count));
				var selectedMagazine = magazines.ElementAtOrDefault(Random.Range(0, magazines.Count));
				var selectedScope = scopes.ElementAtOrDefault(Random.Range(0, scopes.Count));
				var selectedBarrel = barrels.ElementAtOrDefault(Random.Range(0, barrels.Count));

				var weaponName = GenerateRandomWeaponName();

				var generatedWeaponRootTransform = new GameObject($"{weaponName} {weaponBody.Type.ToString()}").transform;

				var generatedWeaponBodyInstance = Object.Instantiate(weaponBody, generatedWeaponRootTransform);

				var weaponComponent = generatedWeaponRootTransform.gameObject.AddComponent<Weapon>();

				var properties = GenerateRandomWeaponProperties(weaponPropertiesTemplate, weaponBody.Type);

				weaponComponent.WeaponName = weaponName;

				weaponComponent.Type = weaponBody.Type;

				weaponComponent.Rank = properties.Rank;

				weaponComponent.RemainingShots = weaponComponent.MagazineSize = properties.MagazineSize;
				weaponComponent.ProjectileCount = properties.ProjectileCount;
				weaponComponent.Damage = properties.Damage;
				weaponComponent.Spread = properties.Spread;
				weaponComponent.ProjectileSpeed = properties.ProjectileSpeed;
				weaponComponent.FireRate = properties.FireRate;

				weaponComponent.FirePoints = new List<Transform>();

				if (selectedStock != null)
				{
					var stockTransform = generatedWeaponBodyInstance.StockTransform;

					if (stockTransform != null)
					{
						_ = Object.Instantiate(selectedStock, stockTransform.position, stockTransform.rotation, stockTransform);
					}
				}

				if (selectedHandle != null)
				{
					var handleTransform = generatedWeaponBodyInstance.HandleTransform;

					if (handleTransform != null)
					{
						_ = Object.Instantiate(selectedHandle, handleTransform.position, handleTransform.rotation, handleTransform);
					}
				}

				if (selectedMagazine != null)
				{
					var magazineTransform = generatedWeaponBodyInstance.MagazineTransform;

					if (magazineTransform != null)
					{
						_ = Object.Instantiate(selectedMagazine, magazineTransform.position, magazineTransform.rotation, magazineTransform);
					}
				}

				if (selectedScope != null)
				{
					var scopeTransform = generatedWeaponBodyInstance.ScopeTransform;

					if (scopeTransform != null)
					{
						_ = Object.Instantiate(selectedScope, scopeTransform.position, scopeTransform.rotation, scopeTransform);
					}
				}

				if (selectedBarrel != null)
				{
					var barrelTransform = generatedWeaponBodyInstance.BarrelTransform;

					if (barrelTransform != null)
					{
						var barrelInstanceTransform = Object.Instantiate(selectedBarrel, barrelTransform.position, barrelTransform.rotation, barrelTransform).transform;

						for (int i = 0; i < barrelInstanceTransform.childCount; i++)
						{
							var firePoint = barrelInstanceTransform.GetChild(i);

							if (firePoint != null)
							{
								weaponComponent.FirePoints.Add(firePoint);
							}
						}
					}
				}

				return generatedWeaponRootTransform;
			}
		}

		return null;
	}
}