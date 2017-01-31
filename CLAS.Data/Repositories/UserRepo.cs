using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Common;
using CLAS.Data.Infrastructure;
using CLAS.Model.Entities;
using CLAS.Data.Dapper;
using CLAS.Model;
using CLAS.Model.SMs;
using CLAS.Model.VMs;
using CLAS.Utils;

namespace CLAS.Data.Repositories
{
    /// <summary>
    ///用户Repo
    /// </summary>
    public class UserRepo : RepositoryBase<CL_User>, IUserRepo
    {
        public UserRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public AccountVM Login(AccountLoginVM accountVM)
        {
            var result = new AccountVM() { Message = "" };
              //判断是否登陆成功
            accountVM.Password = DESEncrypt.Encrypt(accountVM.Password);
            var account = DapperHelper.SqlQuery<AccountVM>("select Id UserId ,UserName from CL_User where UserName=@UserName and Password=@Password", accountVM).FirstOrDefault();
            if (account == null)
            {
                   result.Message = "输入的用户不存在";
            } 
            else
            {
                //登陆成功
                result.UserId = account.UserId; 
                result.UserName = account.UserName;
                 
            }
              
            return result;

        }
        public async Task<List<CL_User>> GetUserList(SystemUserSM sm)
        {
            var sql = @"select *  from CL_User  ";
            sql += string.IsNullOrEmpty(sm.UserName) ? "" : " and  UserName like '%'+@UserName+'%' ";  
            sql += " order by  id ";
            var result = (await DapperHelper.SqlQueryAsync<CL_User>(sql, sm)).ToList();
            return result;

        }

        public bool IsUserNameHaved(string userName)
        {
          return  DapperHelper.SqlQuery<int>("select Id  from CL_User where userName=@userName", new {userName}).Any();
        }


        public string ChangePassword(int id, string OPassword, string NPassword)
        {
            var result = DapperHelper.SqlQuery<int>("select UserId from CL_User where id=@id and Password=@Password", new {  id, Password = DESEncrypt.Encrypt(OPassword) }).FirstOrDefault();
            if (result != 0)
            {
                result = DapperHelper.SqlExecute(@"update CL_User set Password=@Password where id=@id", new { id, Password = DESEncrypt.Encrypt(NPassword) });
                return result > 0 ? "" : "保存失败,请重试";
            }
            return "旧密码错误，请重新输入";
        }
    }
    public interface IUserRepo : IRepository<CL_User>
    {
        AccountVM Login(AccountLoginVM accountVM);


        Task<List<CL_User>> GetUserList(SystemUserSM sm);

        bool IsUserNameHaved(string userName);

        string ChangePassword(int UserId, string OPassword, string NPassword);
    }
}
