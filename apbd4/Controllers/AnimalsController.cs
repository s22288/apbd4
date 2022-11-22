using apbd4.model;
using apbd4.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd4.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AnimalsController : Controller
    {
        private readonly IDBinterface _idbInterface;
        public AnimalsController(IDBinterface dBinterface)
        {
            _idbInterface = dBinterface;
        }

        [HttpGet("{order}")]
        public IActionResult getAllAnimals(String order)
        {

            if (order == null)
            {
                order = "name";
            }
            if (!order.Equals("name") && !order.Equals("IdAnimal") && !order.Equals("Description") && !order.Equals("Category") && !order.Equals("Area"))
            {

                return BadRequest("400 Cant order by unknown value");
            }

            List<Animal> avalibleAnimals = _idbInterface.getAllAnimals(order).ToList();

            return Ok(avalibleAnimals);
        }

        [HttpPost]
        public IActionResult postAnimal(Animal animal)
        {
            if (animal == null)
            {
                return BadRequest("400 animal cant be null");
            }

            Animal added = _idbInterface.addAnimalToDb(animal).Result;

            return Ok("Dodano zwierze o danych" + added.ToString());
        }
        [HttpDelete("{animalId}")]
        public IActionResult deleteAnimal(String animalId)
        {
           Boolean ifExists = _idbInterface.checkIfRecordExists(animalId).Result;
            if (!ifExists)
            {
                return BadRequest("400 animal not exits");
                 
            }
            if(animalId == null)
            {
                return BadRequest("400 animal id cant be null");
            }

            Boolean record = _idbInterface.deleteAnimalById(animalId).Result ;

            if(!record)
            {
                return BadRequest("Sql eroor");
            }
            return Ok("Animal with id " + animalId + " was deleted");
        }
        [HttpPut("{idAnimal}")]
        public IActionResult updateAnimalInDb(Animal animal,String idAnimal)
        {
            Boolean ifExists = _idbInterface.checkIfRecordExists(idAnimal).Result;
            if (!ifExists)
            {
                return BadRequest("400 animal not exits");

            }
            if (idAnimal== null)
            {
                return BadRequest("Id Cant be null");

            }
           
            
         String id =   _idbInterface.updateAnimal(animal, idAnimal).Result;

            return Ok("Animal with id " + id + " was updated");
        }
    }

   
}
