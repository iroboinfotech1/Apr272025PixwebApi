using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.QRCode
{
    public class qrcodedetail
    {
        public string Id { get; set; }

        public string QrToken { get; set; } = string.Empty;

        public byte[]? QRCodeImage { get; set; }  // Change type to byte[] to match PostgreSQL bytea

        public string? UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? RoomId { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool? IsUsed { get; set; }

        public DateTime ExpiresAt { get; set; }
    }


    public class QRCodeResponseEntity
    {
        public string? Status { get; set; }

        public string? Message { get; set; }

    }
}
