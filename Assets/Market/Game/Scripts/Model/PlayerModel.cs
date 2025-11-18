using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Games.Scripts.Model
{
    public class PlayerModel : MonoSingleton<PlayerModel>
    {
        #region Currency

        private Dictionary<PlayerCurrencyType, FloatVar> _pCurrencies;
        public static Dictionary<PlayerCurrencyType, FloatVar> PlayerCurrencies
        {
            get
            {
                if (Instance._pCurrencies != null) return Instance._pCurrencies;
                Instance._pCurrencies = new Dictionary<PlayerCurrencyType, FloatVar>();
                foreach (PlayerCurrencyType type in Enum.GetValues(typeof(PlayerCurrencyType)))
                {
                    Instance._pCurrencies.Add(type, new FloatVar($"{Key.CurrencyKey}_{type}"));
                }
                return Instance._pCurrencies;
            }
        }
        public static FloatVar PlayerMoney => PlayerCurrencies[PlayerCurrencyType.Money];
        #endregion

    }
    public enum PlayerCurrencyType
    {
        Money,
    }
}