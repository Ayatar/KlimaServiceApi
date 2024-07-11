using Dapper;
using DapperCrud.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace KlimaServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CagriController : Controller
    {
        private readonly IConfiguration _config;

        public CagriController(IConfiguration config)
        {
            _config = config;
        }



        [HttpGet]
        public async Task<ActionResult> GetAllCagri()
        {
            async Task<ActionResult> AllCagri()
            {
                string query = "SELECT * FROM tbl_cagri";
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                {
                    var values = await connection.QueryAsync(query);
                    return Ok(values);
                }
            }

            var values = await AllCagri();
            return Ok(values);
        }

        [HttpPost]
        public async Task<ActionResult<List<tbl_cagri>>> PostCagri(tbl_cagri cagri)
        {
            string query = "SELECT * FROM tbl_cagri";
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            {
                var values = await connection.QueryAsync(query);
                await connection.ExecuteAsync("insert into tbl_cagri(cagri_tarihi, musteri_adi, musteri_soyadi,musteri_telno, musteri_adres,musteri_ariza) values (@cagri_tarihi, @musteri_adi, @musteri_soyadi,@musteri_telno,@musteri_adres,@musteri_ariza)", cagri);
                return Ok(await SelectAllCagriIslemleri(connection));
            }
        }

        private async Task<object?> SelectAllCagriIslemleri(SqlConnection connection)
        {
            return await connection.QueryAsync<tbl_cagri>("SELECT * FROM tbl_cagri");
        }



        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<tbl_cagri>>> DeleteCagriIslem(int Id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from tbl_cagri where cagri_id= @cagri_id", new { cagri_id = Id });
            return Ok(await SelectAlllCagri(connection));
        }

       

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<tbl_cagri>>> UpdateCagriIslemleri(tbl_cagri_islemleri Id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("Update tbl_cagri set cagri_tarihi=@cagri_tarihi, musteri_adi=@musteri_adi, musteri_soyadi=@musteri_soyadi,musteri_telno = @musteri_telno, musteri_adres = @musteri_adres, musteri_ariza = @musteri_ariza  where cagri_id=@cagri__id", Id);
            return Ok(await SelectAlllCagri(connection));


        }

        private async Task<object?> SelectAlllCagri(SqlConnection connection)
        {
            return await connection.QueryAsync<tbl_cagri>("SELECT * FROM tbl_cagri");
        }
    }
}
