using Models;
using Services.InMemory;
using Services.Interfaces;
using System.Text.Json;

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
            using FileStream fileStream = new FileStream(_filePath, FileMode.OpenOrCreate);
            using StreamReader reader = new StreamReader(fileStream);

            string json = reader.ReadToEnd();

            List<T> items = JsonSerializer.Deserialize<List<T>>(json)!;

            Items.AddRange(items);
        }
    }
}
