using QRCoder;
using System.Drawing.Imaging;

namespace EventManagmentSystem.Application.Services
{
    public class QrCodeGeneratorService
    {
        public byte[] GenerateQRCodeImage(string ticketId)
        {
            // Create a new instance of the QR code generator
            using (var qrGenerator = new QRCodeGenerator())
            {
                // Create the QR code data with the ticketId encoded
                var qrCodeData = qrGenerator.CreateQrCode(ticketId, QRCodeGenerator.ECCLevel.Q);

                // Create a QR code object based on the data
                var qrCode = new QRCode(qrCodeData);

                // Generate a QR code as a bitmap image (Graphic) with specified pixel size
                using (var qrCodeImage = qrCode.GetGraphic(20)) // Adjust the size with the pixel size
                {
                    // Save the QR code image to a memory stream
                    using (var memoryStream = new MemoryStream())
                    {
                        // Save the bitmap as a PNG to the stream
                        qrCodeImage.Save(memoryStream, ImageFormat.Png);

                        // Return the byte array representing the QR code image
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }
}
