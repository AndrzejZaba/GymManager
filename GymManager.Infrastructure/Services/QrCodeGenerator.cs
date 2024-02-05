using GymManager.Application.Common.Interfaces;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace GymManager.Infrastructure.Services;

public class QrCodeGenerator : IQrCodeGenerator
{
    public string Get(string message)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qRCodeData = qrGenerator.CreateQrCode(message, QRCodeGenerator.ECCLevel.Q);

        QRCode qRCode = new QRCode(qRCodeData);

        Bitmap bitmap = qRCode.GetGraphic(20);

        using (MemoryStream ms = new MemoryStream())
        {
            bitmap.Save(ms, ImageFormat.Png);
            var byteImage = ms.ToArray();
            return "data:image/png;base64," + Convert.ToBase64String(byteImage);
        }
    }
}
