using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace WinterboltGames.RandomWeaponGenerator
{
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

		public static WeaponProperties GenerateRandomWeaponProperties(string categoryName, WeaponPropertiesTemplate template)
		{
			float chance = Random.value;

			float multiplier = 1f;

			WeaponRank rank = WeaponRank.Common;

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

			for (int i = 0; i < template.Categories.Count; i++)
			{
				WeaponProperties category = template.Categories[i];

				if (category != null)
				{
					if (!string.IsNullOrEmpty(category.CategoryName) || !string.IsNullOrWhiteSpace(category.CategoryName))
					{
						if (string.Compare(category.CategoryName, categoryName) == 0)
						{
							return new WeaponProperties()
							{
								CategoryName = categoryName,

								Rank = rank,

								Damage = Random.Range(category.Damage, category.Damage * multiplier),
								Accuracy = Random.Range(category.Accuracy, Mathf.Max(0f, category.Accuracy - ((category.Accuracy * multiplier) - category.Accuracy))),
								Recoil = Random.Range(category.Recoil, Mathf.Max(0f, category.Recoil - ((category.Recoil * multiplier) - category.Recoil))),
								FireRate = Random.Range(category.FireRate, Mathf.Max(0f, category.FireRate - ((category.FireRate * multiplier) - category.FireRate))),

								ProjectilesPerShot = Mathf.RoundToInt(Random.Range(category.ProjectilesPerShot, category.ProjectilesPerShot * multiplier)),
								MagazineSize = Mathf.RoundToInt(Random.Range(category.MagazineSize, category.MagazineSize * multiplier)),
							};
						}
					}
				}
			}

			return new WeaponProperties();
		}

		public static WeaponBase GenerateRandomWeapon
			(List<WeaponPropertiesTemplate> templates,
			List<WeaponBase> weaponBases,
			List<GameObject> stocks,
			List<GameObject> handles,
			List<GameObject> magazines,
			List<GameObject> scopes,
			List<GameObject> barrels)
		{
			if (templates != null && weaponBases != null && weaponBases.Count != 0)
			{
				WeaponBase weaponBase = weaponBases.ElementAtOrDefault(Random.Range(0, weaponBases.Count));

				if (weaponBase != null)
				{
					GameObject selectedStock = stocks.ElementAtOrDefault(Random.Range(0, stocks.Count));
					GameObject selectedHandle = handles.ElementAtOrDefault(Random.Range(0, handles.Count));
					GameObject selectedMagazine = magazines.ElementAtOrDefault(Random.Range(0, magazines.Count));
					GameObject selectedScope = scopes.ElementAtOrDefault(Random.Range(0, scopes.Count));
					GameObject selectedBarrel = barrels.ElementAtOrDefault(Random.Range(0, barrels.Count));

					WeaponBase generatedWeaponBase = Object.Instantiate(weaponBase, new GameObject($"{GenerateRandomWeaponName()} {weaponBase.Properties.CategoryName}").transform);

					generatedWeaponBase.Properties = GenerateRandomWeaponProperties(generatedWeaponBase.Category, templates.ElementAtOrDefault(Random.Range(0, templates.Count)));

					if (selectedStock != null)
					{
						Transform stockTransform = generatedWeaponBase.StockTransform;

						if (stockTransform != null)
						{
							_ = Object.Instantiate(selectedStock, stockTransform.position, stockTransform.rotation, stockTransform);
						}
					}

					if (selectedHandle != null)
					{
						Transform handleTransform = generatedWeaponBase.HandleTransform;

						if (handleTransform != null)
						{
							_ = Object.Instantiate(selectedHandle, handleTransform.position, handleTransform.rotation, handleTransform);
						}
					}

					if (selectedMagazine != null)
					{
						Transform magazineTransform = generatedWeaponBase.MagazineTransform;

						if (magazineTransform != null)
						{
							_ = Object.Instantiate(selectedMagazine, magazineTransform.position, magazineTransform.rotation, magazineTransform);
						}
					}

					if (selectedScope != null)
					{
						Transform scopeTransform = generatedWeaponBase.ScopeTransform;

						if (scopeTransform != null)
						{
							_ = Object.Instantiate(selectedScope, scopeTransform.position, scopeTransform.rotation, scopeTransform);
						}
					}

					if (selectedBarrel != null)
					{
						Transform barrelTransform = generatedWeaponBase.BarrelTransform;

						if (barrelTransform != null)
						{
							_ = Object.Instantiate(selectedBarrel, barrelTransform.position, barrelTransform.rotation, barrelTransform);
						}
					}

					return generatedWeaponBase;
				}
			}

			return default;
		}
	}
}