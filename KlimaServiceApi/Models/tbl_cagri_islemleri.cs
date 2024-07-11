namespace DapperCrud.Database
{
    public class tbl_cagri_islemleri
    {
        public int cagri_islem_id { get; set; }

        public string cagri_durum { get; set; }

        public string cagri_yapilan_is { get; set; }

        public string cagri_tarihi { get; set; }

        public int? personel_id { get; set; }
        
        public int? cagri_id { get; set; }
    }
}
