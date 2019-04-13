using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private string weaponName;

	[SerializeField] private WeaponType type;

	[SerializeField] private WeaponRank rank;

	[SerializeField] private int magazineSize;
	[SerializeField] private int remainingShots;
	[SerializeField] private int projectileCount;

	[SerializeField] private float damage;
	[SerializeField] private float spread;
	[SerializeField] private float projectileSpeed;
	[SerializeField] private float fireRate;
	[SerializeField] private float counter;

	[SerializeField] private bool isSemiAuto;
	[SerializeField] private bool repeatFireForEachFirePoint;

	[SerializeField] private Projectile projectilePrefab;

	[SerializeField] private List<Transform> firePoints;

	[Header("Sound FX")]

	[SerializeField] private AudioSource audioSource;

	[SerializeField] private AudioClip[] sounds;

	public string WeaponName { get => weaponName; set => weaponName = value; }

	public WeaponType Type { get => type; set => type = value; }
	public WeaponRank Rank { get => rank; set => rank = value; }

	public int MagazineSize { get => magazineSize; set => magazineSize = value; }
	public int RemainingShots { get => remainingShots; set => remainingShots = value; }
	public int ProjectileCount { get => projectileCount; set => projectileCount = value; }

	public float Damage { get => damage; set => damage = value; }
	public float Spread { get => spread; set => spread = value; }
	public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
	public float FireRate { get => fireRate; set => fireRate = value; }
	public float Counter { get => counter; set => counter = value; }

	public bool IsSemiAuto { get => isSemiAuto; set => isSemiAuto = value; }
	public bool RepeatFireForEachFirePoint { get => repeatFireForEachFirePoint; set => repeatFireForEachFirePoint = value; }

	public Projectile ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }

	public List<Transform> FirePoints { get => firePoints; set => firePoints = value; }

	public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

	public AudioClip[] Sounds { get => sounds; set => sounds = value; }

	private void Update()
	{
		if (remainingShots > 0)
		{
			if (isSemiAuto)
			{
				if (Input.GetMouseButtonDown(0))
				{
					Fire();
				}
			}
			else
			{
				if (Input.GetMouseButton(0) && counter >= fireRate)
				{
					Fire();

					counter = 0f;
				}

				counter += counter < fireRate ? Time.deltaTime : 0f;
			}
		}
	}

	private void Fire()
	{
		if (ProjectilePrefab != null && projectileCount != 0 && FirePoints.Count > 0)
		{
			if (repeatFireForEachFirePoint)
			{
				for (int i = 0; i < FirePoints.Count; i++)
				{
					var firePoint = FirePoints[i];

					if (firePoint == null)
					{
						continue;
					}

					for (int k = 0; k < projectileCount; k++)
					{
						CreateProjectile(firePoint);
					}
				}
			}
			else
			{
				var firePoint = FirePoints[0];

				if (firePoint != null)
				{
					for (int i = 0; i < projectileCount; i++)
					{
						CreateProjectile(firePoint);
					}
				}
			}

			void CreateProjectile(Transform firePoint)
			{
				var clone = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);

				clone.transform.eulerAngles += new Vector3(0f, Random.Range(-spread, spread), Random.Range(-spread, spread));

				clone.Damage = damage;

				clone.Rigidbody?.AddForce(clone.transform.forward * projectileSpeed);
			}
		}

		if (AudioSource != null)
		{
			if (Sounds != null && Sounds.Length != 0)
			{
				var sound = Sounds.ElementAtOrDefault(Random.Range(0, Sounds.Length));

				if (sound != null)
				{
					AudioSource.PlayOneShot(sound);
				}
			}
		}

		remainingShots--;
	}
}