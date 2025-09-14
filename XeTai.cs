using System;
using System.Collections.Generic;

namespace ChuongTrinhQuanLyXe
{
    class XeTai : Xe
    {
        public double TrongTaiTan { get; private set; }

        public XeTai() { }

        public override void nhapThongTinXe(HashSet<string> tapBienSoDaCo)
        {
            while (true)
            {
                Console.Write("Nhập trọng tải (tấn): ");
                if (double.TryParse(Console.ReadLine(), out double tt) && tt > 0)
                {
                    TrongTaiTan = tt;
                    break;
                }
                Console.WriteLine("Giá trị không hợp lệ. Nhập lại!");
            }

            base.NhapThongTinChung(tapBienSoDaCo);
        }

        public override void xuatThongTinXe()
        {
            Console.WriteLine("=== XE TẢI ===");
            Console.WriteLine($"Trọng tải: {TrongTaiTan} tấn");
            base.XuatThongTinChung();
        }


        public (DateTime HanDangKiemTiepTheo, int PhiDangKiem) TinhDangKiemVaPhi()
        {
            var homNay = DateTime.Today;


            int phi = (TrongTaiTan > 20) ? 560_000 : (TrongTaiTan >= 7 ? 350_000 : 320_000);

            DateTime hanLanDau = NgaySX.AddMonths(24);
            if (homNay < hanLanDau) return (hanLanDau, phi);

            DateTime moc = hanLanDau;
            while (true)
            {
                int tuoi = TinhSoNamTaiThoiDiem(moc, NgaySX);
                int congThem = (tuoi <= 7) ? 12 : 6;
                DateTime hanTiepTheo = moc.AddMonths(congThem);
                if (homNay < hanTiepTheo) return (hanTiepTheo, phi);
                moc = hanTiepTheo;
            }
        }

        private static int TinhSoNamTaiThoiDiem(DateTime thoiDiem, DateTime ngayGoc)
        {
            int soNam = thoiDiem.Year - ngayGoc.Year;
            if (thoiDiem.Month < ngayGoc.Month ||
                (thoiDiem.Month == ngayGoc.Month && thoiDiem.Day < ngayGoc.Day))
                soNam--;
            return soNam < 0 ? 0 : soNam;
        }
    }
}
