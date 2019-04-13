using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Properties Template")]
public class WeaponPropertiesTemplate : ScriptableObject
{
	[Header("Pistols")]

	[SerializeField] private int basePistolMagazineSize;
	[SerializeField] private int basePistolProjectileCount;

	[SerializeField] private float basePistolDamage;
	[SerializeField] private float basePistolSpread;
	[SerializeField] private float basePistolProjectileSpeed;
	[SerializeField] private float basePistolFireRate;

	[Header("SMGs")]

	[SerializeField] private int baseSMGMagazineSize;
	[SerializeField] private int baseSMGProjectileCount;

	[SerializeField] private float baseSMGDamage;
	[SerializeField] private float baseSMGSpread;
	[SerializeField] private float baseSMGProjectileSpeed;
	[SerializeField] private float baseSMGFireRate;

	[Header("Rifles")]

	[SerializeField] private int baseRifleMagazineSize;
	[SerializeField] private int baseRifleProjectileCount;

	[SerializeField] private float baseRifleDamage;
	[SerializeField] private float baseRifleSpread;
	[SerializeField] private float baseRifleProjectileSpeed;
	[SerializeField] private float baseRifleFireRate;

	[Header("Shotguns")]

	[SerializeField] private int baseShotgunMagazineSize;
	[SerializeField] private int baseShotgunProjectileCount;

	[SerializeField] private float baseShotgunDamage;
	[SerializeField] private float baseShotgunSpread;
	[SerializeField] private float baseShotgunProjectileSpeed;
	[SerializeField] private float baseShotgunFireRate;

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

	public int BasePistolMagazineSize { get => basePistolMagazineSize; set => basePistolMagazineSize = value; }
	public int BasePistolProjectileCount { get => basePistolProjectileCount; set => basePistolProjectileCount = value; }

	public float BasePistolDamage { get => basePistolDamage; set => basePistolDamage = value; }
	public float BasePistolSpread { get => basePistolSpread; set => basePistolSpread = value; }
	public float BasePistolProjectileSpeed { get => basePistolProjectileSpeed; set => basePistolProjectileSpeed = value; }
	public float BasePistolFireRate { get => basePistolFireRate; set => basePistolFireRate = value; }

	public int BaseSMGMagazineSize { get => baseSMGMagazineSize; set => baseSMGMagazineSize = value; }
	public int BaseSMGProjectileCount { get => baseSMGProjectileCount; set => baseSMGProjectileCount = value; }

	public float BaseSMGDamage { get => baseSMGDamage; set => baseSMGDamage = value; }
	public float BaseSMGSpread { get => baseSMGSpread; set => baseSMGSpread = value; }
	public float BaseSMGProjectileSpeed { get => baseSMGProjectileSpeed; set => baseSMGProjectileSpeed = value; }
	public float BaseSMGFireRate { get => baseSMGFireRate; set => baseSMGFireRate = value; }

	public int BaseRifleMagazineSize { get => baseRifleMagazineSize; set => baseRifleMagazineSize = value; }
	public int BaseRifleProjectileCount { get => baseRifleProjectileCount; set => baseRifleProjectileCount = value; }

	public float BaseRifleDamage { get => baseRifleDamage; set => baseRifleDamage = value; }
	public float BaseRifleSpread { get => baseRifleSpread; set => baseRifleSpread = value; }
	public float BaseRifleProjectileSpeed { get => baseRifleProjectileSpeed; set => baseRifleProjectileSpeed = value; }
	public float BaseRifleFireRate { get => baseRifleFireRate; set => baseRifleFireRate = value; }

	public int BaseShotgunMagazineSize { get => baseShotgunMagazineSize; set => baseShotgunMagazineSize = value; }
	public int BaseShotgunProjectileCount { get => baseShotgunProjectileCount; set => baseShotgunProjectileCount = value; }

	public float BaseShotgunDamage { get => baseShotgunDamage; set => baseShotgunDamage = value; }
	public float BaseShotgunSpread { get => baseShotgunSpread; set => baseShotgunSpread = value; }
	public float BaseShotgunProjectileSpeed { get => baseShotgunProjectileSpeed; set => baseShotgunProjectileSpeed = value; }
	public float BaseShotgunFireRate { get => baseShotgunFireRate; set => baseShotgunFireRate = value; }

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