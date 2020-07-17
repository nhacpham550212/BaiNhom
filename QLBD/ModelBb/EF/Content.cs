namespace ModelBb.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaND { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(250)]
        public string NoiDungDia { get; set; }

        [StringLength(100)]
        public string TenDia { get; set; }

        public decimal? GiaThanh { get; set; }

        public int? Soluong { get; set; }

        [StringLength(50)]
        public string MaTheLoai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayNhap { get; set; }

        [StringLength(150)]
        public string Image { get; set; }

        public long MaDia { get; set; }

        public virtual Dia Dia { get; set; }

        public virtual TheLoaiDia TheLoaiDia { get; set; }
    }
}
