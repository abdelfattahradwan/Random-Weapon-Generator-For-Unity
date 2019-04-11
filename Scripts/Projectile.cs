using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float lifespan;
	[SerializeField] private float damage;

	[SerializeField] private Rigidbody _rigidbody;

	public float Lifespan { get => lifespan; set => lifespan = value; }
	public float Damage { get => damage; set => damage = value; }

	public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }

	private void Start()
	{
		Destroy(gameObject, Lifespan);
	}

	private void OnCollisionEnter(Collision collision)
	{
		collision.gameObject.GetComponent<IDamageReceiver>()?.ReceiveDamage(Damage);

		Destroy(gameObject);
	}
}