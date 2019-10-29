using System.Windows.Media.Imaging;
using System.Drawing;
using Tool;

namespace Entity
{
    class CreateQr
    {
        public CreateQr()
        {

        }

        public Bitmap ToQrcode(patient patient,string key)
        {
            return QRCodeEncoder(AesEncrypt(PatientToJson(patient),key));
        }

        public patient FromQrcode(Bitmap bitmap, string key)
        {
            return JsonToPatient(AesDecrypt(QRCodeDecoder(bitmap),key));
        }

        public Bitmap QRCodeEncoder(string qrCodeContent)
        {
            qrcode qr = new qrcode();
            return qr.QRCodeEncoder(qrCodeContent);
        }

        public string QRCodeDecoder(Bitmap bitmap)
        {
            qrcode qr = new qrcode();
            return qr.QRCodeDecoder(bitmap);
        }
        public string PatientToJson(patient obj)
        {
            return JsonTool.ObjectToJson(obj);
        }

        public patient JsonToPatient(string jsonString)
        {
            patient p = new patient();
            return (patient)JsonTool.JsonToObject(jsonString, p.GetType());
        }

        public string AesEncrypt(string pass ,string key)
        {
            return AES.AESEncrypt(pass,key);
        }

        public string AesDecrypt(string pass, string key)
        {
            return AES.AESDecrypt(pass,key);
        }

    }
}
