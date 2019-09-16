using System.Collections.Generic;

using UnityEngine;

namespace WinterboltGames.RandomWeaponGenerator
{
	[CreateAssetMenu(menuName = "Weapons/Weapon Properties Template")]
	public class WeaponPropertiesTemplate : ScriptableObject
	{
		[SerializeField] private List<WeaponProperties> categories;

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

		public List<WeaponProperties> Categories { get => categories; set => categories = value; }

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
}