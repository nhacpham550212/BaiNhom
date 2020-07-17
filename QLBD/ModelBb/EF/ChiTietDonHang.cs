namespace ModelBb.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaDia { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SoHoaDon { get; set; }

        public decimal? GiaThanh { get; set; }

        public int? SoLuong { get; set; }

        public decimal? Tongtien { get; set; }

        public virtual Dia Dia { get; set; }

        public virtual DonHang DonHang { get; set; }
    }
}
