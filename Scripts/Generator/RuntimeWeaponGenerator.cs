using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace WinterboltGames.RandomWeaponGenerator
{
	public static class RuntimeWeaponGenerator
	{
		public static readonly char[] Digits = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		};

		public static readonly char[] Letters = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
		};

		public static string GenerateRandomWeaponName(string format)
		{
			if (string.IsNullOrEmpty(format) || string.IsNullOrWhiteSpace(format))
			{
				return "Unnamed";
			}
			else
			{
				StringBuilder nameStringBuilder = new StringBuilder();

				for (int i = 0; i < format.Length; i++)
				{
					char currentCharacter = format[i];

					if (currentCharacter == 'D')
					{
						nameStringBuilder.Append(Digits[Random.Range(0, Digits.Length)]);
					}
					else if (currentCharacter == 'L')
					{
						nameStringBuilder.Append(Letters[Random.Range(0, Letters.Length)]);
					}
					else
					{
						nameStringBuilder.Append(currentCharacter);
					}
				}

				return nameStringBuilder.ToString();
			}
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

		public static WeaponBase GenerateRandomWeapon(
			List<WeaponPropertiesTemplate> templates,
			List<WeaponBase> weaponBases,
			List<GameObject> stocks,
			List<GameObject> grips,
			List<GameObject> magazines,
			List<GameObject> scopes,
			List<GameObject> barrels)
		{
			if (templates == null || weaponBases == null || weaponBases.Count == 0)
			{
				return null;
			}

			WeaponBase weaponBase = weaponBases.ElementAtOrDefault(Random.Range(0, weaponBases.Count));

			if (weaponBase == null)
			{
				return null;
			}

			WeaponPropertiesTemplate selectedTemplate = templates.ElementAtOrDefault(Random.Range(0, templates.Count));

			if (selectedTemplate == null)
			{
				return null;
			}

			string selectedNameFormat = selectedTemplate.NameFormats.ElementAtOrDefault(Random.Range(0, selectedTemplate.NameFormats.Count));

			GameObject selectedStock = stocks.ElementAtOrDefault(Random.Range(0, stocks.Count));
			GameObject selectedGrip = grips.ElementAtOrDefault(Random.Range(0, grips.Count));
			GameObject selectedMagazine = magazines.ElementAtOrDefault(Random.Range(0, magazines.Count));
			GameObject selectedScope = scopes.ElementAtOrDefault(Random.Range(0, scopes.Count));
			GameObject selectedBarrel = barrels.ElementAtOrDefault(Random.Range(0, barrels.Count));

			WeaponBase generatedWeaponBase = Object.Instantiate(weaponBase);

			generatedWeaponBase.name = $"{GenerateRandomWeaponName(selectedNameFormat)} {weaponBase.Properties.CategoryName}";

			generatedWeaponBase.Properties = GenerateRandomWeaponProperties(generatedWeaponBase.Category, templates.ElementAtOrDefault(Random.Range(0, templates.Count)));

			if (selectedStock != null)
			{
				Transform stockTransform = generatedWeaponBase.StockTransform;

				if (stockTransform != null)
				{
					Object.Instantiate(selectedStock, stockTransform.position, stockTransform.rotation, stockTransform).name = "Stock";
				}
			}

			if (selectedGrip != null)
			{
				Transform gripTransform = generatedWeaponBase.GripTransform;

				if (gripTransform != null)
				{
					Object.Instantiate(selectedGrip, gripTransform.position, gripTransform.rotation, gripTransform).name = "Grip";
				}
			}

			if (selectedMagazine != null)
			{
				Transform magazineTransform = generatedWeaponBase.MagazineTransform;

				if (magazineTransform != null)
				{
					Object.Instantiate(selectedMagazine, magazineTransform.position, magazineTransform.rotation, magazineTransform).name = "Magazine";
				}
			}

			if (selectedScope != null)
			{
				Transform scopeTransform = generatedWeaponBase.ScopeTransform;

				if (scopeTransform != null)
				{
					Object.Instantiate(selectedScope, scopeTransform.position, scopeTransform.rotation, scopeTransform).name = "Scope";
				}
			}

			if (selectedBarrel != null)
			{
				Transform barrelTransform = generatedWeaponBase.BarrelTransform;

				if (barrelTransform != null)
				{
					Object.Instantiate(selectedBarrel, barrelTransform.position, barrelTransform.rotation, barrelTransform).name = "Barrel";
				}
			}

			return generatedWeaponBase;
		}
	}
}