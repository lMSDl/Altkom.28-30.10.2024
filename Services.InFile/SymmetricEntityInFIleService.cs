using Models;
using Services.InFile.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InFile
{
    public class SymmetricEntityInFileService<T> : EntityInFileService<T> where T : Entity
    {
        private string _password;
        private SymmetricEncryption _encryption;
        public SymmetricEntityInFileService(string filePath, string password)
        {
            FilePath = filePath;
            _password = password;
            _encryption = new SymmetricEncryption("alamakota");
            LoadData();
        }

        protected override string ReadFromFile()
        {
            var data = File.ReadAllBytes(FilePath);
            data = _encryption.Decrypt(data, _password);

            return Encoding.Default.GetString(data);
        }

        protected override void WriteToFile(string json)
        {
            var data = _encryption.Encrypt(json, _password);
            File.WriteAllBytes(FilePath, data);
        }
    }
}
