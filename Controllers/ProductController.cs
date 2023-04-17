using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_webApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        private readonly IConfiguration _configuration;
        private SqlConnection connection;
        public ProductController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connection = new SqlConnection(_configuration.GetConnectionString("DB"));
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                
                SqlCommand command = new SqlCommand("select * from Products",connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    ProductModel product = new ProductModel();
                    product.Id = (int)reader["ID"];
                    product.Name = (string)reader["Name"];
                    product.Description = (string)reader["Description"];

                    products.Add(product);


                }
                reader.Close();
                connection.Close();
                return Ok(products);
               
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return NotFound();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
