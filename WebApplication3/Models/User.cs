using System;
using System.Collections.Generic;
using BTDB.ODBLayer;

namespace WebApplication3.Models
{

    public class KeyObj
    {
        public DateTime DateTime { get; set; }
        public Gender Gender { get; set; }
        public ulong Id { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public class User
    {
        //radi sa podla klucov
        [PrimaryKey(1)]
        public ulong UserId { get; set; }
        [SecondaryKey("Name")]
        public string Name { get; set; }
        // 1 ku N
        [SecondaryKey("Age")]
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public IList<string> Addresses { get; set; }
        public IDictionary<string, int> NameToAge { get; set; }
        public byte[] ByteArray { get; set; }
    }

    //public interface ICustomObjTable
    //umoznuje pouzitie foreach IReadOnlyCollection<T>
    public interface IUserTable : IReadOnlyCollection<User>
    {
        //vsetko generuje IL code az ked to bezi
        void Insert(User customObj);
        bool RemoveById(ulong userId);
        User FindById(ulong userId);
        void Update(User customObj);
        bool Upsert(User customObj);
        bool Contains(ulong userId);
        User FindByAgeOrDefault(int age);
        IEnumerator<User> FindByAge(int age);
        IEnumerator<User> ListByAge(AdvancedEnumeratorParam<int> param);

        //umoznuje iterovat interval 
        //zlozite vraj sa pouziva skor ten interface

        IOrderedDictionaryEnumerator<ulong, User> ListById(AdvancedEnumeratorParam<ulong> param);
    }

    public class Id2UserClass
    {
        public IList<User> Users { get; set; }
        public IOrderedDictionary<ulong, User> Id2User { get; set; }
    }

    public class DateTimeToUserClass
    {
        public IDictionary<DateTime, User> DateTimeToUser { get; set; }
    }

    public class KeyObjToUserClass
    {
        public IDictionary<KeyObj, User> KeyObjToUser { get; set; }
    }

    public class UlongToStringClass
    {
        public IDictionary<ulong, string> UlongToString { get; set; }
    }

    public class ListOfStringsClass
    {
        public IList<string> ListOfStrings { get; set; }
    }

    public class ListOfIntegersClass
    {
        public IList<int> ListOfIntegers { get; set; }
    }
}
