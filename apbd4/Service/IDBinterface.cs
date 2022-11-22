using apbd4.model;

namespace apbd4.Service
{
    public interface IDBinterface
    {
        public IEnumerable<Animal> getAllAnimals(String order);

        public Task<Animal> addAnimalToDb(Animal animal);


        public Task<Boolean> deleteAnimalById(String id);

        public Task<String> updateAnimal(Animal animal, String id);

        public Task<Boolean> checkIfRecordExists(String id);

    }
}
