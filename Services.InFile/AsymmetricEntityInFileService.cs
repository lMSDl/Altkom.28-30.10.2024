using Models;
using Services.InFile.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InFile
{
    public class AsymmetricEntityInFileService<T> : EntityInFileService<T> where T : Entity
    {
        private string _certName;
        private AsymmetricEncryption _encryption;
        public AsymmetricEntityInFileService(string filePath, string certName)
        {
            FilePath = filePath;
            _certName = certName;
            _encryption = new AsymmetricEncryption();
            LoadData();
        }

        protected override string ReadFromFile()
        {
            var data = File.ReadAllBytes(FilePath);
            data = _encryption.Decrypt(data, _certName);

            return Encoding.Default.GetString(data);
        }

        protected override void WriteToFile(string json)
        {
            var data = _encryption.Encrypt(json, _certName);
            File.WriteAllBytes(FilePath, data);
        }
    }
}
