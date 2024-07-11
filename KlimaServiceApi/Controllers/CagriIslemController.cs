using Dapper;
using DapperCrud.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace KlimaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CagriIslemController : Controller
    {
        private readonly IConfiguration _config;

        public CagriIslemController(IConfiguration config)
        {
            _config = config;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllCagriİslemleri()
        {
            async Task<ActionResult> AllCagriIslemleri()
            {
                string query = "SELECT * FROM tbl_cagri_islemleri" ;
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                {
                    var values = await connection.QueryAsync(query);
                    return Ok(values);
                }
            }

            var values = await AllCagriIslemleri();
            return Ok(values);
        }

        [HttpPost]
        public async Task<ActionResult<List<tbl_cagri_islemleri>>> PostCagriİslemleri(tbl_cagri_islemleri cagri_islem)
        {
            string query = "SELECT * FROM tbl_cagri_islemleri";
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            {
                var values = await connection.QueryAsync(query);
                await connection.ExecuteAsync("insert into tbl_cagri_islemleri(cagri_durum, cagri_yapilan_is, cagri_tarihi) values (@cagri_durum, @cagri_yapilan_is, @cagri_tarihi)", cagri_islem);
                return Ok(await SelectAllCagriIslemleri(connection));
            }
        }

        private async Task<object?> SelectAllCagriIslemleri(SqlConnection connection)
        {
            return await connection.QueryAsync<tbl_cagri_islemleri>("SELECT * FROM tbl_cagri_islemleri");
        }



        [HttpDelete("{cagri_islem_id}")]
        public async Task<ActionResult<List<tbl_cagri_islemleri>>> DeleteCagriIslem(int cagri_islem_id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from tbl_cagri_islemleri where cagri_islem_id= @cagri_islem_id", new { cagri_islem_id = cagri_islem_id });
            return Ok(await SelectAlllCagriİslemleri(connection));
        }

        private async Task<object?> SelectAlllCagriİslemleri(SqlConnection connection)
        {
            return await connection.QueryAsync<tbl_cagri_islemleri>("SELECT * FROM tbl_cagri_islemleri");
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<tbl_cagri_islemleri>>> UpdateCagriIslemleri(tbl_cagri_islemleri Id) 
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("Update tbl_cagri_islemleri set cagri_durum=@cagri_durum, cagri_yapilan_is=@cagri_yapilan_is, cagri_tarihi=@cagri_tarihi where cagri_islem_id=@cagri_islem_id", Id);
            return Ok(await SelectAlllCagriİslemleri(connection));
        
        
        }
    }







}

