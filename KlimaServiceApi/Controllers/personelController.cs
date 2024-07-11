using Dapper;
using DapperCrud.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace KlimaServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class personelController : Controller
    {
        private readonly IConfiguration _config;

        public personelController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPersonel()
        {
            async Task<ActionResult> AllPersonel()
            {
                string query = "SELECT * FROM tbl_personel";
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                {
                    var values = await connection.QueryAsync(query);
                    return Ok(values);
                }
            }

            var values = await AllPersonel();
            return Ok(values);
        }

        [HttpPost]
        public async Task<ActionResult<List<tbl_personel>>> AddPersonel(tbl_personel personel)
        {
            
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            {
                string query = "SELECT * FROM tbl_personel";
                var values = await connection.QueryAsync(query);
                await connection.ExecuteAsync("insert into tbl_personel(personel_adi, personel_soyadi, personel_telno,personel_kullanici_adi, personel_sifre) values (@personel_adi, @personel_soyadi,@personel_telno,@personel_kullanici_adi,@personel_sifre)", personel);
                return Ok(await SelectAlllPersonel(connection));
            }
        }



        [HttpGet("Login")]
        public ActionResult LoginPersonal(string kadi, string sifre)
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                var personel = connection.QueryFirstOrDefault("select * from tbl_personel where personel_kullanici_adi = @personel_kullanici_adi and personel_sifre = @personel_sifre", new { personel_kullanici_adi = kadi, personel_sifre = sifre });
               
                if (personel != null && personel!= Empty)
                {
                    return Ok(personel);
                }
                return BadRequest("Giriş Bilgileriniz Hatalı");
            }
            catch (Exception ex)
            {

                return Ok(ex);

            }

            
            


        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<tbl_personel>>> DeletePersonel(int Id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from tbl_personel where personel_id= @personel_id", new { personel_id = Id });
            return Ok(await SelectAlllPersonel(connection));
        }



        [HttpPut("{Id}")]
        public async Task<ActionResult<List<tbl_personel>>> UpdatePersonel(tbl_personel Id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("Update tbl_personel set personel_adi=@personel_adi, personel_soyadi=@personel_soyadi, personel_telno=@personel_telno,personel_kullanici_adi = @personel_kullanici_adi, personel_sifre = @personel_sifre where personel_id=@personel_id", Id);
            return Ok(await SelectAlllPersonel(connection));


        }

        private async Task<object?> SelectAlllPersonel(SqlConnection connection)
        {
            return await connection.QueryAsync<tbl_personel>("SELECT * FROM tbl_personel");
        }
    }
}
