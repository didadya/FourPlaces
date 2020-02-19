using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMob.Client
{
    public interface ILiteDBClient
    {
        List<TObject> GetCollectionFromDB<TObject>(string collectionName) where TObject : class;
        void InsertObjectInDB<TObject>(TObject objectToInsert, string collectionName) where TObject : class;
        void RemoveObjectFromDB<TObject>(int objectToRemoveID, string collectionName) where TObject : class;
        void CleanCollection(string collectionName);
    }
}
