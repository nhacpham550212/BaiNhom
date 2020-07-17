using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QLBD.Areas.admin.Models
{
    public class ProductModel
    {
        [Key]
        public long MaMH { get; set; }

        [Required(ErrorMessage = "Chưa nhập Tên mặt Hàng")]
        [StringLength(100)]
        public string TenMh { get; set; }
        [Required(ErrorMessage = "Chưa nhập Giá thành")]
        public decimal? GiaThanh { get; set; }

        public decimal? GiaKhuyenMai { get; set; }
        [Required(ErrorMessage = "Chưa nhập số lượng")]
        public int? Soluong { get; set; }
        [StringLength(50)]
        public string MaLoaiHang { get; set; }
        [Column(TypeName = "date")]


        public string NgayNhap { get; set; }

        [Column(TypeName = "date")]
        public string NgaySuaDoi { get; set; }
        [Required(ErrorMessage = "Chưa chọn avatar")]
        [StringLength(150)]
        public string Image { get; set; }
        public bool? status { get; set; }
        [Required(ErrorMessage = "Chưa nhập Màn Hình")]

        [StringLength(50)]
        public string link { get; set; }
    }
}