using ACommon.Objects;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Objects.Account
{

    internal class UserDao
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
        public byte[] UserHash { get; set; } = [];
        public byte[] UserSalt { get; set; } = [];
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }

        public async Task<ApiResult<UserDao?>> GetAsync(DataAccessService dal, string storeProcedure, List<SqlParameter> parameters, string[] expectedColumns)
        {
            return await dal.ExecuteReaderSingleAsync(storeProcedure, parameters, reader => new UserDao
            {
                UserId = DataReaderHelper.Get<int>(reader, expectedColumns[0]),
                UserName = DataReaderHelper.Get<string>(reader, expectedColumns[1]),
                UserRoles = DataReaderHelper.Get<string>(reader, expectedColumns[2]),
                UserEmail = DataReaderHelper.Get<string>(reader, expectedColumns[3]),
                IsEmailVerified = DataReaderHelper.Get<bool>(reader, expectedColumns[4]),
                IsActive = DataReaderHelper.Get<bool>(reader, expectedColumns[5]),
            });
        }
    }
}
