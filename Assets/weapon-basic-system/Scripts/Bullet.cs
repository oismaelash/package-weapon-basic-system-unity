using UnityEngine;

namespace IsmaelNascimento
{
    public class Bullet : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private float timeForAutoDestroy = 3f;
        [SerializeField] private ParticleSystem particleSystemDeath;

        private int damage;
        private float speed;
        private Vector3 position;

        #endregion

        #region MONOBEHAVIOUR_METHODS

        private void Start()
        {
            Destroy(gameObject, timeForAutoDestroy);
        }

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime * position);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(particleSystemDeath, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        #endregion

        #region PUBLIC_METHODS

        public void Setup(int damage, float speed, Vector3 position)
        {
            this.damage = damage;
            this.speed = speed;
            this.position = position;
        }

        #endregion
    }
}