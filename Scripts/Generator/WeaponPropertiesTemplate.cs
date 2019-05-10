using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Properties Template")]
public class WeaponPropertiesTemplate : ScriptableObject
{
	[Serializable]
	public class WeaponPropertiesSet
	{
		[SerializeField] private int baseMagazineSize;
		[SerializeField] private int baseProjectileCount;

		[SerializeField] private float baseDamage;
		[SerializeField] private float baseSpread;
		[SerializeField] private float baseProjectileSpeed;
		[SerializeField] private float baseFireRate;

		public int BaseMagazineSize { get => baseMagazineSize; set => baseMagazineSize = value; }
		public int BaseProjectileCount { get => baseProjectileCount; set => baseProjectileCount = value; }

		public float BaseDamage { get => baseDamage; set => baseDamage = value; }
		public float BaseSpread { get => baseSpread; set => baseSpread = value; }
		public float BaseProjectileSpeed { get => baseProjectileSpeed; set => baseProjectileSpeed = value; }
		public float BaseFireRate { get => baseFireRate; set => baseFireRate = value; }

		public WeaponPropertiesSet(int baseMagazineSize, int baseProjectileCount, float baseDamage, float baseSpread, float baseProjectileSpeed, float baseFireRate)
		{
			this.baseMagazineSize = baseMagazineSize;
			this.baseProjectileCount = baseProjectileCount;
			this.baseDamage = baseDamage;
			this.baseSpread = baseSpread;
			this.baseProjectileSpeed = baseProjectileSpeed;
			this.baseFireRate = baseFireRate;
		}

		public WeaponPropertiesSet() { }
	}

	[SerializeField] private WeaponPropertiesSet pistols;

	public WeaponPropertiesSet Pistols { get => pistols; set => pistols = value; }

	[SerializeField] private WeaponPropertiesSet smgs;

	public WeaponPropertiesSet SMGs { get => smgs; set => smgs = value; }

	[SerializeField] private WeaponPropertiesSet rifles;

	public WeaponPropertiesSet Rifles { get => rifles; set => rifles = value; }

	[SerializeField] private WeaponPropertiesSet shotguns;

	public WeaponPropertiesSet Shotguns { get => shotguns; set => shotguns = value; }

	[Header("Chances & Multipliers")]

	[SerializeField, Range(0f, 1f)] private float commonRankChance;
	[SerializeField, Range(0f, 1f)] private float uncommonRankChance;
	[SerializeField, Range(0f, 1f)] private float rareRankChance;
	[SerializeField, Range(0f, 1f)] private float epicRankChance;
	[SerializeField, Range(0f, 1f)] private float legendaryRankChance;

	[SerializeField] private float commonRankMultiplier;
	[SerializeField] private float uncommonRankMultiplier;
	[SerializeField] private float rareRankMultiplier;
	[SerializeField] private float epicRankMultiplier;
	[SerializeField] private float legendaryRankMultiplier;

	public float CommonRankChance { get => commonRankChance; set => commonRankChance = value; }
	public float UncommonRankChance { get => uncommonRankChance; set => uncommonRankChance = value; }
	public float RareRankChance { get => rareRankChance; set => rareRankChance = value; }
	public float EpicRankChance { get => epicRankChance; set => epicRankChance = value; }
	public float LegendaryRankChance { get => legendaryRankChance; set => legendaryRankChance = value; }

	public float CommonRankMultiplier { get => commonRankMultiplier; set => commonRankMultiplier = value; }
	public float UncommonRankMultiplier { get => uncommonRankMultiplier; set => uncommonRankMultiplier = value; }
	public float RareRankMultiplier { get => rareRankMultiplier; set => rareRankMultiplier = value; }
	public float EpicRankMultiplier { get => epicRankMultiplier; set => epicRankMultiplier = value; }
	public float LegendaryRankMultiplier { get => legendaryRankMultiplier; set => legendaryRankMultiplier = value; }
}