using MutualFriend.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MutualFriend.Models
{
    public class FriendDB
    {
        TestEntities _DB = new TestEntities();
        public List<FriendModel> ListAll()
        {
            var modeldata = _DB.Employees.Where(x => x.IsActive == true).ToList();
            List<FriendModel> Model = new List<FriendModel>();
            if (modeldata.Count > 0)
            {
                var count = 0;
                foreach (var item in modeldata)
                {
                    FriendModel model = new FriendModel();
                    count++;
                    model.MutualCount = count;
                    model.Id = item.id;
                    model.PersonName = item.name;
                    Model.Add(model);
                }
            }
            return Model;
        }
        public int Add(FormCollection model)
        {
            //int i;
            Employee em = new Employee();
            //em.name = ;
            em.IsActive = true;
            //_DB.Employees.Add(em);
            //_DB.SaveChanges();
            return 1;
        }
        public List<FriendModel> getMutual(int Id)
        {
            List<FriendModel> Model = new List<FriendModel>();
            int[] mutualId = _DB.MutualFriends.Where(x => x.Person == Id).Select(x => x.PersonMutual).ToArray();//new int[10000];

            var modeldata = _DB.MutualFriends.Where(x => x.Person == Id).ToList();
            if (modeldata.Count > 0)
            {
                //var count1 = 0;
                //foreach (var item in modeldata)
                //{
                //    FriendModel model = new FriendModel();
                                    
                //    mutualId[count1] = item.PersonMutual;
                //    count1++;
                //}
                var count = 0;
                foreach (var item in modeldata)
                {
                    FriendModel model = new FriendModel();
                    count++;
                    model.Id = item.PersonMutual;
                    model.Count = count;
                    //mutualId[count] = item.PersonMutual;
                    var detail = mapdetail(item.PersonMutual);
                    if (detail != null)
                    {
                        model.PersonName = detail.name;
                    }
                    int[] MutualCount = GetMutualDetail(item.PersonMutual);
                    int[] Results = mutualId.Intersect(MutualCount).ToArray();
                    model.MutualCount = Results.Length;
                    Model.Add(model);
                }
            }
            return Model;
        }
        public int[] GetMutualDetail(int Id) {
          int[] array= _DB.MutualFriends.Where(x => x.Person == Id).Select(x=>x.PersonMutual).ToArray();
            return array;
        }
        public MutualFriend.Models.Entity.Employee mapdetail(int Id)
        {
            MutualFriend.Models.Entity.Employee model = new MutualFriend.Models.Entity.Employee();

            model = _DB.Employees.Where(x => x.id == Id).FirstOrDefault();
            return model;
        }
    }
}