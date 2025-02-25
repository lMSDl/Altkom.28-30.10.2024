﻿using Models;
using Services.Interfaces;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Warehouse.Properties;

namespace Warehouse
{
    internal abstract class GenericWarehouse<T> where T : Entity
    {
        IEntityService<T> _service;

        protected GenericWarehouse(IEntityService<T> service)
        {
            _service = service;
        }

        public void Show()
        {
            foreach (T item in _service.Read())
            {
                Console.WriteLine(GetItemInfo(item));
            }
        }

        //nie znamy implementacji dla T
        protected abstract string GetItemInfo(T items);

        public void Create()
        {
            var item = CreateNewItem();

            _service.Create(item);
        }

        //nie wiemy jak stworzyć element typu T, więc tworzymy metodę abstrakcyjną, której ciało będzie musiało być zapewnione w klasach pochodnych
        protected abstract T CreateNewItem();

        public void Delete()
        {
            int id = GetId();

            var result = _service.Delete(id);
            if (!result)
            {
                ShowInfo("Produkt o danym Id nie mógł być usunięty lub nie istnieje");
            }
        }

        public void Edit()
        {
            int id = GetId();
            var old = _service.Read(id);

            if (old == null)
            {
                ShowInfo(string.Format(Resources.idElementNotExists, id));
                return;
            }

            var item = CreateUpdatedItem(old);

            _ = _service.Update(id, item);
        }

        //nie znamy implementacji dla T
        protected abstract T CreateUpdatedItem(T old);

        public static void ShowInfo(string output)
        {
            Console.WriteLine(output);
            //_ = - discard, używany gdy chcemy zaznaczyć, że wiemy iż metoda zwraca jakiś rezultat, ale nie jest on nam potrzebny / nie interesuje nas
            _ = Console.ReadLine();
        }

        private int GetId()
        {
            var input = GetString("Id:");
            //try-catch - służy do obsługi wyjątków.
            //W bloku try umieszczamy kod, w którym może potencjalnie wystąpić wyjątek
            try
            {
                return int.Parse(input);
            }
            //Blok catch zawiera kod, który ma być wykonany jeśli wyjątek wystąpi
            //catch - bez parametru - przechwytuje wszystkie wyjątki i nie daje wglądu w obiekt wyjątku
            catch
            {
                return 0;
            }
        }

        public static string GetString(string label)
        {
            Console.WriteLine(label);

            //metoda zwraca nullowany obiekt, który przypisujemy do nie-nullowalnego
            //! - za pomocą wykrzyknika wyciszamy ostrzeżenie o niedopasowaniu typów nullowalnych, ponieważ w tym przypadku nie oczekujemy, że null wystąpi
            return Console.ReadLine()!;
        }

        public static float GetFloat(string label) {
            var input = GetString(label);
            try
            {
                var result = float.Parse(input);
                if (result < 0)
                    //throw służy do rzucenia wyjątku. Po tym słowie kluczonym tworzymy wyjątek, który ma zostać rzucony
                    throw new InvalidDataException("Wartość nie może być ujemna.");

                return result;
            }
            //możemy mieć wiele bloków catch w kolejność od szczegółu do ogółu (najbardziej ogólny wyjątek na końcu)
            //dzięki temu możemy wykonywać różne akcje w zależności od klasy wyjątku
            //catch() - z parametrem - przechwytuje wyjątki zgodne z klasą parametru, dając wgląd w obiekt
            catch (FormatException e) //korzystamy z właściwości wyjątku, więc nazywamy jego obiekt jako "e", żeby mieć do niego dostęp
            {
                ShowInfo($"Error: {e.Message}");
                //throw bez dodatkowego kodu służy do rzucenia ponownie wyjątku, który właśnie obsługujemy
                throw;
            }
            catch (Exception e)
            {
                ShowInfo($"Error: {e.Message}");
                return GetFloat(label);
            }
        }

        protected DateTime GetDateTime(string label)
        {
            var input = GetString(label);
            DateTime dateTime;
            //metoda TryParse zamiast rzucać wyjątek przy niepowodzeniu, zwraca informację w postaci bool (true/false) czy parsowanie się powiodło
            //rezultat parsowania możemy użyskać z drugiego parametru
            //parametry, który zwracają dodatkowe informacje z funkcji nazywane są parametrami wyjściowymi i muszą być poprzedzone słowem kluczowym "out" zarówno w definicji funkcji jak i podczas jej wywoływania
            bool success = DateTime.TryParse(input, out dateTime);
            if (success)
                return dateTime;
            else
            {
                ShowInfo("Błędny format daty..");
                return GetDateTime(label);
            }
        }

        internal void ToJson()
        {
            /*var id = GetId();
            var item = _service.Read(id);
            if (item == null)
                return;*/

            var item = _service.Read();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreReadOnlyProperties = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
            };

            var json  = JsonSerializer.Serialize(item, options);
            ShowInfo(json);
        }

        internal void ToXml()
        {
            /*var id = GetId();
            var item = _service.Read(id);
            if (item == null)
                return;*/

            var item = _service.Read();

            XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());
            //zamiast ręcznie wywoływać funkcję Dispose(), możemy dodać przed deklaracją z inicjalizacją klasy strumieniowej słowo kluczowe "using".
            //spowoduje to, że po wyjściu z funkcji automatycznie zostanie wywołana metoda Dispose().
            using MemoryStream stream = new MemoryStream();
            xmlSerializer.Serialize(stream, item);

            string xml = Encoding.Default.GetString(stream.ToArray());

            //funkcja służąca zwolnieniu zasobów zarezerwowanych przez obiekt (wywołanie zapobiega wyciekom pamięci)
            //stream.Dispose();

            ShowInfo(xml);
        }
    }
}
