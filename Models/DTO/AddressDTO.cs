using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace projAndreTurismoApp.Models.DTO
{
    public class AddressDTO
    {
        #region Propriedades
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("logradouro")]
        public string Street { get; set; }

        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }
        
        [JsonProperty("cep")]
        public string ZipCode { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }

        [JsonProperty("uf")]
        public string State { get; set; }
        #endregion
    }
}
