using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ChuongTrinhQuanLyXe
{
    public abstract class Xe
    {
        public DateTime NgaySX { get; protected set; }
        public string BienSo { get; protected set; } = "";

        private static readonly Regex _mauBienSo =
            new Regex(@"^\d{2}[A-Z]\d-\d{5}$", RegexOptions.Compiled);

        protected void NhapThongTinChung(HashSet<string> tapBienSoDaCo)
        {

            while (true)
            {
                Console.Write("Nhập ngày/tháng/năm sản xuất (dd/MM/yyyy): ");
                string? s = Console.ReadLine();
                if (DateTime.TryParseExact(s, "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime d))
                {
                    NgaySX = d;
                    break;
                }
                Console.WriteLine("Ngày không hợp lệ, vui lòng nhập lại!");
            }

      
            while (true)
            {
                Console.Write("Nhập biển số (VD 62B6-88889): ");
                string bien = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (!_mauBienSo.IsMatch(bien))
                {
                    Console.WriteLine("Biển số sai định dạng. Nhập lại!");
                    continue;
                }
                if (tapBienSoDaCo.Contains(bien))
                {
                    Console.WriteLine("Biển số đã tồn tại. Nhập biển khác!");
                    continue;
                }

                BienSo = bien;
                tapBienSoDaCo.Add(bien);
                break;
            }
        }

        protected void XuatThongTinChung()
        {
            Console.WriteLine($"Ngày sản xuất: {NgaySX:dd/MM/yyyy}");
            Console.WriteLine($"Biển số: {BienSo}");
        }

        public abstract void nhapThongTinXe(HashSet<string> tapBienSoDaCo);
        public abstract void xuatThongTinXe();
        public bool LaBienSoDep()
        {
            if (string.IsNullOrEmpty(BienSo)) return false;
            var parts = BienSo.Split('-');
            if (parts.Length != 2 || parts[1].Length != 5) return false;

            var so5 = parts[1];
            int[] dem = new int[10];
            foreach (char c in so5) dem[c - '0']++;
            foreach (int v in dem) if (v >= 4) return true;
            return false;
        }
        public bool ThuocTPHCM()
        {
            if (BienSo.Length < 2) return false;
            string dau2 = BienSo.Substring(0, 2);
            if (dau2 == "41") return true;
            if (int.TryParse(dau2, out int v)) return v >= 50 && v <= 59;
            return false;
        }
    }
}
