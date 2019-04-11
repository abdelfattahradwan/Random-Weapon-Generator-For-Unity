using System.Linq;

using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("Others")]

	[SerializeField] private string weaponName;

	[SerializeField] private WeaponType type;

	[SerializeField] private WeaponRarity rarity;

	[Header("Properties")]

	[SerializeField] private int magazineSize;
	[SerializeField] private int remainingShots;
	[SerializeField] private int projectileCount;

	[SerializeField] private float damage;
	[SerializeField] private float spread;
	[SerializeField] private float projectileSpeed;
	[SerializeField] private float fireRate;
	[SerializeField] private float counter;

	[SerializeField] private bool canFire;
	[SerializeField] private bool isSemiAuto;
	[SerializeField] private bool repeatFireForeachFirePoint;

	[SerializeField] private Projectile projectilePrefab;

	[SerializeField] private Transform[] firePoints;

	[Header("Sound FX")]

	[SerializeField] private AudioSource audioSource;

	[SerializeField] private AudioClip[] sounds;

	public string WeaponName { get => weaponName; set => weaponName = value; }

	public WeaponType Type { get => type; set => type = value; }
	public WeaponRarity Rarity { get => rarity; set => rarity = value; }

	public int MagazineSize { get => magazineSize; set => magazineSize = value; }
	public int RemainingShots { get => remainingShots; set => remainingShots = value; }
	public int ProjectileCount { get => projectileCount; set => projectileCount = value; }

	public float Damage { get => damage; set => damage = value; }
	public float Spread { get => spread; set => spread = value; }
	public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
	public float FireRate { get => fireRate; set => fireRate = value; }
	public float Counter { get => counter; set => counter = value; }

	public bool CanFire { get => canFire; set => canFire = value; }
	public bool IsSemiAuto { get => isSemiAuto; set => isSemiAuto = value; }
	public bool RepeatFireForeachFirePoint { get => repeatFireForeachFirePoint; set => repeatFireForeachFirePoint = value; }

	public Projectile ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }

	public Transform[] FirePoints { get => firePoints; set => firePoints = value; }

	public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

	public AudioClip[] Sounds { get => sounds; set => sounds = value; }

	private void Update()
	{
		if (canFire)
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

		canFire = remainingShots > 0;
	}

	private void Fire()
	{
		if (projectilePrefab != null && projectileCount != 0 && firePoints.Length > 0)
		{
			if (repeatFireForeachFirePoint)
			{
				for (int i = 0; i < firePoints.Length; i++)
				{
					var firePoint = firePoints[i];

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
				var firePoint = firePoints[0];

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
				var clone = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

				clone.transform.eulerAngles += new Vector3(0f, Random.Range(-spread, spread), Random.Range(-spread, spread));

				clone.Damage = damage;

				clone.Rigidbody?.AddForce(clone.transform.forward * projectileSpeed);
			}
		}

		if (audioSource != null)
		{
			if (sounds != null && sounds.Length != 0)
			{
				var sound = sounds.ElementAtOrDefault(Random.Range(0, sounds.Length));

				if (sound != null)
				{
					audioSource.PlayOneShot(sound);
				}
			}
		}

		remainingShots--;
	}
}