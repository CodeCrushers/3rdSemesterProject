using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml.Serialization;

namespace RESTServices.Models {
    public interface ICRUD<T> {
        object Create(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(int id);
        IEnumerable<T> CreateList(SqlDataReader reader);
        T CreateObject(SqlDataReader reader, bool singleRead);
    }
}