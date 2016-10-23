using CLAS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Common;

namespace CLAS.Model.VMs
{
  
        public class AccountVM
        {
            public AccountVM()
            {

            }
            public AccountVM(string CookieValue)
            {
                if (CookieValue == "")
                    UserId = 0;
                else
                {
                    CookieValue = DESEncrypt.Decrypt(CookieValue);
                    var account = CookieValue.Split(StaticKey.SplitChar);
                    UserId = account[0].ToInt();
                    UserName = account[1]; 
                }



            }

            public string SetCookie()
            {
                string[] acconutCookie = { UserId.ToString(), UserName };
                return DESEncrypt.Encrypt(string.Join(StaticKey.Split, acconutCookie));
            }
            public int UserId { get; set; }
            public string UserName { get; set; }

            public string Message { get; set; }
        }
     
}
