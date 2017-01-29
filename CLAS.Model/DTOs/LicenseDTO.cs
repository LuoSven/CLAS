using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.DTOs
{
    public class LicenseDTO
    {
        public int Id { get; set; }

        /// <summary>
        ///验证码
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///验证码
        /// </summary>
        public string ActivationCode { get; set; }
        /// <summary>
        ///验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        ///验证码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        ///策略编号
        /// </summary>
        public int TracticsId { get; set; }
        /// <summary>
        ///策略编号
        /// </summary>
        public string TacticsName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public int BidderId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string BidderName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        ///验证码
        /// </summary>
        public DateTime? LastActiveTime { get; set; } 
        /// <summary>
        ///验证码
        /// </summary>
        public DateTime? LastSyncTime { get; set; }
        /// <summary>
        /// 是否是模拟
        /// </summary>
        public bool? IsFor51 { get; set; }
    }
}
