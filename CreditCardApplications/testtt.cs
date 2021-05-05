using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardApplications
{
     struct AnimationStates
    {
        public static Dictionary<string, string[]> items = new Dictionary<string, string[]>
        {
            {"Idle_unarmed",new string[]{ "Knife_equip", "Pistol_equip", "Rifle_equip", "Idle_unarmed" }  }
        };



        public static string[] Idle_unarmed = { "Knife_equip", "Pistol_equip", "Rifle_equip", "Idle_unarmed" };


        public static string[] Pistol_equip = { "Pistol_idle" };

        public static string[] Pistol_fire = { "Pistol_idle" };
        public static string[] Pistol_idle = { "Pistol_fire", "Pistol_reload", "Pistol_unequip", "Pistol_idle" };
        public static string[] Pistol_reload = { "Pistol_idle" };
        public static string[] Pistol_unequip = { "Idle_unarmed" };

        public static string[] Rifle_equip = { "Rifle_idle" };
        public static string[] Rifle_fire = { "Rifle_idle" };
        public static string[] Rifle_idle = new string[4] { "Rifle_fire", "Rifle_reload", "Rifle_unequip", "Rifle_idle" };
        public static string[] Rifle_reload = { "Rifle_idle" };
        public static string[] Rifle_unequip = { "Idle_unarmed" };

        public static string[] Knife_equip = { "Knife_idle" };
        public static string[] Knife_fire = { "Knife_idle" };
        public static string[] Knife_idle = { "Knife_fire", "Knife_unequip", "Knife_idle" };
        public static string[] Knife_unequip = { "Knife_unarmed" };
        public static bool operator ==(AnimationStates anim1, AnimationStates anim2)
        {
            return anim1.Equals(anim2);
        }

        public static bool operator !=(AnimationStates anim1, AnimationStates anim2)
        {
            return !anim1.Equals(anim2);
        }

        public string[] this[string state] => FindStateIndex(state);

        private string[] FindStateIndex(string state)
        {

            foreach (var item in items)
            {
                if (item.Key == state)
                {
                    return item.Value;
                }

            }
            throw new ArgumentOutOfRangeException(
             nameof(state),
             $"State {state} is not supported.");

        }



        public string animationStates { get; set; }
    };
}
