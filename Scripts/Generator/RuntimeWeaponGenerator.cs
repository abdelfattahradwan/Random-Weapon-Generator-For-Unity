using System.Linq;

using UnityEngine;

public static class RuntimeWeaponGenerator
{
	public const int BASE_PISTOL_MAGAZINE_SIZE = 8;
	public const int BASE_SMG_MAGAZINE_SIZE = 24;
	public const int BASE_RIFLE_MAGAZINE_SIZE = 10;
	public const int BASE_SHOTGUN_MAGAZINE_SIZE = 6;

	public const int BASE_PISTOL_PROJECTILE_COUNT = 1;
	public const int BASE_SMG_PROJECTILE_COUNT = 1;
	public const int BASE_RIFLE_PROJECTILE_COUNT = 1;
	public const int BASE_SHOTGUN_PROJECTILE_COUNT = 4;

	public const float BASE_PISTOL_DAMAGE = 16f;
	public const float BASE_SMG_DAMAGE = 8f;
	public const float BASE_RIFLE_DAMAGE = 24f;
	public const float BASE_SHOTGUN_DAMAGE = 8f;

	public const float BASE_PISTOL_SPREAD = 8f;
	public const float BASE_SMG_SPREAD = 12f;
	public const float BASE_RIFLE_SPREAD = 4f;
	public const float BASE_SHOTGUN_SPREAD = 16f;

	public const float BASE_PISTOL_PROJECTILE_SPEED = 1500f;
	public const float BASE_SMG_PROJECTILE_SPEED = 1750f;
	public const float BASE_RIFLE_PROJECTILE_SPEED = 2500f;
	public const float BASE_SHOTGUN_PROJECTILE_SPEED = 1250f;

	public const float BASE_PISTOL_FIRE_RATE = 1f;
	public const float BASE_SMG_FIRE_RATE = 0.25f;
	public const float BASE_RIFLE_FIRE_RATE = 2f;
	public const float BASE_SHOTGUN_FIRE_RATE = 2f;

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

	public static string GenerateRandomName()
	{
		var letters = new string[] { "A", "F", "N", "M", "AA", "MG", "MP", "FN" };

		return $"{WEAPON_MANUFACTURERS[Random.Range(0, WEAPON_MANUFACTURERS.Length)]} {letters[Random.Range(0, letters.Length)]}-{Random.Range(1, 256)}";
	}

	public static (WeaponRarity Rarity, int MagazineSize, int ProjectileCount, float Damage, float Spread, float ProjectileSpeed, float FireRate) GenerateRandomProperties(WeaponType weaponType)
	{
		(WeaponRarity Rarity, int MagazineSize, int ProjectileCount, float Damage, float Spread, float ProjectileSpeed, float FireRate) properties = (WeaponRarity.Common, 0, 0, 0f, 0f, 0f, 0f);

		const float common = 0.65f;
		const float uncommon = 0.7f;
		const float rare = 0.85f;
		const float legendary = 0.9f;
		const float mythical = 0.95f;

		var chance = Random.value;

		if (chance <= common)
		{
			properties.Rarity = WeaponRarity.Common;
		}
		if (chance >= uncommon && chance < rare)
		{
			properties.Rarity = WeaponRarity.Uncommon;
		}
		else if (chance >= rare && chance < legendary)
		{
			properties.Rarity = WeaponRarity.Rare;
		}
		else if (chance >= legendary && chance < mythical)
		{
			properties.Rarity = WeaponRarity.Legendary;
		}
		else if (chance >= mythical)
		{
			properties.Rarity = WeaponRarity.Mythical;
		}

		var multiplier = 1f;

		switch (properties.Rarity)
		{
			case WeaponRarity.Common:
				multiplier = 1f;
				break;

			case WeaponRarity.Uncommon:
				multiplier = 1.25f;
				break;

			case WeaponRarity.Rare:
				multiplier = 1.75f;
				break;

			case WeaponRarity.Legendary:
				multiplier = 2.25f;
				break;

			case WeaponRarity.Mythical:
				multiplier = 2.75f;
				break;
		}

		void SetProperties(int magazineSize, int projectileCount, float damage, float spread, float projectileSpeed, float fireRate)
		{
			properties.MagazineSize = Mathf.RoundToInt(Random.Range(magazineSize, magazineSize * multiplier));
			properties.ProjectileCount = Mathf.RoundToInt(Random.Range(projectileCount, projectileCount * multiplier));
			properties.Damage = Random.Range(damage, damage * multiplier);
			properties.Spread = Random.Range(spread, spread - (spread / (spread * multiplier)));
			properties.ProjectileSpeed = Random.Range(projectileSpeed, projectileSpeed * multiplier);
			properties.FireRate = Random.Range(fireRate, fireRate - (fireRate / (fireRate * multiplier)));
		}

		switch (weaponType)
		{
			case WeaponType.Pistol:
				SetProperties(BASE_PISTOL_MAGAZINE_SIZE, BASE_PISTOL_PROJECTILE_COUNT, BASE_PISTOL_DAMAGE, BASE_PISTOL_SPREAD, BASE_PISTOL_PROJECTILE_SPEED, BASE_PISTOL_FIRE_RATE);
				break;

			case WeaponType.SMG:
				SetProperties(BASE_SMG_MAGAZINE_SIZE, BASE_SMG_PROJECTILE_COUNT, BASE_SMG_DAMAGE, BASE_SMG_SPREAD, BASE_SMG_PROJECTILE_SPEED, BASE_SMG_FIRE_RATE);
				break;

			case WeaponType.Rifle:
				SetProperties(BASE_RIFLE_MAGAZINE_SIZE, BASE_RIFLE_PROJECTILE_COUNT, BASE_RIFLE_DAMAGE, BASE_RIFLE_SPREAD, BASE_RIFLE_PROJECTILE_SPEED, BASE_RIFLE_FIRE_RATE);
				break;

			case WeaponType.Shotgun:
				SetProperties(BASE_SHOTGUN_MAGAZINE_SIZE, BASE_SHOTGUN_PROJECTILE_COUNT, BASE_SHOTGUN_DAMAGE, BASE_SHOTGUN_SPREAD, BASE_SHOTGUN_PROJECTILE_SPEED, BASE_SHOTGUN_FIRE_RATE);
				break;
		}

		return properties;
	}

