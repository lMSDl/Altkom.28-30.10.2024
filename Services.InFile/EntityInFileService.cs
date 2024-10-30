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
        //readonly - pole tylko do odczytu, można ustawić wartość tylko w konstruktorze
        private readonly string _filePath;

        public EntityInFileService(string filePath)
        {
            _filePath = filePath;
            LoadDataFromXml();
        }

        //override - nadpisujemy implementację funkcji wirtualnej
        public override void Create(T item)
        {            
            //base - wykonujemy wersję metody z klasy bazowej
            base.Create(item);

            SaveDataToXml();
        }

        public override bool Delete(int id)
        {
            var result = base.Delete(id);
            if(result)
                SaveDataToXml();

            return result;
        }

        public override bool Update(int id, T item)
        {
            var result = base.Update(id, item);
            if(result)
                SaveDataToXml();

            return result;
        }

        private void SaveData()
        {
            string json = JsonSerializer.Serialize(Items);

            //File - fasada ułatwiająca pracę z plikami
            File.WriteAllText(_filePath, json);
        }

        private void SaveDataUsingStream()
        {
            string json = JsonSerializer.Serialize(Items);

            //klasy strumieniowe - klasy opierające swoje działanie na strumieniu byte'ów
            //wykorzustanie using spowoduje automatyczne wywołanie funkcji Dispose
            using FileStream fileStream = new FileStream(_filePath, FileMode.Create);
            //klasa pomocnicza do zapisu danych do strumienia obsługująca tekst
            using StreamWriter writer = new StreamWriter(fileStream);

            writer.Write(json);
            //metoda flush wymusza wypchnięcie danych do strumienia
            writer.Flush();

            //writer.Dispose();
            //fileStream.Dispose();
        }

        private void LoadData()
        {
            string json = File.ReadAllText(_filePath);

            List<T> items = JsonSerializer.Deserialize<List<T>>(json)!;
            Items.AddRange(items);
        }

        private void LoadDataUsingStream()
        {
            using FileStream fileStream = new FileStream(_filePath, FileMode.OpenOrCreate);
            using StreamReader reader = new StreamReader(fileStream);

            string json = reader.ReadToEnd();

            List<T> items = JsonSerializer.Deserialize<List<T>>(json)!;

            Items.AddRange(items);
        }

        private void SaveDataToXml()
        {
            XmlSerializer serializer = new XmlSerializer(Items.GetType());
            using FileStream fileStream = new FileStream(_filePath, FileMode.Create);

            serializer.Serialize(fileStream, Items);

        }
        private void LoadDataFromXml()
        {
            using FileStream fileStream = new FileStream(_filePath, FileMode.OpenOrCreate);

            XmlSerializer serializer = new XmlSerializer(Items.GetType());
            var items = (List<T>)serializer.Deserialize(fileStream);


            Items.AddRange(items);
        }
    }
}
