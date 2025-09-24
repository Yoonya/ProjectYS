namespace YunSun.Game.Character
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Customer : MonoBehaviour
    {
        private int id;
        private List<int> i_preference;
        private List<int> f_preference;

        private Customer()
        {
            id = -1;
            i_preference = new List<int>();
            f_preference = new List<int>();
        }
        public void Apply( Customer customer )
        {

        }
        public void Reset()
        {
            id = -1;
            i_preference.Clear();
            f_preference.Clear();
        }
    }         
}