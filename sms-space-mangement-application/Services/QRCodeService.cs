using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.Extensions.Configuration;
using QRCoder;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.ReportFault;
using sms.space.management.domain.Entities.QRCode;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace sms.space.management.application.Services
{
    public class QRCodeService : IQRCodeService
    {

        public string GenerateQRCodeBase64(string text)
        {
            // Create a new instance of the QRCodeGenerator class
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                // Generate the QR Code data
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

                // Create a Base64QRCode instance
                using (Base64QRCode qrCode = new Base64QRCode(qrCodeData))
                {
                    // Generate the QR code image as a Base64 string
                    string qrCodeBase64 = qrCode.GetGraphic(20); // 20 is the pixel per module value

                    return qrCodeBase64;
                }
            }
        }

        private readonly IQRCodeRepository _repository;
        public QRCodeService(IQRCodeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> SaveQRCode(qrcodedetail request)
        {
            var QRCodegenerate = await _repository.SaveQRCode(request);
            throw new NotImplementedException();
        }

        public Task<qrcodedetail> GenerateQRCode(qrcodedetail request)
        {
            string QrImage= this.GenerateQRCodeBase64(request.QrToken);
            string qrImageBase64 = this.GenerateQRCodeBase64(request.QrToken);
            byte[] qrImageBytes = Convert.FromBase64String(qrImageBase64);


            return Task.FromResult(new qrcodedetail() { QRCodeImage= qrImageBytes, CreatedAt = DateTime.Now, UserId=request.UserId, UserName=request.UserName });
        }

        public async Task<qrcodedetail> ValidateQRCode(qrcodedetail request)
        {
            var QRCodegenerate = await _repository.ValidateQRCode(request);
            throw new NotImplementedException();
        }

    }

}
