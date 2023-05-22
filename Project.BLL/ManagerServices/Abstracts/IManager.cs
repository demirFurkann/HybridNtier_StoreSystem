using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstracts
{
    public interface IManager<T> where T : BaseEntity
    {
        //List Commands

        List<T> GetAll();
        List<T> GetActives();
        List<T> GetPassives();
        List<T> GetModifieds();

        //Modify Commands
        string Add(T item);
        string AddRange(List<T> list);
        void Delete(T item);
        void DeleteRange(List<T> list);
        void Update(T item);
        void UpdateRange(List<T> list);
        string Destroy(T item);
        void DestroyRange(List<T> list);

        //Linq Commands

        List<T> Where(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
        T FirstOrdefault(Expression<Func<T, bool>> exp);
         object Select(Expression<Func<T, object>> exp);
        IQueryable<X> Select<X>(Expression<Func<T, X>> exp);

        //Find Commands
        T Find(int id);
        //LastDatas
        List<T> GetLastDatas(int count);
        //First Datas
        List<T> GetFirstDatas(int count);
    }
}
