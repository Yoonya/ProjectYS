namespace YunSun.Game
{
    using Character;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.Pool;

    public class Pool : MonoBehaviour
    {
        private IObjectPool<Customer> CustomerPool = null;

        public Pool()
        {
            CustomerPool = new ObjectPool<Customer>(
                createFunc: () =>
                {
                    var customer = Instantiate( Resources.Load<GameObject>("Temp/Customer1") );
                    var result = customer.GetComponent<Customer>();
                    return result;
                },
                actionOnGet: ( it ) => ActionOnGet( it ),
                actionOnRelease: ( it ) => ActionOnRelease( it ),
                actionOnDestroy: ( it ) => Destroy( it.gameObject ),
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 100
            );
        }
        
        public Customer GetCustomerPool()
            => CustomerPool.Get();
        public void ReleaseCustomerPool( Customer customer )
            => CustomerPool.Release( customer );

        private void ActionOnGet( Customer customer )
        {
            customer.Apply( customer );
            customer.SetActiveEx( true );
        }
        private void ActionOnRelease( Customer customer )
        {
            customer.Clean();
            customer.SetActiveEx( false );
        }
    }
}