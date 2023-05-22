using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class BaseManager<T> : IManager<T> where T : BaseEntity
    {
        protected IRepository<T> _iRep;
        public BaseManager(IRepository<T> iRep)
        {
            _iRep = iRep;
        }

        public string Add(T item)
        {
            if (item.CreatedDate != null)
            {
                _iRep.Add(item);
                return "Ekleme Başarılıdır";
            }
            return "Ekleme tarih kısmında bir sorunla karşılaşıldı";
        }

        public string AddRange(List<T> list)
        {
            if (list.Count > 5)
            {
                return "maksimum 5 veri ekleyebilirsiniz";
            }
            _iRep.AddRange(list);
            return "İşlem Başarılı";
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return _iRep.Any(exp);
        }

        public void Delete(T item)
        {
            _iRep.Delete(item);
        }

        public void DeleteRange(List<T> list)
        {
            _iRep.DeleteRange(list);
        }

        public string Destroy(T item)
        {
            if (item.Status != ENTITIES.Enums.DataStatus.Deleted)
            {
                return "Veriyi silmek için ilk önce pasif hale getiriniz";
            }
            _iRep.Destroy(item);
            return "Veri silindi";
        }

        public void DestroyRange(List<T> list)
        {
            _iRep.DeleteRange(list);
        }

        public T Find(int id)
        {
            return _iRep.Find(id);
        }

        public T FirstOrdefault(Expression<Func<T, bool>> exp)
        {
            return _iRep.FirstOrDefault(exp);   
        }

        public List<T> GetActives()
        {
            return _iRep.GetActives();
        }

        public List<T> GetAll()
        {
            return _iRep.GetAll();
        }

        public List<T> GetFirstDatas(int count)
        {
           return _iRep.GetFirstDatas(count);
        }

        public List<T> GetLastDatas(int count)
        {
           return _iRep.GetLastDatas(count);
        }

        public List<T> GetModifieds()
        {
           return _iRep.GetModifieds();
        }

        public List<T> GetPassives()
        {
            return _iRep.GetPassives();
        }

        public object Select(Expression<Func<T, object>> exp)
        {
           return _iRep.Select(exp);
        }

        public IQueryable<X> Select<X>(Expression<Func<T, X>> exp)
        {
            return _iRep.Select(exp);
        }

        public void Update(T item)
        {
            _iRep.Update(item);
        }

        public void UpdateRange(List<T> list)
        {
            _iRep.UpdateRange(list);
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _iRep.Where(exp);
        }
    }
}
