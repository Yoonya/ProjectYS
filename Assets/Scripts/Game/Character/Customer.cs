namespace YunSun.Game.Character
{
    using System.Collections.Generic;
    using System.Data.Common;
    using UnityEngine;

    public class Customer : MonoBehaviour
    {
        private int id;
        private CustomerType customerType;
        private int counterLine; 
        private int orderNum;

        public bool isValid => id >= 0;
        public bool isSpecial => customerType != CustomerType.Normal;
        public bool isOrder => orderNum == 0;
        public int CounterLine => counterLine;

        private Customer()
        {
            Clean();
        }
        public void Clean()
        {
            id = -1;
            customerType = CustomerType.Normal;
            counterLine = -1;
            orderNum = -1;
        }
        public void Apply( Customer customer )
        {
            if ( customer == null )
                return;

            this.id = customer.id;
            this.customerType = customer.customerType;
            this.counterLine = customer.counterLine;
            this.orderNum = customer.orderNum;
        }
    }         
}