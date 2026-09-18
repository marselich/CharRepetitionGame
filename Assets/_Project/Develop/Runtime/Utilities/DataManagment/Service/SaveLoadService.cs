using Assets._Project.Develop.Runtime.Utilities.DataManagment.Data;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.KeysStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializers;
using System;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.Service
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataKeysStorage _keysStorage;
        private readonly IDataRepository _dataRepository;

        public SaveLoadService(IDataSerializer serializer, IDataKeysStorage keysStorage, IDataRepository dataRepository)
        {
            _serializer = serializer;
            _keysStorage = keysStorage;
            _dataRepository = dataRepository;
        }

        public IEnumerator Exists<TData>(Action<bool> onExistsResult) where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();

            yield return _dataRepository.Exists(key, result => onExistsResult?.Invoke(result));
        }

        public IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();

            string serializedData = "";

            yield return _dataRepository.Read(key, result => serializedData = result);

            TData data = _serializer.Deserialize<TData>(serializedData);

            onLoad?.Invoke(data);

            yield break;
        }

        public IEnumerator Remove<TData>() where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();

            yield return _dataRepository.Remove(key);
        }

        public IEnumerator Save<TData>(TData data) where TData : ISaveData
        {
            string serializedData = _serializer.Serialize(data);
            string key = _keysStorage.GetKeyFor<TData>();
            yield return _dataRepository.Write(key, serializedData);
        }
    }
}