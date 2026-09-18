using Assets._Project.Develop.Runtime.Utilities.DataManagment.Data;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}