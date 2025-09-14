using System;

namespace ChuongTrinhQuanLyXe
{
    class XeOto : Xe
    {
        public int SoChoNgoi { get; private set; }
        public bool CoKinhDoanhVanTai { get; private set; }

        public XeOto() { }

        public override void nhapThongTinXe(System.Collections.Generic.HashSet<string> tapBienSoDaCo)
        {
            while (true)
            {
                Console.Write("Nhập số lượng chỗ ngồi: ");
                if (int.TryParse(Console.ReadLine(), out int soChoNgoi) && soChoNgoi > 0)
                {
                    SoChoNgoi = soChoNgoi;
                    break;
                }
                Console.WriteLine("Giá trị không hợp lệ. Nhập lại!");
            }


            while (true)
            {
                Console.Write("Xe có đăng ký kinh doanh vận tải không? (Yes/No): ");
                string luaChon = (Console.ReadLine() ?? "").Trim();
                if (luaChon.Equals("Yes", StringComparison.OrdinalIgnoreCase)) { CoKinhDoanhVanTai = true; break; }
                if (luaChon.Equals("No", StringComparison.OrdinalIgnoreCase)) { CoKinhDoanhVanTai = false; break; }
                Console.WriteLine("Chỉ nhập Yes hoặc No!");
            }

            base.NhapThongTinChung(tapBienSoDaCo);
        }

        public override void xuatThongTinXe()
        {
            Console.WriteLine("=== Ô TÔ ===");
            Console.WriteLine($"Số chỗ ngồi: {SoChoNgoi}");
            Console.WriteLine($"Kinh doanh vận tải: {(CoKinhDoanhVanTai ? "Có" : "Không")}");
            base.XuatThongTinChung();
        }

 
        public (DateTime hanDangKiemTiepTheo, int phiDangKiem) TinhDangKiemVaPhi()
        {
            var homNay = DateTime.Today;


            int phiDangKiem = (SoChoNgoi <= 10) ? 240_000 : 320_000;


            bool laNhom1 = (SoChoNgoi <= 9 && !CoKinhDoanhVanTai);

            DateTime ngayHienTai = NgaySX;
            if (laNhom1)
            {
                int thoiHanLanDau = 36;
                int dinhKy24Thang = 24;

                DateTime hanLanDau = NgaySX.AddMonths(thoiHanLanDau);
                if (homNay < hanLanDau) return (hanLanDau, phiDangKiem);

                ngayHienTai = hanLanDau;
                while (true)
                {
                    int tuoiXe = TinhSoNamTaiThoiDiem(ngayHienTai, NgaySX);
                    int soThangThem;
                    if (tuoiXe <= 7) soThangThem = dinhKy24Thang;
                    else if (tuoiXe <= 20) soThangThem = 12;
                    else soThangThem = 6;

                    var hanTiepTheo = ngayHienTai.AddMonths(soThangThem);
                    if (homNay < hanTiepTheo) return (hanTiepTheo, phiDangKiem);
                    ngayHienTai = hanTiepTheo;
                }
            }
            else
            {
               
                int thoiHanLanDau = 24;

                DateTime hanLanDau = NgaySX.AddMonths(thoiHanLanDau);
                if (homNay < hanLanDau) return (hanLanDau, phiDangKiem);

                ngayHienTai = hanLanDau;
                while (true)
                {
                    int tuoiXe = TinhSoNamTaiThoiDiem(ngayHienTai, NgaySX);
                    int soThangThem = (tuoiXe <= 5) ? 12 : 6;

                    var hanTiepTheo = ngayHienTai.AddMonths(soThangThem);
                    if (homNay < hanTiepTheo) return (hanTiepTheo, phiDangKiem);
                    ngayHienTai = hanTiepTheo;
                }
            }
        }

        private int TinhSoNamTaiThoiDiem(DateTime thoiDiem, DateTime ngaySX)
        {
            int soNam = thoiDiem.Year - ngaySX.Year;
            if (thoiDiem.Month < ngaySX.Month || (thoiDiem.Month == ngaySX.Month && thoiDiem.Day < ngaySX.Day))
                soNam--;
            return soNam < 0 ? 0 : soNam;
        }
    }
}
