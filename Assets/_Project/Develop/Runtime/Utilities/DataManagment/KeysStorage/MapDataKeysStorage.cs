using Assets._Project.Develop.Runtime.Utilities.DataManagment.Data;
using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.KeysStorage
{
    public class MapDataKeysStorage : IDataKeysStorage
    {
        private readonly Dictionary<Type, string> Keys = new()
        {
            {typeof(PlayerData), "PlayerData" }
        };

        public string GetKeyFor<TData>() where TData : ISaveData
        => Keys[typeof(TData)];
    }
}