using Models;
using Services.InMemory;
using Services.Interfaces;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Services.InFile
{
    //dziedziczymy po EntityInMemoryService, ponieważ chcemy skorzystać z tam zapisanej funkcjonalności, a tylko dodać opcje zapisu
    public class EntityInFileService<T> : EntityInMemoryService<T>, IEntityService<T> where T : Entity
    {
        protected string FilePath { get; set; }

        protected EntityInFileService() { }

        public EntityInFileService(string filePath)
        {
            FilePath = filePath;
            LoadData();
        }

        //override - nadpisujemy implementację funkcji wirtualnej
        public override void Create(T item)
        {            
            //base - wykonujemy wersję metody z klasy bazowej
            base.Create(item);

            SaveData();
        }

        public override bool Delete(int id)
        {
            var result = base.Delete(id);
            if(result)
                SaveData();

            return result;
        }

        public override bool Update(int id, T item)
        {
            var result = base.Update(id, item);
            if(result)
                SaveData();

            return result;
        }

        private void SaveData()
        {
            string json = JsonSerializer.Serialize(Items);

            WriteToFile(json);
        }


        protected virtual void WriteToFile(string json)
        {
            //File - fasada ułatwiająca pracę z plikami
            File.WriteAllText(FilePath, json);
        }

        private void SaveDataUsingStream()
        {
            string json = JsonSerializer.Serialize(Items);

            //klasy strumieniowe - klasy opierające swoje działanie na strumieniu byte'ów
            //wykorzustanie using spowoduje automatyczne wywołanie funkcji Dispose
            using FileStream fileStream = new FileStream(FilePath, FileMode.Create);
            //klasa pomocnicza do zapisu danych do strumienia obsługująca tekst
            using StreamWriter writer = new StreamWriter(fileStream);

            writer.Write(json);
            //metoda flush wymusza wypchnięcie danych do strumienia
            writer.Flush();

            //writer.Dispose();
            //fileStream.Dispose();
        }

        protected void LoadData()
        {
            if (!File.Exists(FilePath))
                return;
            
            string json = ReadFromFile();

            List<T> items = JsonSerializer.Deserialize<List<T>>(json)!;
            Items.AddRange(items);
        }

        protected virtual string ReadFromFile()
        {
            return File.ReadAllText(FilePath);
        }

        private void LoadDataUsingStream()
        {
            using FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate);
            using StreamReader reader = new StreamReader(fileStream);

            string json = reader.ReadToEnd();

            List<T> items = JsonSerializer.Deserialize<List<T>>(json)!;

            Items.AddRange(items);
        }

        private void SaveDataToXml()
        {
            XmlSerializer serializer = new XmlSerializer(Items.GetType());
            using FileStream fileStream = new FileStream(FilePath, FileMode.Create);

            serializer.Serialize(fileStream, Items);

        }
        private void LoadDataFromXml()
        {
            using FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate);

            XmlSerializer serializer = new XmlSerializer(Items.GetType());
            var items = (List<T>)serializer.Deserialize(fileStream);


            Items.AddRange(items);
        }
    }
}
