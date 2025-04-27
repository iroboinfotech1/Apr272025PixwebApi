using sms.space.management.domain.Entities.QRCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IQRCodeRepository
    {

        Task<bool> SaveQRCode(qrcodedetail request);

        Task<qrcodedetail> GenerateQRCode(qrcodedetail request);

        Task<qrcodedetail> ValidateQRCode(qrcodedetail request);

    }
}
