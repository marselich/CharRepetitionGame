using Assets._Project.Develop.Runtime.Utilities.DataManagment.Data;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.KeysStorage
{
    public interface IDataKeysStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}