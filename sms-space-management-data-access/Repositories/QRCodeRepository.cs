using Dapper;
using Newtonsoft.Json.Linq;
using Npgsql;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.ContentManagement;
using sms.space.management.domain.Entities.PlayerManagement;
using sms.space.management.domain.Entities.QRCode;
using sms.space.management.domain.Entities.Spaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace sms.space.management.data.access.Repositories
{
    public class QRCodeRepository : IQRCodeRepository
    {

        private readonly DbSession _session;

        public QRCodeRepository(DbSession session)
        {
            _session = session;
        }

        //public async Task<QRCodeResponseEntity> GenerateQRCode(QRCodeRequestEntity request)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> SaveQRCode(qrcodedetail request)
        {
            var query = $@"INSERT INTO  space_admin.QRCodeDetail(id,QrToken, QRCodeImage, userId,userName, RoomId, CreatedAt,IsUsed,ExpiresAt)
            VALUES (@id,@QrToken,@QRCodeImage,@userId,@userName,@RoomId,@CreatedAt,@IsUsed,@ExpiresAt)";

            request.Id = await _session.Connection.ExecuteScalarAsync<string>(query, request, _session.Transaction);
            
            return false;
        }

        Task<qrcodedetail> IQRCodeRepository.GenerateQRCode(qrcodedetail request)
        {
            throw new NotImplementedException();
        }

        public async Task<qrcodedetail> ValidateQRCode(qrcodedetail request)
        {
            var query = @"select * from QRcodeDetail where QrToken = @QrToken";

            var result = await _session.Connection.QueryAsync<qrcodedetail>(query, new { QrToken = request.QrToken }, _session.Transaction);

            return (qrcodedetail)result;
        }
    }
}
