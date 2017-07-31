using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList.Mvc;
using PagedList;

namespace Model.DAO
{   
     
    public class UserDao
    {
        OnlineShop db = null;
        public UserDao()
        {
            db = new OnlineShop();
        }
        //Insert user vào db
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        //Lay du lieu du db
        public IEnumerable<User> ListAll(int pageNumber, int pageSize) // Get all list User
        {
            return db.Users.OrderByDescending(x => x.CreateDate).ToPagedList(pageNumber, pageSize);
        }
        //Update du lieu
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public bool Update(User enity)
        {
            try
            {
                var user = db.Users.Find(enity.ID);
                user.Name = enity.Name;
                user.Email = enity.Email;
                user.ModifieldBy = enity.ModifieldBy;
                user.ModifieldDate = enity.ModifieldDate;
                user.CreateDate = DateTime.Now;
                db.SaveChanges();
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }
       
        public User GetByID(string userName)
        {
            return db.Users.SingleOrDefault(x=>x.UserName == userName);
        }

        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else return -2;
                }
            }
        }
        //Xoa du lieu tren db
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
           

        }

        
    }
}
