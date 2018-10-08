using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get () {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public ActionResult<string> Get (int id) {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post ([FromBody] Person person) {

            if (!ModelState.IsValid) {
                return new string[] { ModelState.Keys.First (), ModelState.Values.First ().Errors.First ().ErrorMessage };
            }
            return new string[] { "Success" };
        }
    }

    public class Person {

        [Display (Name = "名前")]
        [Required]
        [MaxLength (2, ErrorMessage = "{0}は10文字まで")]
        [CustomValidation (typeof (Person), "CheckName")]
        public string Name { get; set; }

        [Display (Name = "年齢")]
        [Range (0, 99, ErrorMessage = "{0}は99歳まで")]
        public int Age { get; set; }

        public static ValidationResult CheckName (string value, ValidationContext context) {
            var model = (Person) context.ObjectInstance;
            if (model.Name == "saito" && model.Age == 29) {
                return new ValidationResult ("この人はダメです。");
            }
            return ValidationResult.Success;
        }
    }
}