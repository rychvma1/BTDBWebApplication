using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BTDB.ODBLayer;

namespace WebApplication3.Models
{
    public enum Gender
    {
        male,
        female
    }

    public class User
    {
        //radi sa podla klucov
        [PrimaryKey(1)]
        public ulong Id { get; set; }
        [SecondaryKey("Name")]
        public string Name { get; set; }
        // 1 ku N
        [SecondaryKey("Age")]
        public int Age { get; set; }
        public Gender Gender;
        public IList<string> Addresses { get; set; }
    }

    //public interface ICustomObjTable
    //umoznuje pouzitie foreach IReadOnlyCollection<T>
    public interface IUserTable : IReadOnlyCollection<User>
    {
        //vsetko generuje IL code az ked to bezi
        void Insert(User customObj);
        bool RemoveById(ulong id);
        User FindById(ulong id);
        void Update(User customObj);
        bool Upsert(User customObj);
        bool Contains(ulong id);
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
}