	public static Transform GenerateRandomModel(WeaponBody[] weaponBodies, GameObject[] stocks, GameObject[] handles, GameObject[] magazines, GameObject[] scopes, GameObject[] barrels)
	{
		if (weaponBodies != null && weaponBodies.Length != 0)
		{
			var weaponBody = weaponBodies.ElementAtOrDefault(Random.Range(0, weaponBodies.Length));

			if (weaponBody != null)
			{
				var selectedStock = stocks.ElementAtOrDefault(Random.Range(0, stocks.Length));
				var selectedHandle = handles.ElementAtOrDefault(Random.Range(0, handles.Length));
				var selectedMagazine = magazines.ElementAtOrDefault(Random.Range(0, magazines.Length));
				var selectedScope = scopes.ElementAtOrDefault(Random.Range(0, scopes.Length));
				var selectedBarrel = barrels.ElementAtOrDefault(Random.Range(0, barrels.Length));

				var weaponName = GenerateRandomName();

				var generatedWeaponRootTransform = new GameObject($"{weaponName} {weaponBody.Type.ToString()}").transform;

				var generatedWeaponBodyInstance = Object.Instantiate(weaponBody, generatedWeaponRootTransform);

				var weaponComponent = generatedWeaponRootTransform.gameObject.AddComponent<Weapon>();

				var properties = GenerateRandomProperties(weaponBody.Type);

				weaponComponent.WeaponName = weaponName;
				weaponComponent.Rarity = properties.Rarity;
				weaponComponent.Type = weaponBody.Type;
				weaponComponent.RemainingShots = weaponComponent.MagazineSize = properties.MagazineSize;
				weaponComponent.ProjectileCount = properties.ProjectileCount;
				weaponComponent.Damage = properties.Damage;
				weaponComponent.Spread = properties.Spread;
				weaponComponent.ProjectileSpeed = properties.ProjectileSpeed;
				weaponComponent.FireRate = properties.FireRate;

				if (selectedStock != null)
				{
					var stockTransform = generatedWeaponBodyInstance.StockTransform;

					if (stockTransform != null)
					{
						Object.Instantiate(selectedStock, stockTransform.position, stockTransform.rotation, stockTransform);
					}
				}

				if (selectedHandle != null)
				{
					var handleTransform = generatedWeaponBodyInstance.HandleTransform;

					if (handleTransform != null)
					{
						Object.Instantiate(selectedHandle, handleTransform.position, handleTransform.rotation, handleTransform);
					}
				}

				if (selectedMagazine != null)
				{
					var magazineTransform = generatedWeaponBodyInstance.MagazineTransform;

					if (magazineTransform != null)
					{
						Object.Instantiate(selectedMagazine, magazineTransform.position, magazineTransform.rotation, magazineTransform);
					}
				}

				if (selectedScope != null)
				{
					var scopeTransform = generatedWeaponBodyInstance.ScopeTransform;

					if (scopeTransform != null)
					{
						Object.Instantiate(selectedScope, scopeTransform.position, scopeTransform.rotation, scopeTransform);
					}
				}

				if (selectedBarrel != null)
				{
					var barrelTransform = generatedWeaponBodyInstance.BarrelTransform;

					if (barrelTransform != null)
					{
						Object.Instantiate(selectedBarrel, barrelTransform.position, barrelTransform.rotation, barrelTransform);
					}
				}

				return generatedWeaponRootTransform;
			}
		}

		return null;
	}
}
